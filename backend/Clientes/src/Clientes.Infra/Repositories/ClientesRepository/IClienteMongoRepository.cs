using Clientes.Domain.ClientAggregate;

namespace Clientes.Infra.Repositories.ClientesRepository;

public interface IClienteMongoRepository : IMongoGenericRepository<ClienteModel>
{
}