using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Requests;

public class ActualizarProyectoRequest
{
    public int IdProyecto { get; set; }
    public string NombreProyecto { get; set; }
    public string DescripcionProyecto { get; set; }
    public DateTime FechaInicioProyecto { get; set; }
}
