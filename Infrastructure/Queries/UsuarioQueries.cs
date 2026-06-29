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
            _dbConn.Close();

            if (resultado.Any())
            {
                _logger.LogInformation($"ListarUsuarios: Se encontraron {resultado.Count()}resultaods");
                return new ResponseData<IEnumerable<UsuarioResponse>>
                {
                    Descripcion = "Ok",
                    Resultado = resultado,
                    Exitoso = true
                };
            }
            else
            {
                _logger.LogWarning("ListarUsuarios: sin resultados");
                return new ResponseData<IEnumerable<UsuarioResponse>>
                {
                    Descripcion = "No hay registros asociados",
                    Resultado = Enumerable.Empty<UsuarioResponse>(),
                    Exitoso = false
                };
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ListarUsuarios: Error inesperado");
            return new ResponseData<IEnumerable<UsuarioResponse>>
            {
                Descripcion = "ListarUsuarios: Error inesperado",
                Resultado = null,
                Exitoso = false
            };
        }
    }
}
