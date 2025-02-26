using AutoMapper;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Category.Api.Implements;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;
using System.Security.Cryptography.Xml;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;
using CommonLib.Constants;
namespace Catalog.Api.Implements
{
    public class ProductService : IProduct
    {
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<ProductService> _logger;
        public ProductService(IUnitOfWork<Elly_CatalogContext> unitOfWork, ILogger<ProductService> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<ProductDetailData> GetProductDetailAsync(string id)
        {
            var guaranteeList = _unitOfWork.Repository<Guarantee>().AsNoTracking().AsEnumerable();
            var guideList = _unitOfWork.Repository<Guide>().AsNoTracking().AsEnumerable();
            var product = await GetProductAsync(id);
            var newProducts = GetProductListByTag(new Guid(GuidContstants.NewTag));
            var favourites = GetProductListByTag(new Guid(GuidContstants.FavoriteTag));
            var ratingList = _unitOfWork.Repository<Rating>().AsNoTracking().AsEnumerable();

            var productDetailContent = new ProductDetailData()
            {
                GuaranteeList = guaranteeList,
                GuideList = guideList,
                Product = product,
                NewProducts = newProducts,
                Favourites = favourites,
                RatingList = ratingList,
            };
            return productDetailContent;
        }
        public MainPageProductResponse GetMainPageProduct()
        {
            var result = new MainPageProductResponse();
            var newProducts = GetProductListByTag(new Guid(GuidContstants.NewTag));
            var bestSellers = GetProductListByTag(new Guid(GuidContstants.bestSellers));
            var favourites = GetProductListByTag(new Guid(GuidContstants.FavoriteTag));
            var promotions = GetProductListByTag(new Guid(GuidContstants.promotions));

            result.NewProducts = newProducts.Take(8);
            result.BestSellers = bestSellers.Take(8);
            result.Favourites = favourites.Take(8);
            result.Promotions = promotions.Take(8);

            return result;
        }

        private async Task<ProductInfoData?> GetProductAsync(string id)
        {
            var result = await _unitOfWork.Repository<Product>().AsNoTracking().Where(x => x.Id == id).Include(x => x.ProductImages).Include(x => x.ProductDetail).
                        ThenInclude(x => x.Color).Include(x => x.ProductDetail).ThenInclude(x => x.Size).Include(x => x.Category).Include(x => x.ProductTags)
                        .Select(x => new ProductInfoData
                        {
                            Id = x.Id,
                            Name = x.Name,
                            Category = x.Category,
                            ShortDescription = x.ShortDescription,
                            DetailDecription = x.DetailDecription,
                            Price = x.Price,
                            PercentDiscount = x.PercentDiscount,
                            Discount = x.Discount,
                            ProductDetail = x.ProductDetail.Select(x => new ProductDetail { Id = x.Id, Color = x.Color, Size = x.Size, Quantity = x.Quantity }),
                            ProductImages = x.ProductImages.Select(x => new ProductImageModel { Id = x.Id, ProductId = x.ProductId, Picture = x.Picture, ImageIndex = x.ImageIndex, Type = x.Type, ColorId = x.ColorId, DefaultColor = x.DefaultColor  }).AsEnumerable(),
                        }).FirstOrDefaultAsync();
            if (result == null) throw new BusinessException("Sản phẩm không tìm thấy");
            return result;
        }

        private IEnumerable<ProductListByTagModel> GetProductListByTag(Guid tagId)
        {
            var result = from p in _unitOfWork.Repository<Product>().AsNoTracking()
                         join pt in _unitOfWork.Repository<ProductTags>().AsNoTracking() on p.Id equals pt.ProductId
                         join t in _unitOfWork.Repository<Tags>().AsNoTracking() on pt.TagId equals t.Id
                         where pt.TagId == tagId  // Adjust based on your needs
                         orderby p.CreatedAt descending
                         select new ProductListByTagModel
                         {
                             ProductId = p.Id,
                             ProductName = p.Name,
                             Price = p.Price,
                             PercentDiscount = p.PercentDiscount,
                             Discount = p.Discount,
                             CreatedAt = p.CreatedAt,
                             UpdatedAt = p.UpdatedAt,
                             TagDescription = t.Description,
                             TagTitle = t.Title,
                             TagName = t.Name,
                             ProductImages = p.ProductImages.Where(x => x.ImageIndex <= 2).ToList(),
                         };

            if (result == null) throw new BusinessException("Sản phẩm không tìm thấy");

            return result;
        }
    }
}
