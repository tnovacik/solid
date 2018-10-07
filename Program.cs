using System.Linq;

namespace DemoApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var productService = new ProductService();
            
            productService.SaveProduct(1, new Product
            {
                Name = "Test",
                Price = 123.32m
            });

            var product = productService.GetProduct(1);
        }
    }
}