using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Comman.Domain.Elly_Catalog;

namespace Catalog.Api.Interfaces
{
    public interface IProduct
    {
        Task<ProductDetailData> GetProductDetailAsync(string id);

        MainPageProductResponse GetMainPageProduct();
        
    }
}
