public interface IProductCatalogService
{
    Task<IEnumerable<ProductDto>> GetAllProductsAsync();
    Task<IEnumerable<ProductDto>> GetPagedProductsAsync(int pageIndex, int pageSize);
    Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria);
}
