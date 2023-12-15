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
    //private WebApplicationFactory<Program> _factory;
    //private IMediator _mediator;
    //private ILogger<CustomerEndpoint> _logger;

    //[TestInitialize]
    //public async Task TestInitialize()
    //{
    //    _factory = new WebApplicationFactory<Program>();

    //    var scope = _factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
    //    var context = scope.ServiceProvider.GetRequiredService<ICustomerRepository>();

    //    await context.CreateAsync(new CustomerInfo("test@mail.com"));
    //}

    [TestMethod]
    public async Task GetCustomer_ById_Returns_CustomerOrNotFound()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var customer = new CustomerInfo("test@mail.com");
        await unityOfWork.CustomerRepository.CreateAsync(customer);
        await unityOfWork.SaveAsync();

        var endpoints = new CustomerEndpoint();

        var result1 = await endpoints.GetCustomerAsync(customer.Id, mediator, logger);
        var customerResult = result1.Result as Ok<CustomerInfo>;

        Assert.IsInstanceOfType<Results<Ok<CustomerInfo>, NotFound>>(result1);
        Assert.AreEqual("test@mail.com", customerResult?.Value?.Email);

        var result2 = await endpoints.GetCustomerAsync(2, mediator, logger);
        Assert.IsInstanceOfType<Results<Ok<CustomerInfo>, NotFound>>(result2);
    }

    [TestMethod]
    public async Task GetCustomers_Returns_AllCustomers()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("get1@mail.com"));
        await unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("get2@mail.com"));
        await unityOfWork.SaveAsync();

        var endpoints = new CustomerEndpoint();

        var result = await endpoints.GetCustomersAsync(mediator, logger);

        Assert.IsInstanceOfType<Ok<IEnumerable<CustomerInfo>>>(result);
        Assert.IsTrue(result?.Value?.Count() > 0);
    }

    [TestMethod]
    public async Task SearchCustomer_Returns_CustomerOrNotFound()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("search1@mail.com"));
        await unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("search2@mail.com"));
        await unityOfWork.SaveAsync();

        var endpoints = new CustomerEndpoint();

        var data = new SearchCustomerRequest("search1@mail.com");
        var result = await endpoints.SearchCustomerAsync(data, mediator, logger);
        var customerFound = result.Result as Ok<CustomerInfo>;
        Assert.IsNotNull(customerFound);
        Assert.IsInstanceOfType<Ok<CustomerInfo>>(customerFound);
        Assert.AreEqual("search1@mail.com", customerFound?.Value?.Email);

        data = new SearchCustomerRequest("search3@mail.com");
        result = await endpoints.SearchCustomerAsync(data, mediator, logger);
        var customerNotFound = result.Result as NotFound;
        Assert.IsNotNull(customerNotFound);
        Assert.IsInstanceOfType<NotFound>(customerNotFound);
    }

    [TestMethod]
    public async Task CreateCustomer_Returns_IdOfCreatedItem()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();

        var data = new CreateCustomerRequest("create@mail.com");

        var endpoints = new CustomerEndpoint();
        var result = await endpoints.CreateCustomerAsync(data, mediator, logger);

        Assert.IsInstanceOfType<Ok<long>>(result);
    }

    [TestMethod]
    public async Task UpdateCustomer_Returns_NoContent()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        await unityOfWork.CustomerRepository.CreateAsync(new CustomerInfo("update@mail.com"));
        await unityOfWork.SaveAsync();

        var endpoints = new CustomerEndpoint();

        await endpoints.UpdateCustomerAsync(new UpdateCustomerRequest(1, new("update@mail.net")), mediator, logger);

        var result = await endpoints.SearchCustomerAsync(new SearchCustomerRequest("update@mail.net"), mediator, logger);
        var customerResult = result.Result as Ok<CustomerInfo>;
        Assert.AreEqual("update@mail.net", customerResult?.Value?.Email);
    }

    [TestMethod]
    public async Task DeleteCustomer_Returns_NoContent()
    {
        await using var factory = new WebApplicationFactory<Program>();

        var scope = factory.Services.GetRequiredService<IServiceScopeFactory>().CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<CustomerEndpoint>>();
        var unityOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        var customer = new CustomerInfo("delete@mail.com");
        await unityOfWork.CustomerRepository.CreateAsync(customer);
        await unityOfWork.SaveAsync();

        var endpoints = new CustomerEndpoint();

        await endpoints.DeleteCustomerAsync(customer.Id, mediator, logger);

        var result = await endpoints.SearchCustomerAsync(new SearchCustomerRequest("delete@mail.com"), mediator, logger);
        var customerResult = result.Result as NotFound;
        Assert.IsInstanceOfType<NotFound>(customerResult);
    }
}
