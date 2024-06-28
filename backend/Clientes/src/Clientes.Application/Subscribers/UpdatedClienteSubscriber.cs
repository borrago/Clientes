using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clientes.Application.Subscribers;

public class UpdatedClienteSubscriber(
    ILogger<UpdatedClienteSubscriber> logger,
    IClienteProjectionRepository clientesRepository
    ) : INotificationHandler<UpdatedClienteEventInput>
{
    private readonly IClienteProjectionRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));
    private readonly ILogger<UpdatedClienteSubscriber> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(UpdatedClienteEventInput notification, CancellationToken cancellationToken)
    {
        try
        {
            var clienteReadModel = new ClienteModel
            {
                Id = notification.Id,
                NomeEmpresa = notification.NomeEmpresa,
                Porte = notification.Porte.ToString()
            };

            var success = await _clientesRepository.UpdateAsync(clienteReadModel, cancellationToken);
            if (!success)
                throw new Exception("Erro ao atualizar cliente no MongoDB.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar UpdatedClienteEventInput.");
        }
    }
}