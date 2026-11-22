using System.ComponentModel.DataAnnotations.Schema;

namespace OrdersAPI
{
    /// <summary>
    /// Local copy of the product data required by OrdersAPI.
    /// It is filled asynchronously from the ProductCreated messages published
    /// by ProductsAPI. The identifier is not generated here: it is the same
    /// ProductId used by ProductsAPI so that Orders.ProductId stays meaningful.
    /// </summary>
    public class Product
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        public string Name { get; set; } = String.Empty;
    }
}
