using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Events;

public record AddedClienteEventInput(Guid Id, string NomeEmpresa, PorteEmpresa Porte) : INotification;
