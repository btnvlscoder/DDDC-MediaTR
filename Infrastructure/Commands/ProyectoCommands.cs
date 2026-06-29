using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Commands;

public class ProyectoCommands : IProyectoCommands
{
    private readonly MiDbContext _context;

    public ProyectoCommands(MiDbContext context)
    {
        _context = context;
    }

    public async Task<ResponseData<int>> CrearProyecto(CrearProyectoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var nuevoProyecto = new Proyecto
            {
                NombreProyecto = request.NombreProyecto,
                DescripcionProyecto = request.DescripcionProyecto,
                // Evitamos el error de zona horaria (Kind=Unspecified)
                FechaInicioProyecto = request.FechaInicioProyecto.ToUniversalTime()
            };

            _context.Proyecto.Add(nuevoProyecto);
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseData<int>
            {
                Exitoso = true,
                Resultado = nuevoProyecto.IdProyecto,
                Descripcion = "Proyecto creado exitosamente."
            };
        }
        catch (Exception ex)
        {
            return new ResponseData<int>
            {
                Exitoso = false,
                Descripcion = $"Error al crear el proyecto: {ex.Message}"
            };
        }
    }
}