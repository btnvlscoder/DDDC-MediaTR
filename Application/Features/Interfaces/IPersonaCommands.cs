using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IPersonaCommands
{
    Task<ResponseData<int>> AgregarPersona(
        CrearPersonaRequest request, CancellationToken cancellationToken
        );
}
