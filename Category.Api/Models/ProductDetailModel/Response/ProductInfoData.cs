using Comman.Domain.Elly_Catalog;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.Models.ProductDetailModel.Response
{
    public class ProductInfoData
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public string? ShortDescription { get; set; }
        public string? DetailDecription { get; set; }
        public decimal Price { get; set; }
        public decimal? PercentDiscount { get; set; }
        public decimal? Discount { get; set; }
        public CategoryEntity? Category { get; set; }
        public IEnumerable<ProductDetail>? ProductDetail { get; set; }
        public IEnumerable<ProductImageModel> ProductImages { get; set; } = Enumerable.Empty<ProductImageModel>();
    }

    public class ProductImageModel
    {
        public Guid Id { get; set; }
        public string? ProductId { get; set; }
        public int? ImageIndex { get; set; }
        public string? Type { get; set; }
        public string? Picture { get; set; }
        public Guid? ColorId { get; set; }
        public bool? DefaultColor { get; set; }
    }
}
