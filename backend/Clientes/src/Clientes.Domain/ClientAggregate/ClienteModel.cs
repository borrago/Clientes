using Clientes.Domain.DomainObjects;

namespace Clientes.Domain.ClientAggregate;

public class ClienteModel : IMongoEntity
{
    public Guid Id { get; set; }
    public string NomeEmpresa { get; set; } = string.Empty;
    public string Porte { get; set; } = string.Empty;
}