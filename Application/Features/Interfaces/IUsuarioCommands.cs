using Application.Models.Requests;
using Application.Models.Responses;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Features.Interfaces;

public interface IUsuarioCommands
{
    Task<ResponseData<int>> AgregarUsuario(AgregarUsuarioRequest request, CancellationToken cancellationToken);

}
