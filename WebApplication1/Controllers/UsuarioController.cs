using Application.Features.Interfaces;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioQueries _usuarioQueries;

    public UsuarioController(IUsuarioQueries usuarioQueries)
    {
        _usuarioQueries = usuarioQueries;
    }

    [HttpGet("ListarUsuarios")]
    public async Task<ActionResult<ResponseData<IEnumerable<UsuarioResponse>>>> GetUsuarios(CancellationToken cancellationToken)
    {
        var response = await _usuarioQueries.ListarUsuarios(cancellationToken);
        return Ok(response);
    }
}
