using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using Application.Features.Interfaces;
using Application.Models.Responses;

namespace Infrastructure.Queries;

public class PersonaQueries:IPersonaQueries {
    private readonly IDbConnection _dbConn;
    private readonly ILogger<PersonaQueries> _logger;

    public PersonaQueries(IDbConnection dbConn, ILogger<PersonaQueries> logger){
        _dbConn = dbConn;
        _logger = logger;
    }

    public async Task<ResponseData<IEnumerable<PersonaResponse>>> ListarPersonas(CancellationToken cancellationToken)
    {
        try{
            var sb = new SqlBuilder();
            var template = sb.AddTemplate(
                @"SELECT 
                id               AS IdPersona,
                usuario_id       AS UsuarioId,
                nombre           AS NombrePersona,
                apellido         AS ApellidoPersona,
                rut              AS RutPersona,
                telefono         AS TelefonoPersona,
                fecha_nacimiento::timestamp AS FechaNacimientoPersona
                FROM public.persona 
                /**where**/
                /**orderby**/
                ");

            sb.OrderBy("id ASC");

            _dbConn.Open();
            var resultado = await _dbConn.QueryAsync<PersonaResponse>(template.RawSql, template.Parameters);

            _logger.LogInformation($"ListarPersonas: Consulta ejecutada con éxito. Registros obtenidos: {resultado.Count()}");

            return new ResponseData<IEnumerable<PersonaResponse>>
            {
                Exitoso = true,
                Resultado = resultado,
                Descripcion = "Consulta realizada exitosamente."
            };
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "ListarPersonas: Error crítico al consultar la base de datos.");
            return new ResponseData<IEnumerable<PersonaResponse>>
            {
                Exitoso = false,
                Resultado = Enumerable.Empty<PersonaResponse>(),
                Descripcion = "No se pudo procesar la solicitud de lectura debido a un inconveniente interno."
            };
        }
        finally
        {
            _dbConn.Close();
        }
    }
}

