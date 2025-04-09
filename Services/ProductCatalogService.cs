using Microsoft.EntityFrameworkCore;

namespace MyWebApi2.Services
{
    public class ProductCatalogService : IProductCatalogService
    {
        private readonly ApplicationDbContext _context;

        public ProductCatalogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Price.Amount,
                    Category = p.Category.Name
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<ProductDto>> GetPagedProductsAsync(int pageIndex, int pageSize)
        {
            return await _context.Products
                .Include(p => p.Category)
                .Skip(pageIndex * pageSize)  // Sauter les produits des pages précédentes
                .Take(pageSize)             // Prendre les produits de la page actuelle
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Price.Amount,
                    Category = p.Category.Name
                })
                .ToListAsync();
        }

        public Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria)
        {
            throw new NotImplementedException();
        }
    }
}
