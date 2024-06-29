using Clientes.Application.Queries.GetClientesQuery;
using Clientes.Domain.ClientAggregate;
using Clientes.Domain.Data;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Moq;

namespace Clientes.Unit.Tests.Queries;

public class GetClienteQueryHandlerTests
{
    private readonly Mock<IClienteProjectionRepository> _clientesRepositoryMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly GetClientesQueryHandler _handler;
    private readonly Mock<IUnitOfWork> _unitOfWorkMock;

    public GetClienteQueryHandlerTests()
    {
        _clientesRepositoryMock = new Mock<IClienteProjectionRepository>();
        _mediatorMock = new Mock<IMediator>();
        _unitOfWorkMock = new Mock<IUnitOfWork>();
        _handler = new GetClientesQueryHandler(_clientesRepositoryMock.Object);
    }

    [Fact]
    public async Task Handle_ShouldReturnCliente()
    {
        // Arrange
        var cancellationToken = new CancellationToken();
        var clientes = new List<ClienteModel>
            {
                new() { Id = Guid.NewGuid(), NomeEmpresa = "Empresa Pequena", Porte = "Pequena" },
                new() { Id = Guid.NewGuid(), NomeEmpresa = "Empresa Média", Porte = "Media" }
            };
        var query = new GetClientesQueryInput();

        _clientesRepositoryMock
                .Setup(r => r.GetAllAsync(cancellationToken))
                .Returns(Task.FromResult(clientes));

        // Act
        var result = await _handler.Handle(query, cancellationToken);

        // Assert
        Assert.Equal(clientes, result);
    }
}