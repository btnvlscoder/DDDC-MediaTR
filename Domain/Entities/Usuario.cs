using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class Usuario{
    public int IdUsuario { set; get; }
    public string NombreUsuario { set; get; }
    public string EmailUsuario { set; get; }
    public string FechaRegistroUsuario { set; get; }

}
