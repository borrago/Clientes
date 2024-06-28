using Clientes.Domain.ClientAggregate;
using FluentValidation;

namespace Clientes.Application.Commands.UpdateClienteCommand;

public class AtualizarClienteCommandValidator : AbstractValidator<UpdateClienteCommandInput>
{
    public AtualizarClienteCommandValidator()
    {
        RuleFor(x => x.NomeEmpresa)
             .MaximumLength(250)
             .WithMessage("O nome da empresa possuir menos de 250 caracteres.");

        RuleFor(x => x.Porte)
            .Must(porte => Enum.IsDefined(typeof(PorteEmpresa), porte))
            .WithMessage("O valor fornecido para Porte está fora do intervalo permitido.");
    }
}