using Clientes.Domain.ClientAggregate;
using MongoDB.Driver;

namespace Clientes.Infra.Repositories.ClientesRepository;

public class ClienteMongoRepository(IMongoDatabase database) : MongoGenericRepository<ClienteModel>(database, "clientes"), IClienteMongoRepository
{
}