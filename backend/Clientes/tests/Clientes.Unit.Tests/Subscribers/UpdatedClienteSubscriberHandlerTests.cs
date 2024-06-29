using Clientes.Application.Events;
using Clientes.Application.Subscribers;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using Microsoft.Extensions.Logging;
using Moq;

namespace Clientes.Unit.Tests.Subscribers;

public class UpdatedClienteSubscriberHandlerTests
{
    private readonly Mock<IClienteProjectionRepository> _clientesRepository;
    private readonly Mock<ILogger<UpdatedClienteSubscriber>> _loggerMock;
    private readonly UpdatedClienteSubscriber _handler;

    public UpdatedClienteSubscriberHandlerTests()
    {
        _clientesRepository = new Mock<IClienteProjectionRepository>();
        _loggerMock = new Mock<ILogger<UpdatedClienteSubscriber>>();
        _handler = new UpdatedClienteSubscriber(_loggerMock.Object, _clientesRepository.Object);
    }

    [Fact]
    public async Task Handle_ShouldInsertClienteClienteModel()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var clienteEvent = new UpdatedClienteEventInput(Guid.NewGuid(), "Empresa Pequena", PorteEmpresa.Pequena);
        var clienteModel = new ClienteModel { Id = clienteEvent.Id, NomeEmpresa = clienteEvent.NomeEmpresa, Porte = clienteEvent.Porte.ToString() };

        _clientesRepository
            .Setup(r => r.InsertAsync(It.IsAny<ClienteModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

        // Act
        await _handler.Handle(clienteEvent, cancellationToken);

        // Assert
        _clientesRepository.Verify(r => r.UpdateAsync(It.Is<ClienteModel>(c =>
            c.Id == clienteModel.Id && c.NomeEmpresa == clienteModel.NomeEmpresa && c.Porte == clienteModel.Porte),
            cancellationToken), Times.Once);
    }
}
