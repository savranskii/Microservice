using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Models;

public record UpdateCustomerRequest(long Id, CustomerInfo Item);
