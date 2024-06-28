using Clientes.Domain.ClientAggregate;
using MongoDB.Driver;

namespace Clientes.Infra.Contexts;

public class MongoDbContext(IMongoDatabase database)
{
    private readonly IMongoDatabase _database = database;

    public IMongoCollection<ClienteModel> Clientes => _database.GetCollection<ClienteModel>("Clientes");
}