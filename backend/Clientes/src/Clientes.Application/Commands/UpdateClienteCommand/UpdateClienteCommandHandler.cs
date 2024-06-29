using Clientes.Application.Events;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Application.Commands.UpdateClienteCommand;

public class UpdateClienteCommandHandler(
    IClientesRepository clientesRepository,
    IMediator mediator) : IRequestHandler<UpdateClienteCommandInput, bool>
{

    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IClientesRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));

    public async Task<bool> Handle(UpdateClienteCommandInput request, CancellationToken cancellationToken)
    {
        var cliente = await _clientesRepository.GetAsync(g => g.Id == request.Id, cancellationToken);

        if (cliente is null)
            return false;

        cliente.WithNomeEmpresa(request.NomeEmpresa);
        cliente.WithPorteEmpresa(request.Porte);

        _clientesRepository.Update(cliente);
        await _clientesRepository.UnitOfWork.CommitAsync(cancellationToken);

        await _mediator.Publish(new UpdatedClienteEventInput(cliente.Id, cliente.NomeEmpresa, cliente.Porte), cancellationToken);

        return true;
    }
}