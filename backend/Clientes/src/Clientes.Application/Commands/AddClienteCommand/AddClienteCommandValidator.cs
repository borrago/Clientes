using Clientes.Domain.ClientAggregate;
using FluentValidation;

namespace Clientes.Application.Commands.AddClienteCommand;

public class AddClienteCommandValidator : AbstractValidator<AddClienteCommandInput>
{
    public AddClienteCommandValidator()
    {
        RuleFor(x => x.NomeEmpresa)
             .MaximumLength(250)
             .WithMessage("O nome da empresa possuir menos de 250 caracteres.");

        RuleFor(x => x.Porte)
            .Must(porte => Enum.IsDefined(typeof(PorteEmpresa), porte))
            .WithMessage("O valor fornecido para Porte está fora do intervalo permitido.");
    }
}