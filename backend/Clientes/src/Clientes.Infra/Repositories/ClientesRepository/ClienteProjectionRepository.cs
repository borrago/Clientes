using Clientes.Domain.ClientAggregate;
using MongoDB.Driver;

namespace Clientes.Infra.Repositories.ClientesRepository;

public class ClienteProjectionRepository(IMongoDatabase database) : IClienteProjectionRepository
{
    private readonly IMongoCollection<ClienteModel> _clientes = database.GetCollection<ClienteModel>("clientes");

    public async Task<bool> InsertAsync(ClienteModel cliente, CancellationToken cancellationToken)
    {
        await _clientes.InsertOneAsync(cliente, new InsertOneOptions(), cancellationToken);
        return true;
    }

    public async Task<bool> UpdateAsync(ClienteModel cliente, CancellationToken cancellationToken)
    {
        var result = await _clientes.ReplaceOneAsync(c => c.Id == cliente.Id, cliente, new ReplaceOptions(), cancellationToken);
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> RemoveAsync(Guid id, CancellationToken cancellationToken)
    {
        var result = await _clientes.DeleteOneAsync(c => c.Id == id, cancellationToken);
        return result.IsAcknowledged && result.DeletedCount > 0;
    }

    public Task<List<ClienteModel>> GetAllAsync(CancellationToken cancellationToken)
        => _clientes.Find(_ => true).ToListAsync(cancellationToken);

    public Task<ClienteModel> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        => _clientes.Find(cliente => cliente.Id == id).FirstOrDefaultAsync(cancellationToken);
}