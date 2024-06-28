using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Events;

public record UpdatedClienteEventInput(Guid Id, string NomeEmpresa, PorteEmpresa Porte) : INotification;
