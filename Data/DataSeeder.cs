using MyWebApi2.Models;

public static class DataSeeder
{
    public static void Seed(ApplicationDbContext context)
    {
        // Vérifie si des produits et des commandes existent déjà pour éviter de dupliquer les données
        if (context.Categories.Any() && context.Products.Any() && context.Orders.Any())
            return; // Si des données existent, on ne fait rien

        // Crée une catégorie "Fruits"
        var category = new Category { Name = "Fruits" };
        context.Categories.Add(category);

        // Crée une liste de produits pour la catégorie
        var products = new List<Product>();

        for (int i = 1; i <= 5; i++)  // Ajoute moins de produits pour tester
        {
            var product = new Product
            {
                Name = $"Produit {i}",
                Category = category, // Associe le produit à la catégorie "Fruits"
                Price = new Money
                {
                    Amount = 1.50m * i, // Le prix augmente à chaque itération
                    Currency = "EUR"
                },
                Stocks = new List<Stock>
                {
                    new Stock { Quantity = 10 + i }, // Stock de 10 + i pour chaque produit
                    new Stock { Quantity = 5 } // Stock supplémentaire de 5
                }
            };

            products.Add(product); // Ajoute le produit à la liste
        }

        context.Products.AddRange(products);

        // Crée un client pour associer à la commande
        var customer = new Customer
        {
            FullName = "John Doe",
            Email = "john.doe@example.com"
        };

        context.Customers.Add(customer);

        // Crée une commande pour le client
        var order = new Order
        {
            Customer = customer,
            OrderDate = DateTime.Now,
            OrderItems = new List<OrderItem>
            {
                new OrderItem
                {
                    Product = products[0], // Produit 1
                    Quantity = 2
                },
                new OrderItem
                {
                    Product = products[1], // Produit 2
                    Quantity = 3
                }
            }
        };

        context.Orders.Add(order);

        // Sauvegarde toutes les données dans la base de données
        context.SaveChanges();
    }
}
