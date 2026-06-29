using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Commands;

public class PersonaCommands : IPersonaCommands
{

    private readonly MiDbContext _context;

    public PersonaCommands(MiDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseData<int>> AgregarPersona(CrearPersonaRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var rutDuplicado = await _context.Personas
            .AnyAsync(p => p.RutPersona == request.RutPersona, cancellationToken);

            if (rutDuplicado)
            {
                // Detenemos el flujo elegantemente con un mensaje amigable
                return new ResponseData<int>
                {
                    Exitoso = false,
                    Resultado = 0,
                    Descripcion = $"El RUT {request.RutPersona} ya se encuentra registrado en el sistema."
                };
            }
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

            _context.Personas.Add(nuevaPersona);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseData<int>
            {
                Exitoso = true,
                Resultado = nuevaPersona.IdPersona,
                Descripcion = "Persona creada exitosamente."
            };
        }
        catch (Exception ex)
        {
            return new ResponseData<int>
            {
                Exitoso = false,
                Descripcion = $"Error inesperado al crear la persona: {ex.Message}"
            };
        }
    }
}