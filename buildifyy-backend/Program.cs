﻿using buildifyy_backend_repository;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddSingleton<IRepository>(InitializeCosmosClientInstance(builder.Configuration.GetSection("CosmosDB")));

var app = builder.Build();

app.UseSwagger();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

static Repository InitializeCosmosClientInstance(IConfigurationSection configurationSection)
{
    var databaseName = configurationSection["DatabaseName"];
    var connectionString = configurationSection["ConnectionString"];
    var client = new CosmosClient(connectionString: connectionString);
    var cosmosDbService = new Repository(client, databaseName);
    return cosmosDbService;
}