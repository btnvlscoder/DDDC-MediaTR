using Npgsql; 
using System.Data; 
using Microsoft.EntityFrameworkCore;
using Infrastructure.Repositories;
using Application.Features.Interfaces;
using Infrastructure.Queries;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("PostgresConnection");

builder.Services.AddDbContext<MiDbContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddTransient<IDbConnection>(sp => new NpgsqlConnection(connectionString));

builder.Services.AddScoped<IPersonaQueries, PersonaQueries>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();   
    app.UseSwaggerUI();
}
app.UseMiddleware<WebApplication1.Middlewares.HttpGlobalExceptionMiddleware>();
//app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
