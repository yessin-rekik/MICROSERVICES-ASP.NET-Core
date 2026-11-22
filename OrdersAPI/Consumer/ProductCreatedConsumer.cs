using MassTransit;
using OrdersAPI.Data;
using Shared.Models;

namespace OrdersAPI.Consumer
{
    /// <summary>
    /// Consumes the ProductCreated messages sent by ProductsAPI and stores
    /// the product in the OrdersAPI database.
    /// </summary>
    public class ProductCreatedConsumer : IConsumer<ProductCreated>
    {
        private readonly OrdersAPIContext _ordersAPIContext;

        public ProductCreatedConsumer(OrdersAPIContext ordersAPIContext)
        {
            _ordersAPIContext = ordersAPIContext;
        }

        public async Task Consume(ConsumeContext<ProductCreated> context)
        {
            // RabbitMQ guarantees at-least-once delivery: the same message can be
            // received twice, so the local copy is inserted or updated, never duplicated.
            var product = await _ordersAPIContext.Products.FindAsync(context.Message.Id);

            if (product == null)
            {
                product = new Product { Id = context.Message.Id };
                _ordersAPIContext.Products.Add(product);
            }

            product.Name = context.Message.Name;

            await _ordersAPIContext.SaveChangesAsync();
        }
    }
}
