using Microsoft.EntityFrameworkCore;
using MyWebApi2.DTOs;

namespace MyWebApi2.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<ProductSummaryDto>> GetProductSummariesAsync()
        {
            return await _context.Products
                .Select(p => new ProductSummaryDto
                {
                    Name = p.Name,
                    Price = p.Price.Amount,
                    TotalStock = p.Stocks.Sum(s => s.Quantity)
                })
                .ToListAsync();
        }

        public async Task<List<ProductSummaryDto>> GetPagedProductsAsync(int page, int pageSize)
        {
            return await _context.Products
                .OrderBy(p => p.Name)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductSummaryDto
                {
                    Name = p.Name,
                    Price = p.Price.Amount,
                    TotalStock = p.Stocks.Sum(s => s.Quantity)
                })
                .ToListAsync();
        }
    }
}
