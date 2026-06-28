using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Models;

public class ApiException: Exception
{
    public int Code { get; set; }
    public string MessageApi { get; set; }
    public object? Errors { get; set; }
    public object? Result { get; set; }
    public bool Success { get; set; }

    public ApiException(string message, int code = 400, object? errors = null) : base(message)
    {
        Code = code;
        MessageApi = message;
        Errors = errors;
        Success = false;
    }
}

