using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Responses;

public class UsuarioResponse
{
    public int IdUsuario { set; get; }
    public string NombreUsuario { set; get; }
    public string EmailUsuario { set; get; }
}
