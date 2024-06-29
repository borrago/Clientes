using Clientes.Application.Events;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clientes.Application.Commands.DeleteClienteCommand;

public class DeleteClienteCommandHandler(IClientesRepository clientesRepository, IMediator mediator) : IRequestHandler<DeleteClienteCommandInput, bool>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IClientesRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));

    public async Task<bool> Handle(DeleteClienteCommandInput request, CancellationToken cancellationToken)
    {
        var cliente = await _clientesRepository.GetAsync(g => g.Id == request.Id, cancellationToken);

        if (cliente is null)
            return false;

        _clientesRepository.Remove(cliente);
        await _clientesRepository.UnitOfWork.CommitAsync(cancellationToken);

        await _mediator.Publish(new DeletedClienteEventInput(cliente.Id), cancellationToken);

        return true;
    }
}