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
public class PersonaController : ControllerBase
{
    private readonly IPersonaQueries _personaQueries;
    private readonly IPersonaCommands _personaCommands;

    // Inyectamos el contrato
    public PersonaController(IPersonaQueries personaQueries, IPersonaCommands personaCommands)
    {
        _personaQueries = personaQueries;
        _personaCommands = personaCommands;
    }

    // Definimos la ruta del endpoint: GET api/Persona/ListarPersonas
    [HttpGet("ListarPersonas")]
    public async Task<ActionResult<ResponseData<IEnumerable<PersonaResponse>>>> GetPersonas(CancellationToken cancellationToken)
    {
        // Llamamos al caso de uso de la capa de Aplicación
        var response = await _personaQueries.ListarPersonas(cancellationToken);
        // Retornamos HTTP 200 (OK) con el JSON envuelto en el ResponseData
        return Ok(response);
    }

    [HttpPost("AgregarPersona")]
    public async Task<ActionResult<ResponseData<int>>> CrearPersona([FromBody] CrearPersonaRequest request, CancellationToken cancellationToken)
    {
        // Llamamos al motor de escritura (Entity Framework)
        var response = await _personaCommands.AgregarPersona(request, cancellationToken);

        if (response.Exitoso)
        {
            return Ok(response);
        }
        else
        {
            // Si hubo un error (ej. llave foránea mala), devolvemos un 400 Bad Request
            return BadRequest(response);
        }
    }
}