using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clientes.Application.Subscribers;

public class AddedClienteSubscriber(
    ILogger<AddedClienteSubscriber> logger,
    IClienteMongoRepository clientesRepository
    ) : INotificationHandler<AddedClienteEventInput>
{
    private readonly IClienteMongoRepository _clientesRepository = clientesRepository ?? throw new ArgumentNullException(nameof(clientesRepository));
    private readonly ILogger<AddedClienteSubscriber> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    public async Task Handle(AddedClienteEventInput notification, CancellationToken cancellationToken)
    {
        try
        {
            var clienteReadModel = new ClienteModel
            {
                Id = notification.Id,
                NomeEmpresa = notification.NomeEmpresa,
                Porte = notification.Porte.ToString()
            };

            var success = await _clientesRepository.InsertAsync(clienteReadModel, cancellationToken);
            if (!success)
                throw new Exception("Erro ao inserir cliente no MongoDB.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao processar AddClienteEventInput.");
        }
    }
}