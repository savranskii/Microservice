using MediatR;
using SampleApp.Api.Application.Models;
using SampleApp.Domain.Customer.Entities;

namespace SampleApp.Api.Application.Queries;

public record SearchCustomerQuery(SearchCustomerRequest Data) : IRequest<Customer>;