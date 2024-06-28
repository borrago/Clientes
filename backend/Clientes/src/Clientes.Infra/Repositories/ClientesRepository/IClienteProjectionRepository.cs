using Clientes.Domain.ClientAggregate;

namespace Clientes.Infra.Repositories.ClientesRepository;

public interface IClienteProjectionRepository
{
    Task<bool> InsertAsync(ClienteModel cliente, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(ClienteModel cliente, CancellationToken cancellationToken);
    Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken);
    Task<List<ClienteModel>> GetAllAsync(CancellationToken cancellationToken);
    Task<ClienteModel> GetByIdAsync(Guid id, CancellationToken cancellationToken);
}