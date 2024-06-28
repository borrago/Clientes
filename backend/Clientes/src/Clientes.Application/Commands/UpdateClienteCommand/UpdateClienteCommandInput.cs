using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Commands.UpdateClienteCommand;

public record UpdateClienteCommandInput(Guid Id, string NomeEmpresa, PorteEmpresa Porte) : IRequest<bool>;