using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IPersonaQueries
{
    Task<ResponseData<IEnumerable<PersonaResponse>>> ListarPersonas(CancellationToken cancellationToken);
}
