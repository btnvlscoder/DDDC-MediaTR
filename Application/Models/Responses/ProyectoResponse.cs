using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Responses;

public class ProyectoResponse
{
    public int IdProyecto { set; get; }
    public string NombreProyecto { set; get; }
    public string DescripcionProyecto { set; get; }
    public DateTime FechaInicioProyecto { set; get; }
}
