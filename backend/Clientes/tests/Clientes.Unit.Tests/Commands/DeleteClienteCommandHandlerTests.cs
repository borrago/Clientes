using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.Commands.DeleteClienteCommand;
using Clientes.Application.Commands.UpdateClienteCommand;
using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Domain.Data;
using Clientes.Domain.DomainObjects;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Moq;
using System.Linq.Expressions;

namespace Clientes.Unit.Tests.Commands;

public class DeleteClienteCommandHandlerTests
{
    private readonly Mock<IClientesRepository> _clientesRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly DeleteClienteCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public DeleteClienteCommandHandlerTests()
    {
        _clientesRepositoryMock = new Mock<IClientesRepository>();
        _mediatorMock = new Mock<IMediator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _clientesRepositoryMock.Setup(r => r.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _handler = new DeleteClienteCommandHandler(_clientesRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateClienteAndPublishEvent()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var cliente = new Cliente("Empresa Grande", PorteEmpresa.Grande);
        var command = new DeleteClienteCommandInput(cliente.Id);

        var clientesQueryable = new[] { cliente }.AsQueryable();

        _clientesRepositoryMock
               .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Cliente, bool>>>(), It.IsAny<CancellationToken>()))
               .Returns((Expression<Func<Cliente, bool>> predicate, CancellationToken ct) =>
                   Task.FromResult(clientesQueryable.FirstOrDefault(predicate)));

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _clientesRepositoryMock.Verify(x => x.Remove(It.IsAny<Cliente>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
        _mediatorMock.Verify(x => x.Publish(It.IsAny<DeletedClienteEventInput>(), cancellationToken), Times.Once);
        Assert.True(result);
    }
}