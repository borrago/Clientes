using Clientes.Api.Middlewares;
using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.FailFastPipeLineBehaviors;
using Clientes.Infra.Contexts;
using Clientes.Infra.Repositories.ClientesRepository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cliente API", Version = "v1" });
});


// Configuração do DbContext para SQL Server
builder.Services.AddDbContext<SqlServerDbContext>(options =>
    options.UseSqlServer(builder.Configuration["ConnectionStrings:SqlServerConn"]));

builder.Services.AddSingleton<IMongoClient>(sp =>
    new MongoClient(builder.Configuration["MongoDbSettings:MongoDbConn"]));
builder.Services.AddSingleton<MongoDbContext>();

builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    var databaseName = builder.Configuration["MongoDbSettings:DatabaseName"];
    return client.GetDatabase(databaseName);
});

// Configuração dos repositórios
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IClienteProjectionRepository, ClienteProjectionRepository>();

// Configuração do MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddClienteCommandHandler).Assembly);
});

// Validações com fluent
builder.Services.AddValidatorsFromAssemblyContaining<AddClienteCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastPipeLineBehaviors<,>));

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

await app.RunAsync(new CancellationToken());
