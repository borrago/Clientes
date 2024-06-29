using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.Commands.UpdateClienteCommand;
using Clientes.Application.Events;
using Clientes.Domain.ClientAggregate;
using Clientes.Domain.Data;
using Clientes.Domain.DomainObjects;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Moq;
using System.Linq.Expressions;

namespace Clientes.Unit.Tests.Commands;

public class UpdateClienteCommandHandlerTests
{
    private readonly Mock<IClientesRepository> _clientesRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UpdateClienteCommandHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public UpdateClienteCommandHandlerTests()
    {
        _clientesRepositoryMock = new Mock<IClientesRepository>();
        _mediatorMock = new Mock<IMediator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _clientesRepositoryMock.Setup(r => r.UnitOfWork).Returns(_unitOfWorkMock.Object);
        _handler = new UpdateClienteCommandHandler(_clientesRepositoryMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldUpdateClienteAndPublishEvent()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var cliente = new Cliente("Empresa Grande", PorteEmpresa.Grande);
        var command = new UpdateClienteCommandInput(cliente.Id, "Empresa Média", PorteEmpresa.Media);

        var clientesQueryable = new[] { cliente }.AsQueryable();

        _clientesRepositoryMock
               .Setup(r => r.GetAsync(It.IsAny<Expression<Func<Cliente, bool>>>(), It.IsAny<CancellationToken>()))
               .Returns((Expression<Func<Cliente, bool>> predicate, CancellationToken ct) =>
                   Task.FromResult(clientesQueryable.FirstOrDefault(predicate)));

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        _clientesRepositoryMock.Verify(x => x.Update(It.IsAny<Cliente>()), Times.Once);
        _unitOfWorkMock.Verify(x => x.CommitAsync(cancellationToken), Times.Once);
        _mediatorMock.Verify(x => x.Publish(It.IsAny<UpdatedClienteEventInput>(), cancellationToken), Times.Once);
        Assert.True(result);
    }
}