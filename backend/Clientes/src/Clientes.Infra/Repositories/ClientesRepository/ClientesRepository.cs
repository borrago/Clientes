using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Contexts;

namespace Clientes.Infra.Repositories.ClientesRepository;

public class ClientesRepository(SqlServerDbContext context) : GenericRepository<Cliente>(context), IClientesRepository
{
}