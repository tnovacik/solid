using System;
using System.Globalization;
using System.IO;

namespace DemoApp
{
    public class ProductService
    {
        public static string SaveRootPath = "c:\\3";

        public void SaveProduct(int id, Product product)
        {
            Logger.Debug("Save product start");

            using (var stream = File.CreateText(GetProductFilePath(id)))
            {
                stream.Write(string.Format(CultureInfo.InvariantCulture, "{0},{1}", product.Name, product.Price));
            }

            SendNotification($"Product saved {id}");

            Logger.Debug("Save product end");
        }

        public string GetProductFilePath(int id)
        {
            return Path.Combine(SaveRootPath, id + ".txt");
        }

        private void SendNotification(string message)
        {
            // Uses some external messaging system that informs other about modification
            Console.WriteLine(message);
        }

        public Product GetProduct(int id)
        {
            Logger.Debug("Get product start");

            var productFilePath = GetProductFilePath(id);
            if (!File.Exists(productFilePath))
            {
                return null;
            }

            using (var stream = File.OpenText(GetProductFilePath(id)))
            {
                var readLine = stream.ReadLine();

                if (readLine != null)
                {
                    var strings = readLine.Split(",");
                    return new Product()
                    {
                        Name = strings[0],
                        Price = Convert.ToDecimal(strings[1], CultureInfo.InvariantCulture)
                    };
                }
            }

            Logger.Debug("Get product end");

            return null;
        }
    }
}