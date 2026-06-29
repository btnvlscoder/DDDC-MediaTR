using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoQueries _proyectoQueries;
    private readonly IProyectoCommands _proyectoCommands; 
    public ProyectoController(IProyectoQueries proyectoQueries, IProyectoCommands proyectoCommands)
    {
        _proyectoQueries = proyectoQueries;
        _proyectoCommands = proyectoCommands;
    }

    [HttpGet("ListarProyectos")]
    public async Task<ActionResult<ResponseData<IEnumerable<ProyectoResponse>>>> ListarProyectos(CancellationToken cancellationToken)
    {
        var response = await _proyectoQueries.ListarProyectos(cancellationToken);
        return Ok(response);
    }

    [HttpPost("CrearProyecto")]
    public async Task<ActionResult<ResponseData<int>>> CrearProyecto([FromBody] CrearProyectoRequest request, CancellationToken cancellationToken)
    {
        var response = await _proyectoCommands.AgregarProyecto(request, cancellationToken);
        if (response.Exitoso) return Ok(response);
        return BadRequest(response);
    }

    [HttpPut("ActualizarProyecto")]
    public async Task<ActionResult<ResponseData<int>>> ActualizarProyecto([FromBody] ActualizarProyectoRequest request, CancellationToken cancellationToken)
    {
        var response = await _proyectoCommands.ActualizarProyecto(request, cancellationToken);

        if (response.Exitoso)
        {
            return Ok(response);
        }
        return BadRequest(response);
    }
}