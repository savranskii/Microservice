using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Commands;

public record CreateCustomerCommand(string Email) : IRequest<CustomerInfo>;
