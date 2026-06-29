using Application.Features.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
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
    //add
    public async Task<ResponseData<int>> AgregarProyecto(CrearProyectoRequest request, CancellationToken cancellationToken)
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

    //update
    public async Task<ResponseData<int>> ActualizarProyecto(ActualizarProyectoRequest request, CancellationToken cancellationToken)
    {
        try
        {
            // 1. Buscamos el proyecto en la base de datos
            var proyectoExistente = await _context.Proyecto
                .FirstOrDefaultAsync(p => p.IdProyecto == request.IdProyecto, cancellationToken);

            // 2. REGLA DE NEGOCIO: ¿Qué pasa si me mandan un ID que no existe?
            // Lo atrapamos elegantemente sin que explote el servidor.
            if (proyectoExistente == null)
            {
                return new ResponseData<int>
                {
                    Exitoso = false,
                    Resultado = 0,
                    Descripcion = $"No se encontró ningún proyecto con el ID {request.IdProyecto}."
                };
            }

            // 3. Si lo encontró, EF Core lo está "vigilando". Solo sobreescribimos los datos con los del Request.
            proyectoExistente.NombreProyecto = request.NombreProyecto;
            proyectoExistente.DescripcionProyecto = request.DescripcionProyecto;

            // ToUniversalTime() para que Postgres no joda
            proyectoExistente.FechaInicioProyecto = request.FechaInicioProyecto.ToUniversalTime();

            // 4. Mandamos el save. EF Core armará el UPDATE automático.
            await _context.SaveChangesAsync(cancellationToken);

            return new ResponseData<int>
            {
                Exitoso = true,
                Resultado = proyectoExistente.IdProyecto,
                Descripcion = "Proyecto actualizado exitosamente."
            };
        }
        catch (Exception ex)
        {
            return new ResponseData<int>
            {
                Exitoso = false,
                Descripcion = $"Error crítico al actualizar el proyecto: {ex.Message}"
            };
        }
    }

}