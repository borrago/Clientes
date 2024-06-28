using Clientes.Domain.ClientAggregate;
using MediatR;

namespace Clientes.Application.Queries.GetClientesQuery;

public record GetClientesQueryInput : IRequest<List<ClienteModel>>;