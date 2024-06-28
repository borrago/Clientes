using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Commands.AddClienteCommand;

public record AddClienteCommandInput(string NomeEmpresa, PorteEmpresa Porte) : IRequest<Guid>;