namespace Catalog.Api.Models.ProductModel.Respone
{
    public class MainPageProductResponse
    {
        public IEnumerable<ProductListByTagModel>? NewProducts { get; set; }
        public IEnumerable<ProductListByTagModel>? BestSellers { get; set; }
        public IEnumerable<ProductListByTagModel>? Favourites { get; set; }
        public IEnumerable<ProductListByTagModel>? Promotions { get; set; }
    }
}
