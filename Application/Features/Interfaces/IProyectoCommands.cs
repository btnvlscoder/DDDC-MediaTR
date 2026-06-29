using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IProyectoCommands
{
    Task<ResponseData<int>> AgregarProyecto(CrearProyectoRequest request, CancellationToken cancellationToken);

    Task<ResponseData<int>> ActualizarProyecto(ActualizarProyectoRequest request, CancellationToken cancellationToken);
}
