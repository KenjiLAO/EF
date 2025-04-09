public interface IProductService
{
    Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria);
}
