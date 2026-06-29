using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Requests;

public class AgregarUsuarioRequest
{
    public string NombreUsuario { set; get; }
    public string EmailUsuario { set; get; }
    public DateTime FechaRegistroUsuario { set; get; }
}
