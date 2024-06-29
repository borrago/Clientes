using Clientes.Application.Events;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clientes.Application.Subscribers;

public class DeletedClienteSubscriber(
    ILogger<DeletedClienteSubscriber> logger,
    IClienteMongoRepository clientesRepository
    ) : INotificationHandler<DeletedClienteEventInput>
{
    private readonly IClienteMongoRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));
    private readonly ILogger<DeletedClienteSubscriber> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(DeletedClienteEventInput notification, CancellationToken cancellationToken)
    {
        try
        {
            var success = await _clientesRepository.RemoveAsync(notification.Id, cancellationToken);
            if (!success)
                throw new Exception("Erro ao remover cliente no MongoDB.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar DeletedClienteEventInput.");
        }
    }
}