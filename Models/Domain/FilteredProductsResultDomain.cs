using SportEdge.API.Models.DTO;

namespace SportEdge.API.Models.Domain
{
    public class FilteredProductsResultDomain
    {
        public List<Product> ProductsDomain { get; set; }
        public int TotalCountDomain { get; set; }
    }
}
