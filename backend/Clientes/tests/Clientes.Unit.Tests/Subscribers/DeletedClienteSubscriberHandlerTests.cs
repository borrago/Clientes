using Clientes.Application.Events;
using Clientes.Application.Subscribers;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clientes.Unit.Tests.Subscribers;

public class DeletedClienteSubscriberHandlerTests
{
    private readonly Mock<IClienteProjectionRepository> _clientesRepository;
    private readonly Mock<ILogger<DeletedClienteSubscriber>> _loggerMock;
    private readonly DeletedClienteSubscriber _handler;

    public DeletedClienteSubscriberHandlerTests()
    {
        _clientesRepository = new Mock<IClienteProjectionRepository>();
        _loggerMock = new Mock<ILogger<DeletedClienteSubscriber>>();
        _handler = new DeletedClienteSubscriber(_loggerMock.Object, _clientesRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertClienteClienteModel()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var clienteEvent = new DeletedClienteEventInput(Guid.NewGuid());

        _clientesRepository
            .Setup(r => r.InsertAsync(It.IsAny<ClienteModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await _handler.Handle(clienteEvent, cancellationToken);

        // Assert
        _clientesRepository.Verify(r => r.RemoveAsync(clienteEvent.Id, cancellationToken), Times.Once);
    }
}
