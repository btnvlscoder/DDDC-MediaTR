using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Infrastructure.Commands;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApplication1.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioQueries _usuarioQueries;
    private readonly IUsuarioCommands _usuarioCommands;


    public UsuarioController(IUsuarioQueries usuarioQueries, IUsuarioCommands usuarioCommands)
    {
        _usuarioQueries = usuarioQueries;
        _usuarioCommands = usuarioCommands;

    }

    [HttpGet("ListarUsuarios")]
    public async Task<ActionResult<ResponseData<IEnumerable<UsuarioResponse>>>> GetUsuarios(CancellationToken cancellationToken)
    {
        var response = await _usuarioQueries.ListarUsuarios(cancellationToken);
        return Ok(response);
    }

    [HttpPost("AgregarUsuario")]
    public async Task<ActionResult<ResponseData<int>>> AgregarUsuario([FromBody] AgregarUsuarioRequest request, CancellationToken cancellationToken)
    {
        var response = await _usuarioCommands.AgregarUsuario(request, cancellationToken);

        if (response.Exitoso)
        {
            return Ok(response);
        }
        else
        {
            return BadRequest(response);
        }
    }
}
