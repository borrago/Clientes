using Clientes.Domain.DomainObjects;
using System.ComponentModel.DataAnnotations;

namespace Clientes.Domain.ClientAggregate;

public class Cliente : Entity, IAggregateRoot
{
    // EF Core
    protected Cliente()
    {
    }

    public Cliente(string nomeEmpresa, PorteEmpresa porteEmpresa)
    {
        WithNomeEmpresa(nomeEmpresa);
        WithPorteEmpresa(porteEmpresa);
    }

    public void WithNomeEmpresa(string nomeEmpresa)
    {
        Validations.ValidarSeVazio(nomeEmpresa, "O campo NomeEmpresa não pode estar vazio.");
        Validations.ValidarSeNulo(nomeEmpresa, "O campo NomeEmpresa não pode ser nulo.");
        Validations.ValidarTamanho(nomeEmpresa, 250, "O campo NomeEmpresa não pode ser maior que 250 caracteres.");

        NomeEmpresa = nomeEmpresa;
    }

    public void WithPorteEmpresa(PorteEmpresa porte)
    {
        Validations.ValidarSeNulo(porte, "O campo Porte não pode ser nulo.");

        Porte = porte;
    }

    [MaxLength(250)]
    public string NomeEmpresa { get; private set; } = string.Empty;
    public PorteEmpresa Porte { get; private set; }
}