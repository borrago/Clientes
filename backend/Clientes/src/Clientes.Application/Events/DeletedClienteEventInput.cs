using MediatR;

namespace Clientes.Application.Events;

public record DeletedClienteEventInput(Guid Id) : INotification;
