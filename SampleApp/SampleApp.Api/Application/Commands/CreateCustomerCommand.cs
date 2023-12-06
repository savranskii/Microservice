using MediatR;

namespace SampleApp.Api.Application.Commands;

public record CreateCustomerCommand(string Email) : IRequest<long>;
