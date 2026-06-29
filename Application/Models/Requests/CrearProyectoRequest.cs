using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Requests;

public class CrearProyectoRequest
{
    public string NombreProyecto { get; set; }
    public string DescripcionProyecto { get; set; }
    public DateTime FechaInicioProyecto { get; set; }
}
