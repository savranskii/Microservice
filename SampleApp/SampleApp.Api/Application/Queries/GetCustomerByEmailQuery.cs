using MediatR;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Queries;

public record GetCustomerByEmailQuery(string Email) : IRequest<Customer>;