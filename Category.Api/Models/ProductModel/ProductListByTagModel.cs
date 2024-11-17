using Comman.Domain.Elly_Catalog;

namespace Catalog.Api.Models.ProductModel
{
    public class ProductListByTagModel
    {
        public string? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; }
        public decimal? Discount { get; set; }
        public decimal? PercentDiscount { get; set; }
        public List<ProductImages>? ProductImages { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? TagDescription { get; set; }
        public string ? TagTitle { get; set; }
        public string? TagName { get; set; }
    }
}
