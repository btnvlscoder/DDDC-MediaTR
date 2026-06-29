using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IProyectoQueries
{
    Task<ResponseData<IEnumerable<ProyectoResponse>>> ListarProyectos(CancellationToken cancellationToken);
}
