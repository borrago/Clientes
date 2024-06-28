using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Queries.GetClienteByIdQuery;

public record GetClienteByIdQueryInput(Guid Id) : IRequest<ClienteModel>;