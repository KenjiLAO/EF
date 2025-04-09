using Microsoft.EntityFrameworkCore;

public class SalesReportService
{
    private readonly ApplicationDbContext _context;

    public SalesReportService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SalesDashboardDto>> GetSalesDashboardAsync()
    {
        var salesData = await _context.Orders
            .Where(o => o.OrderDate >= DateTime.Now.AddMonths(-12))  // Filtre sur la dernière année
            .SelectMany(o => o.OrderItems) // Aplatir les commandes et leurs produits (si chaque commande contient plusieurs produits)
            .GroupBy(oi => new { oi.Product.Category.Name, Month = oi.Order.OrderDate.Month }) // Jointure sur la catégorie du produit
            .Select(g => new SalesDashboardDto
            {
                Category = g.Key.Name,
                Month = g.Key.Month,
                TotalSales = g.Sum(oi => oi.TotalAmount) // Somme des ventes
            })
            .ToListAsync();
        
        return salesData;

    }
}
