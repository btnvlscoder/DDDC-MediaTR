using Application.Features.Interfaces;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProyectoController : ControllerBase
{
    private readonly IProyectoQueries _proyectoQueries;

    public ProyectoController(IProyectoQueries proyectoQueries)
    {
        _proyectoQueries = proyectoQueries;
    }

    [HttpGet("ListarProyectos")]
    public async Task<ActionResult<ResponseData<IEnumerable<ProyectoResponse>>>> ListarProyectos(CancellationToken cancellationToken)
    {
        var response = await _proyectoQueries.ListarProyectos(cancellationToken);
        return Ok(response);
    }
}