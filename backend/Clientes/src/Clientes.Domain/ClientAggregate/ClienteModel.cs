namespace Clientes.Domain.ClientAggregate;

public class ClienteModel
{
    public Guid Id { get; set; }
    public string NomeEmpresa { get; set; } = string.Empty;
    public string Porte { get; set; } = string.Empty;
}