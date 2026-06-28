using Application.Models;
using Application.Models.Responses;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Application.Models;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace WebApplication1.Middlewares;

public class HttpGlobalExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<HttpGlobalExceptionMiddleware> _logger;

    public HttpGlobalExceptionMiddleware(RequestDelegate next, ILogger<HttpGlobalExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context); // Pasa a la siguiente capa
        }
        catch (Exception error)
        {
            _logger.LogError(new EventId(error.HResult), error, error.Message);

            var response = context.Response;
            response.ContentType = "application/json";

            string message;
            object? errors = null;
            object? data = null;
            bool success = false;

            switch (error)
            {
                case ApiException e: // Error controlado (ej. "El RUT ya existe")
                    response.StatusCode = e.Code;
                    message = e.MessageApi;
                    errors = e.Errors;
                    data = e.Result;
                    success = e.Success;
                    break;
                default: // Error feo (ej. se cayó Postgres)
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    // En producción, NUNCA devuelvas el error.Message al cliente, pero lo dejaremos como tu profe
                    message = "Ha ocurrido un error, por favor comuníquese con el administrador del sistema. " + error.Message;
                    break;
            }

            var result = JsonSerializer.Serialize(new ResponseData<object>
            {
                Exitoso = success,
                Descripcion = message,
                Resultado = data,
                Errores = errors
            });

            await response.WriteAsync(result);
        }
    }
}