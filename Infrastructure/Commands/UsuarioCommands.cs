using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Commands;

public class UsuarioCommands : IUsuarioCommands
{
    private readonly MiDbContext _context;

    public UsuarioCommands(MiDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseData<int>> AgregarUsuario(AgregarUsuarioRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var nuevoUsuario = new Usuario
            {
                NombreUsuario = request.NombreUsuario,
                EmailUsuario = request.EmailUsuario,
                FechaRegistroUsuario = request.FechaRegistroUsuario.ToUniversalTime()
            };

            _context.Usuarios.Add(nuevoUsuario);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseData<int>
            {
                Exitoso = true,
                Descripcion = "Usuario agregado exitosamente.",
                Resultado = nuevoUsuario.IdUsuario
            };
        }
        catch (Exception ex)
        {
            return new ResponseData<int>
            {
                Exitoso = false,
                Descripcion = $"Error al agregar el usuario: {ex.Message}",
                Resultado = 0,
                Errores = ex.Message
            };
        }
    }
}
