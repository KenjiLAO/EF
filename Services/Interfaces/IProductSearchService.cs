namespace MyWebApi2.Services.Interfaces
{
    public interface IProductSearchService
    {
        Task<PagedResult<ProductDto>> SearchProductsAsync(ProductSearchCriteria criteria);
    }
}
