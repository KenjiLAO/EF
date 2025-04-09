using MyWebApi2.Models;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        if (context.Products.Any()) return; // éviter de dupliquer

        var category = new Category { Name = "Fruits" };
        context.Categories.Add(category);

        var products = new List<Product>();

        for (int i = 1; i <= 50; i++)
        {
            var product = new Product
            {
                Name = $"Produit {i}",
                Category = category,
                Price = new Money
                {
                    Amount = 1.50m * i,
                    Currency = "EUR"
                },
                Stocks = new List<Stock>
                {
                    new Stock { Quantity = 10 + i },
                    new Stock { Quantity = 5 }
                }
            };

            products.Add(product);
        }

        context.Products.AddRange(products);
        context.SaveChanges();
    }
}
