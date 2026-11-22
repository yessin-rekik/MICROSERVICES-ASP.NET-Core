namespace Shared.Models;

/// <summary>
/// Message DTO exchanged between the two microservices.
/// ProductsAPI publishes it to RabbitMQ, OrdersAPI consumes it and mirrors
/// the product in its own database.
/// </summary>
public class ProductCreated
{
    public int Id { get; set; }

    public string Name { get; set; } = String.Empty;
}
