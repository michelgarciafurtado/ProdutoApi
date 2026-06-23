using MassTransit;
using Microsoft.EntityFrameworkCore;
using ProdutosApi.Infra.Contexto;
using ProdutosApi.Infra.Repositories;
using ProdutosApi.Models.Interfaces;
using ProdutosApi.Publisher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();

var connectionString = builder.Configuration.GetConnectionString("WorkConnection");


builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddMassTransit(busConfigurator =>
{
    
    busConfigurator.UsingRabbitMq((busContext, rabbitCfg) =>
    {
        rabbitCfg.Message<ProdutoCriadoEvento>(x => x.SetEntityName("produto_criado_event"));
        rabbitCfg.Host(builder.Configuration["RabbitMQ:HostName"] ?? "localhost", "/", h =>
        {
            h.Username(builder.Configuration["RabbitMQ:UserName"] ?? "guest");
            h.Password(builder.Configuration["RabbitMQ:Password"] ?? "guest");
        });

    });
});

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        await DbSeeder.SeedAsync(context);
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "Ocorreu um erro ao alimentar o banco de dados.");
    }
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// CRUCIAL PARA A VERSÃO 4: Iniciar o barramento manualmente
var busControl = app.Services.GetRequiredService<IBusControl>();
await busControl.StartAsync(); // Isso ativa a conexão definitivamente antes de o app aceitar requisições

app.UseAuthorization();

app.MapControllers();

app.Run();
