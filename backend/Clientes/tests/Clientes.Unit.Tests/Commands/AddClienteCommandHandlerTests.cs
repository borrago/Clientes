using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Domain.Data;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Moq;

namespace Clientes.Unit.Tests.Commands
{
    public class AddClienteCommandHandlerTests
    {
        private readonly Mock<IClientesRepository> _clientesRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly AddClienteCommandHandler _handler;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;

        public AddClienteCommandHandlerTests()
        {
            _clientesRepositoryMock = new Mock<IClientesRepository>();
            _mediatorMock = new Mock<IMediator>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _clientesRepositoryMock.Setup(r => r.UnitOfWork).Returns(_unitOfWorkMock.Object);
            _handler = new AddClienteCommandHandler(_clientesRepositoryMock.Object, _mediatorMock.Object);
        }

        [Fact]
        public async Task Handle_ShouldAddClienteAndPublishEvent()
        {
            // Arrange
            var cancellationToken = new CancellationToken();
            var command = new AddClienteCommandInput("Empresa Média", PorteEmpresa.Media);

            // Act
            await _handler.Handle(command, cancellationToken);

            // Assert
            _clientesRepositoryMock.Verify(x => x.AddAsync(It.IsAny<Cliente>(), cancellationToken), Times.Once);
            _clientesRepositoryMock.Verify(x => x.UnitOfWork.CommitAsync(cancellationToken), Times.Once);
            _mediatorMock.Verify(x => x.Publish(It.IsAny<AddedClienteEventInput>(), cancellationToken), Times.Once);
        }
    }
}
