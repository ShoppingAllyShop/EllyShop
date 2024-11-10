using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Comman.Domain.Elly_Catalog;

namespace Catalog.Api.Interfaces
{
    public interface IProduct
    {
        //Task<ProductInfoData?> GetProductAsync(string id);
        Task<ProductDetailData> GetProductDetail(string id);

        MainPageProductResponse GetMainPageProduct();
        
    }
}
