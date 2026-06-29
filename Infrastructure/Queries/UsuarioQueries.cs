using Application.Features.Interfaces;
using Application.Models.Responses;
using Dapper;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Infrastructure.Queries;

public class UsuarioQueries:IUsuarioQueries {
    private readonly IDbConnection _dbConn;
    private readonly ILogger<UsuarioQueries> _logger;

    public UsuarioQueries(IDbConnection dbConn, ILogger<UsuarioQueries> logger)
    {
        _dbConn = dbConn;
        _logger = logger;
    }

    public async Task<ResponseData<IEnumerable<UsuarioResponse>>> ListarUsuarios(CancellationToken cancellationToken)
    {
        try
        {
            var sb = new SqlBuilder();
            var template = sb.AddTemplate(
                @"SELECT 
                id               AS IdUsuario,
                nombre           AS NombreUsuario,
                email              AS EmailUsuario,
                fecha_registro::timestamp  AS FechaRegistroUsuario
                FROM public.usuario
                /**where**/
                /**orderby**/
                ");

            sb.OrderBy("id DESC");

            _dbConn.Open();
            var resultado = await _dbConn.QueryAsync<UsuarioResponse>(template.RawSql, template.Parameters);

            _logger.LogInformation($"ListarUsuarios: Consulta ejecutada con éxito. Registros obtenidos: {resultado.Count()}");

            return new ResponseData<IEnumerable<UsuarioResponse>>
            {
                Exitoso = true,
                Resultado = resultado,
                Descripcion = "Consulta realizada exitosamente."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ListarUsuarios: Error crítico al consultar la base de datos.");
            return new ResponseData<IEnumerable<UsuarioResponse>>
            {
                Exitoso = false,
                Resultado = Enumerable.Empty<UsuarioResponse>(),
                Descripcion = "No se pudo procesar la solicitud de lectura debido a un inconveniente interno."
            };
        }
        finally
        {
            _dbConn.Close();
        }
    }
}
