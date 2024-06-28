using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;

namespace Clientes.Application.Commands.AddClienteCommand;

public class AddClienteCommandHandler(IClientesRepository clientesRepository, IMediator mediator) : IRequestHandler<AddClienteCommandInput, Guid>
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly IClientesRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));

    public async Task<Guid> Handle(AddClienteCommandInput request, CancellationToken cancellationToken)
    {
        var cliente = new Cliente(request.NomeEmpresa, request.Porte);
        await _clientesRepository.AddAsync(cliente, cancellationToken);
        await _clientesRepository.UnitOfWork.CommitAsync(cancellationToken);

        await _mediator.Publish(new AddedClienteEventInput(cliente.Id, cliente.NomeEmpresa, cliente.Porte), cancellationToken);

        return cliente.Id;
    }
}