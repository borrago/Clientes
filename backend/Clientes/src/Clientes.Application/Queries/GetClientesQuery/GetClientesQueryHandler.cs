using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;

namespace Clientes.Application.Queries.GetClientesQuery;

public class GetClientesQueryHandler(IClienteMongoRepository clientesRepository) : IRequestHandler<GetClientesQueryInput, List<ClienteModel>>
{
    private readonly IClienteMongoRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));

    public Task<List<ClienteModel>> Handle(GetClientesQueryInput request, CancellationToken cancellationToken) =>
        _clientesRepository.GetAllAsync(cancellationToken);
}