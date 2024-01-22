using Nest;
using ElasticDotnet.Domain.Models;
using Elasticsearch.Net;
using System.Text;
using elastic_tuto.Services;



var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// Configure Elasticsearch client
var elasticCloudId = "Elastic-Dotnet:ZXVyb3BlLXdlc3QyLmdjcC5lbGFzdGljLWNsb3VkLmNvbSQ3NzkxNzQ0OWU3ODk0NmEzYmIyOTBhOGY4MDg5N2FmYSQ2NjdiMGIzZDhkZjQ0NDBjODBlNjcwZmEyOWFkZmExMQ==";
var username = "elastic";
var elasticPassword = "6xeUll1DSxgavMge9XTVUL1A";

// Add the Elasticsearch client to the DI container
builder.Services.AddSingleton<IElasticClient>(_ =>
{
    var settings = new ConnectionSettings(
            new CloudConnectionPool(elasticCloudId,
                new BasicAuthenticationCredentials("elastic", elasticPassword))
        )
        .DisableDirectStreaming()
        .DefaultMappingFor<Product>(m => m
            .PropertyName(p => p.ProductName, "ProductName")
            .PropertyName(p => p.Description, "Description")
        );

    return new ElasticClient(settings);
});
builder.Services.AddScoped<ISearchService, SearchService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
