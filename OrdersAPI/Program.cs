using MassTransit;
using Microsoft.EntityFrameworkCore;
using OrdersAPI.Consumer;
using OrdersAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// services a ajouter
builder.Services.AddDbContext<OrdersAPIContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("OrdersAPIContext")));

builder.Services.AddMassTransit(options =>
{
    options.AddConsumer<ProductCreatedConsumer>();

    options.UsingRabbitMq((context, cfg) =>
    {
        // Port 4001 = port expose par le conteneur Docker RabbitMQ.
        cfg.Host(new Uri("rabbitmq://localhost:4001"), h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        cfg.ReceiveEndpoint("event-listener", e =>
        {
            e.ConfigureConsumer<ProductCreatedConsumer>(context);
        });
    });
});

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
