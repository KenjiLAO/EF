using Microsoft.EntityFrameworkCore;

public class ProductSearchService
{
    private readonly ApplicationDbContext _context;

    public ProductSearchService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria)
    {
        var query = _context.Products.AsQueryable();

        if (!string.IsNullOrEmpty(criteria.Name))
            query = query.Where(p => p.Name.Contains(criteria.Name));

        if (criteria.MinPrice.HasValue)
            query = query.Where(p => p.Price.Amount >= criteria.MinPrice);  // 'Amount' est un exemple, changez-le selon votre structure.

        if (criteria.MaxPrice.HasValue)
            query = query.Where(p => p.Price.Amount <= criteria.MaxPrice);  // 'Amount' est un exemple, changez-le selon votre structure.

        if (criteria.CategoryId.HasValue)
            query = query.Where(p => p.CategoryId == criteria.CategoryId);

        // Facettes de recherche
        var categories = await _context.Products
            .GroupBy(p => p.Category.Name)
            .Select(g => g.Key)
            .ToListAsync();

        var totalCount = await query.CountAsync();

        var products = await query
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
            Items = products,
            Facets = categories
        };
    }
}
