using Microsoft.EntityFrameworkCore;
using Serilog;
using SistemaPedidos.Application.Profiles;
using SistemaPedidos.Domain.Repositories;
using SistemaPedidos.Infrastructure.Data;
using SistemaPedidos.Infrastructure.Repositories;

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

// Registro dos repositórios
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
builder.Services.AddScoped<IPedidoRepository, PedidoRepository>();

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
