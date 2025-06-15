using Microsoft.EntityFrameworkCore;
using Serilog;
using SistemaPedidos.Application.Profiles;
using SistemaPedidos.Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

// Configura Serilog
builder.Host.UseSerilog((context, services, configuration) =>
{
    configuration
        .ReadFrom.Configuration(context.Configuration)
        .ReadFrom.Services(services)
        .Enrich.FromLogContext()
        .WriteTo.Console();
});

// Configura o DbContext com PostgreSQL
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar AutoMapper, apontando para a assembly onde estão os Profiles
builder.Services.AddAutoMapper(typeof(ClienteProfile).Assembly);

// Configura Swagger (para testes)
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers();

var app = builder.Build();

// Swagger sempre ativo
app.UseSwagger();
app.UseSwaggerUI();

app.MapControllers();

app.Run();
