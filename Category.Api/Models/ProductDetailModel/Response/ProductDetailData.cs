using Comman.Domain.Models;
using System.Drawing;
using Guarantee = Comman.Domain.Elly_Catalog.Guarantee;
using Guide = Comman.Domain.Elly_Catalog.Guide;
using ProductImages = Comman.Domain.Elly_Catalog.ProductImages;
using ProductDetail = Comman.Domain.Elly_Catalog.ProductDetail;
using Product = Comman.Domain.Elly_Catalog.Product;
using Color = Comman.Domain.Elly_Catalog.Color;
using Size = Comman.Domain.Elly_Catalog.Size;
using Rating = Comman.Domain.Elly_Catalog.Rating;
using Branch = Comman.Domain.Elly_ContentManagement.Branch;
using Catalog.Api.Models.ProductModel;

namespace Catalog.Api.Models.ProductDetailModel.Response
{
    public class ProductDetailData
    {
            public IEnumerable<Guarantee>? GuaranteeList { get; set; }
            public IEnumerable<Guide>? GuideList { get; set; }
            public ProductInfoData? Product { get; set; }
            public IEnumerable<ProductListByTagModel>? NewProducts { get; set; }
            public IEnumerable<ProductListByTagModel>? Favourites { get; set; }
            public IEnumerable<Rating>? RatingList { get; set; }        
    }
}
