using Microsoft.EntityFrameworkCore;
using MyWebApi2.DTOs;

namespace MyWebApi2.Services
{
    public class ProductService : IProductCatalogService
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

        public async Task<PagedResult<ProductDto>> GetPagedProductsAsync(int pageIndex, int pageSize)
        {
        var totalCount = await _context.Products.CountAsync();

        var products = await _context.Products
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .Select(p => new ProductDto
            {
                Id = p.ProductId,
                Name = p.Name,
                Price = p.Price.Amount
            })
            .ToListAsync();

        return new PagedResult<ProductDto>
        {
            TotalCount = totalCount,
            Items = products
        };
        }
        public async Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria)
        {
            var query = _context.Products
                .Include(p => p.Category)
                .Include(p => p.Price)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criteria.Name))
                query = query.Where(p => p.Name.Contains(criteria.Name));

            if (criteria.MinPrice.HasValue)
                query = query.Where(p => p.Price.Amount >= criteria.MinPrice.Value);

            if (criteria.MaxPrice.HasValue)
                query = query.Where(p => p.Price.Amount <= criteria.MaxPrice.Value);

            if (criteria.CategoryId.HasValue)
                query = query.Where(p => p.CategoryId == criteria.CategoryId.Value);

            var categories = await _context.Categories.Select(c => c.Name).ToListAsync();
            var totalCount = await query.CountAsync();

            var items = await query
                .Skip(criteria.PageIndex * criteria.PageSize)
                .Take(criteria.PageSize)
                .Select(p => new ProductDto
                {
                    Id = p.ProductId,
                    Name = p.Name,
                    Price = p.Price.Amount,
                    Category = p.Category.Name
                })
                .ToListAsync();

            return new PagedResult<ProductDto>
            {
                TotalCount = totalCount,
                Items = items,
                Facets = categories
            };
        }

        public Task<IEnumerable<ProductDto>> GetAllProductsAsync()
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<ProductDto>> IProductCatalogService.GetPagedProductsAsync(int pageIndex, int pageSize)
        {
            throw new NotImplementedException();
        }
    }
}
