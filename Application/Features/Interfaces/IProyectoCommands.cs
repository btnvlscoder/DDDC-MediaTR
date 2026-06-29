using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IProyectoCommands
{
    Task<ResponseData<int>> CrearProyecto(CrearProyectoRequest request, CancellationToken cancellationToken);
}
