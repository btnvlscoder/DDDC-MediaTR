using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Commands;

public class PersonaCommands : IPersonaCommands{

    private readonly MiDbContext _context;

    public PersonaCommands(MiDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseData<int>> AgregarPersona(CrearPersonaRequest request, CancellationToken cancellationToken)
    {
        try
        {

            // Crear una nueva instancia de Persona con los datos del request
            var nuevaPersona = new Persona
            {
                UsuarioId = request.UsuarioId,
                NombrePersona = request.NombrePersona,
                ApellidoPersona = request.ApellidoPersona,
                RutPersona = request.RutPersona,
                TelefonoPersona = request.TelefonoPersona,
                FechaNacimientoPersona = request.FechaNacimientoPersona.ToUniversalTime()
            };

            // Preparar la respuesta
            _context.Personas.Add(nuevaPersona); ;

            // Guardar los cambios en la base de datos
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseData<int>
            {
                Exitoso = true,
                Descripcion = "Persona creada exitosamente en la data base",
                Resultado = nuevaPersona.IdPersona,
            };
        }
        catch (Exception ex){
            return new ResponseData<int>
            {
                Exitoso = false,
                Descripcion = $"Error al crear la persona: {ex.Message}",
            };
        }   
    }

}
