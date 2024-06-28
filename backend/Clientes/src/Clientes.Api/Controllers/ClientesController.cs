using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.Commands.DeleteClienteCommand;
using Clientes.Application.Commands.UpdateClienteCommand;
using Clientes.Application.Queries.GetClienteByIdQuery;
using Clientes.Application.Queries.GetClientesQuery;
using Clientes.Domain.ClientAggregate;
using Clientes.Infra.Repositories.ClientesRepository;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Clientes.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));


    [HttpGet]
    public async Task<ActionResult<List<ClienteModel>>> GetClientes(CancellationToken cancellationToken)
        => Ok(await _mediator.Send(new GetClientesQueryInput(), cancellationToken));

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ClienteModel>> GetCliente([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var cliente = await _mediator.Send(new GetClienteByIdQueryInput(id), cancellationToken);

        if (cliente == null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateCliente([FromBody] AddClienteCommandInput command, CancellationToken cancellationToken)
    {
        var clienteId = await _mediator.Send(command, cancellationToken);

        if (clienteId == Guid.Empty)
            return BadRequest($"Não foi possível criar o cliente.");

        return CreatedAtAction(nameof(GetCliente), new { id = clienteId }, clienteId);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateCliente([FromRoute] Guid id, [FromBody] AddClienteCommandInput request, CancellationToken cancellationToken)
    {
        var command = new UpdateClienteCommandInput(id, request.NomeEmpresa, request.Porte);
        var atualizado = await _mediator.Send(command, cancellationToken);

        if (!atualizado)
            return BadRequest($"Não foi possível atualizar o cliente de id: {command.Id}.");

        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteCliente([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var removido = await _mediator.Send(new DeleteClienteCommandInput(id), cancellationToken);

        if (!removido)
            return BadRequest($"Não foi possível remover o cliente de id: {id}.");

        return NoContent();
    }
}