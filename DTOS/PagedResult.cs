public class PagedResult<T>
{
    public int TotalCount { get; set; }
    public IEnumerable<T> Items { get; set; }
    public IEnumerable<string> Facets { get; set; } // Facettes de recherche
}
