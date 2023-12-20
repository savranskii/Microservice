using MediatR;
using SampleApp.Api.Application.Models;

namespace SampleApp.Api.Application.Commands;

public record UpdateCustomerCommand(long Id, UpdateCustomerRequest Data) : IRequest;
