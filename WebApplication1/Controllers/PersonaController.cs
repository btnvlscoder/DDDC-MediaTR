using Application.Features.Interfaces;
using Application.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MiPracticaCasera.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PersonaController : ControllerBase
{
    private readonly IPersonaQueries _personaQueries;

    // Inyectamos el contrato
    public PersonaController(IPersonaQueries personaQueries)
    {
        _personaQueries = personaQueries;
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
}