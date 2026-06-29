using Application.Features.Interfaces;
using Application.Models.Responses;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace Infrastructure.Queries;

public class ProyectoQueries : IProyectoQueries
{
    private readonly IDbConnection _dbConn;
    private readonly ILogger<ProyectoQueries> _logger;

    public ProyectoQueries(IDbConnection dbConn, ILogger<ProyectoQueries> logger)
    {
        _dbConn = dbConn;
        _logger = logger;
    }

    public async Task<ResponseData<IEnumerable<ProyectoResponse>>> ListarProyectos(CancellationToken cancellationToken)
    {
        try
        {
            var sb = new SqlBuilder();
            var template = sb.AddTemplate(
                @"SELECT 
                id               AS IdProyecto,
                nombre           AS NombreProyecto,
                descripcion      AS DescripcionProyecto,
                fecha_inicio::timestamp  AS FechaInicioProyecto
                FROM public.proyecto
                /**where**/
                /**orderby**/
                ");
            sb.OrderBy("id DESC");

            _dbConn.Open();
            var resultado = await _dbConn.QueryAsync<ProyectoResponse>(template.RawSql, template.Parameters);

            _logger.LogInformation($"ListarProyectos: Consulta ejecutada con éxito. Registros obtenidos: {resultado.Count()}");

            return new ResponseData<IEnumerable<ProyectoResponse>>
            {
                Exitoso = true,
                Resultado = resultado, 
                Descripcion = "Consulta realizada exitosamente."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ListarProyectos: Error crítico al consultar la base de datos.");
            return new ResponseData<IEnumerable<ProyectoResponse>>
            {
                Exitoso = false,
                Resultado = Enumerable.Empty<ProyectoResponse>(),
                Descripcion = "No se pudo procesar la solicitud de lectura debido a un inconveniente interno."
            };
        }
        finally
        {
            _dbConn.Close(); 
        }
    }
}