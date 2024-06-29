using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;

namespace Clientes.Application.Queries.GetClienteByIdQuery;

public class GetClienteByIdQueryHandler(IClienteMongoRepository clientesRepository) : IRequestHandler<GetClienteByIdQueryInput, ClienteModel>
{
    private readonly IClienteMongoRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));

    public async Task<ClienteModel> Handle(GetClienteByIdQueryInput request, CancellationToken cancellationToken)
        => await _clientesRepository.GetByIdAsync(request.Id, cancellationToken);
}