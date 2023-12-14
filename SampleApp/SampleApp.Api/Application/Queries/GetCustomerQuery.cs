using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Queries;

public record GetCustomerQuery(long Id) : IRequest<CustomerInfo>;
