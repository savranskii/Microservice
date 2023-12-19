using MediatR;

namespace SampleApp.Api.Application.Commands;

public record DeleteCustomerCommand(long Id) : IRequest;
