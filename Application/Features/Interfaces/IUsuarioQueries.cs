using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IUsuarioQueries
{
    Task<ResponseData<IEnumerable<UsuarioResponse>>> ListarUsuarios(CancellationToken cancellationToken);

}
