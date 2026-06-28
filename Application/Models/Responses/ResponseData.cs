using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models.Responses;

public class ResponseData<T>{
    public bool Exitoso { get; set; }
    public string Descripcion { get; set; } = string.Empty;
    public T? Resultado { get; set; }// atributo comodin para meter cualquier DTO
    public object? Errores { get; set; } //para el middleware
}
