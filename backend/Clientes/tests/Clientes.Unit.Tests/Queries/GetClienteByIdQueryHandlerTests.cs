using Clientes.Application.Queries.GetClienteByIdQuery;
using Clientes.Domain.ClientAggregate;
using Clientes.Domain.Data;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Moq;

namespace Clientes.Unit.Tests.Queries;

public class GetClienteByIdQueryHandlerTests
{
    private readonly Mock<IClienteProjectionRepository> _clientesRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly GetClienteByIdQueryHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GetClienteByIdQueryHandlerTests()
    {
        _clientesRepositoryMock = new Mock<IClienteProjectionRepository>();
        _mediatorMock = new Mock<IMediator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetClienteByIdQueryHandler(_clientesRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCliente()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var cliente = new ClienteModel { Id = Guid.NewGuid(), NomeEmpresa = "Empresa Pequena", Porte = "Pequena" };
        var query = new GetClienteByIdQueryInput(cliente.Id);

        _clientesRepositoryMock
                .Setup(r => r.GetByIdAsync(cliente.Id, cancellationToken))
                .Returns(Task.FromResult(cliente));

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(cliente, result);
    }
}