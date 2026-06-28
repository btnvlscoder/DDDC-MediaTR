using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities;

public class Persona
{
    public int IdPersona { set; get; }
    public string NombrePersona { set; get; }
    public string ApellidoPersona { set; get; }
    public string RutPersona { set; get; }
    public int TelefonoPersona { set; get; }

    public DateTime FechaNacimientoPersona { set; get; }



}
