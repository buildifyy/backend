using buildify_backend_repository;
using Microsoft.Azure.Cosmos;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IRepository>(InitializeCosmosClientInstanceAsync(builder.Configuration.GetSection("CosmosDB")).GetAwaiter().GetResult());

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

static async Task<Repository> InitializeCosmosClientInstanceAsync(IConfigurationSection configurationSection)
{
    var databaseName = configurationSection["DatabaseName"];
    var account = configurationSection["Account"];
    var key = configurationSection["Key"];
    var client = new CosmosClient(account, key);
    var cosmosDbService = new Repository(client, databaseName);
    return cosmosDbService;
}