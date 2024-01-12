using MongoDB.Driver;
using MongoDB.Entities;
using SearchService.Data;
using SearchService.Models;
using MassTransit;
using SearchService.Consumers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMassTransit(x=>
{
    x.AddConsumersFromNamespaceContaining<AuctionCreatedConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionDeleteConsumer>();
    x.AddConsumersFromNamespaceContaining<AuctionUpdateConsumer>();
    x.SetEndpointNameFormatter(new KebabCaseEndpointNameFormatter("search"));
    x.UsingRabbitMq((context,cfg)=>{
        cfg.ReceiveEndpoint("search-auction-created",e=>{
             e.UseMessageRetry(r=>r.Interval(5,5));

             e.ConfigureConsumer<AuctionCreatedConsumer>(context);
        });

        cfg.ConfigureEndpoints(context);
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

try
{
    await DbInitializer.InitDb(app);
}
catch (System.Exception)
{
    
    throw;
}

app.Run();
