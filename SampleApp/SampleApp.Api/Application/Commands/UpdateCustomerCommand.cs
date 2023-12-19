using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Commands;

public record UpdateCustomerCommand(long Id, CustomerInfo Data) : IRequest;
