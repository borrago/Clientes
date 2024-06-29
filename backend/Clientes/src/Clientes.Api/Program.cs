using Clientes.Api.Middlewares;
using Clientes.Application.Commands.AddClienteCommand;
using Clientes.Application.FailFastPipeLineBehaviors;
using Clientes.Infra.Contexts;
using Clientes.Infra.Repositories.ClientesRepository;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cliente API", Version = "v1" });
});


// Configura��o do DbContext para SQL Server
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

// Configura��o dos reposit�rios
builder.Services.AddScoped<IClientesRepository, ClientesRepository>();
builder.Services.AddScoped<IClienteMongoRepository, ClienteMongoRepository>();

// Configura��o do MediatR
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(AddClienteCommandHandler).Assembly);
});

// Valida��es com fluent
builder.Services.AddValidatorsFromAssemblyContaining<AddClienteCommandValidator>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FailFastPipeLineBehaviors<,>));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<SqlServerDbContext>();
    var loggerFactory = LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    });
    var logger = loggerFactory.CreateLogger<Program>();
    try
    {
        dbContext.Database.Migrate();
        logger.LogInformation("Banco de dados migrado com sucesso.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "Ocorreu um erro ao migrar o banco de dados.");
    }
}

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
