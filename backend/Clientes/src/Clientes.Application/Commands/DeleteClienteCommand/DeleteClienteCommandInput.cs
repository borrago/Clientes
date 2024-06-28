using MediatR;

namespace Clientes.Application.Commands.DeleteClienteCommand;

public record DeleteClienteCommandInput(Guid Id) : IRequest<bool>;