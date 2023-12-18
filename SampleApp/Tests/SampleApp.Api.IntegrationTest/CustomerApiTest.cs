using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using SampleApp.Api.Application.Models;
using SampleApp.Api.Infrastructure.Endpoints;
using SampleApp.Domain.Customer.Entities;
using SampleApp.Infrastructure.Services;

namespace SampleApp.Api.IntegrationTest;

[TestClass]
public class CustomerApiTest
{
    private IMediator _mediator;
    private ILogger<CustomerEndpoint> _logger;
    private IUnitOfWork _unityOfWork;
    private CustomerEndpoint _endpoints;

    [TestInitialize]
    public async Task TestInitialize()
    {
        var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        _mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        _logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        _unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
        _endpoints = new CustomerEndpoint();

        await _unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("test1@mail.com"));
        await _unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("test2@mail.com"));
        await _unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("test3@mail.com"));
        await _unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("test4@mail.com"));
        await _unityOfWork.SaveAsync();
    }

    [TestMethod]
    public async Task GetCustomer_ById_Returns_CustomerOrNotFound()
    {
        var result1 = await _endpoints.GetCustomerAsync(1, _mediator, _logger);
        var customerResult = result1.Result as Ok<CustomerInfo>;

        Assert.IsInstanceOfType<Results<Ok<CustomerInfo>, NotFound>>(result1);
        Assert.AreEqual("test1@mail.com", customerResult?.Value?.Email);

        var result2 = await _endpoints.GetCustomerAsync(2, _mediator, _logger);
        Assert.IsInstanceOfType<Results<Ok<CustomerInfo>, NotFound>>(result2);
    }

    [TestMethod]
    public async Task GetCustomers_Returns_AllCustomers()
    {
        var result = await _endpoints.GetCustomersAsync(_mediator, _logger);

        Assert.IsInstanceOfType<Ok<IEnumerable<CustomerInfo>>>(result);
        Assert.IsTrue(result?.Value?.Count() > 0);
    }

    [TestMethod]
    public async Task SearchCustomer_Returns_CustomerOrNotFound()
    {
        var data = new SearchCustomerRequest("test1@mail.com");
        var result = await _endpoints.SearchCustomerAsync(data, _mediator, _logger);
        var customerFound = result.Result as Ok<CustomerInfo>;
        Assert.IsNotNull(customerFound);
        Assert.IsInstanceOfType<Ok<CustomerInfo>>(customerFound);
        Assert.AreEqual("test1@mail.com", customerFound?.Value?.Email);

        data = new SearchCustomerRequest("test5@mail.com");
        result = await _endpoints.SearchCustomerAsync(data, _mediator, _logger);
        var customerNotFound = result.Result as NotFound;
        Assert.IsNotNull(customerNotFound);
        Assert.IsInstanceOfType<NotFound>(customerNotFound);
    }

    [TestMethod]
    public async Task CreateCustomer_Returns_IdOfCreatedItem()
    {
        var result = await _endpoints.CreateCustomerAsync(new CreateCustomerRequest("create@mail.com"), _mediator, _logger);
        Assert.IsInstanceOfType<Ok<CustomerInfo>>(result);
    }

    [TestMethod]
    public async Task UpdateCustomer_Returns_NoContent()
    {
        await _endpoints.UpdateCustomerAsync(new UpdateCustomerRequest(1, new("test1@mail.net")), _mediator, _logger);

        var result = await _endpoints.SearchCustomerAsync(new SearchCustomerRequest("test1@mail.net"), _mediator, _logger);
        var customerResult = result.Result as Ok<CustomerInfo>;
        Assert.IsNotNull(customerResult?.Value);
    }

    [TestMethod]
    public async Task DeleteCustomer_Returns_NoContent()
    {
        var searchResult1 = await _endpoints.SearchCustomerAsync(new SearchCustomerRequest("test4@mail.com"), _mediator, _logger);
        var customerResult1 = searchResult1.Result as Ok<CustomerInfo>;
        await _endpoints.DeleteCustomerAsync(customerResult1?.Value?.Id ?? 0, _mediator, _logger);

        var searchResult2 = await _endpoints.SearchCustomerAsync(new SearchCustomerRequest("test4@mail.com"), _mediator, _logger);
        var customerResult2 = searchResult2.Result as NotFound;
        Assert.IsNull(customerResult2);
    }
}
