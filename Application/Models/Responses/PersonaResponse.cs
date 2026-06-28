using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Responses;

public class PersonaResponse{
    public int IdPersona { set; get; }
    public string NombrePersona { set; get; }
    public string ApellidoPersona { set; get; }
    public string RutPersona { set; get; }
    public string TelefonoPersona { set; get; }
    public DateTime FechaNacimientoPersona { set; get; }
}
