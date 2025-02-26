using Catalog.Api.Implements;
using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Catalog.Api.UnitTest.InitData;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.Constants;
using CommonLib.Helpers.Interfaces;
using CommonLib.Models.Settings;
using CommonLib.TestHelpers;
using Microsoft.Extensions.Options;
using Moq;
using System.Drawing;
using Xunit;
using static CommonLib.Constants.AppEnums;
using Guarantees = Comman.Domain.Elly_Catalog.Guarantee;
using Rating = Comman.Domain.Elly_Catalog.Rating;
namespace Catalog.Api.UnitTest.Implements
{
    public class ProductServicesTest
    {
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<ILogger<ProductService>> _mockLogger = new Mock<ILogger<ProductService>>();

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";

        private ProductService CreateService()
        {
            return new ProductService(
                _mockUnitOfWork.Object,
                _mockLogger.Object
                );
        }

        #region GetProductDetail
        [Fact]
        public async Task GetProductDetail_Success_ReturnData()
        {
            //Arrange
            var service = CreateService();
            var guaranteeData = MockData.CreateGuaranteeData();
            var guideData = MockData.CreateGuideDataData();
            var productListData = CreateProductListData();
            var productTagsListData = CreateProductTagsListData();
            var tagListData = CreateTagsListData();
            var ratingData = MockData.CreateRatingData();

            _mockUnitOfWork.Setup(x => x.Repository<Guarantees>()).Returns(guaranteeData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Guide>()).Returns(guideData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Rating>()).Returns(ratingData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(productListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<ProductTags>()).Returns(productTagsListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Tags>()).Returns(tagListData.MockDbSet().Object);

            //Act
            var result = await service.GetProductDetailAsync(ProductId1);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<ProductDetailData>(result);
        }
        #endregion

        #region GetMainPageProduct
        [Fact]
        public void GetProduct_Success_ReturnData()
        {
            var service = CreateService();
            //var productData = CreateProductData();
            var productListData = CreateProductListData();
            var productTagsListData = CreateProductTagsListData();
            var tagListData = CreateTagsListData();

            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(productListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<ProductTags>()).Returns(productTagsListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Tags>()).Returns(tagListData.MockDbSet().Object);

            //Act
            var result = service.GetMainPageProduct();

            //Assert
            Assert.NotNull(result);
            Assert.IsType<MainPageProductResponse>(result);
        }
        #endregion



        #region private

        private List<Product> CreateProductListData()
        {
            return new List<Product>
            {
                new Product
                {
                    Id = "EC001",
                    Name = ProductName1,
                    DetailDecription = "Mô Tả",
                    Discount = 599000,
                    PercentDiscount = 50,
                    Price = 1299000,
                    ShortDescription = "Mô tả ngắn ....",
                    Category = new Comman.Domain.Elly_Catalog.Category
                    {
                        Id = Guid.NewGuid(),
                        ParentId = Guid.NewGuid(),
                    },
                    ProductImages = new List<ProductImages>
                    {
                        new ProductImages
                          {
                               Id = Guid.NewGuid(),
                               ImageIndex = 1,
                               Picture = "hinh 1",
                               ProductId = ProductId1
                          },
                        new ProductImages
                          {
                               Id = Guid.NewGuid(),
                               ImageIndex = 2,
                               Picture = "hinh 2",
                               ProductId = ProductId1
                          }
                    }

                },
                new Product
                {
                    Id= "A001",
                    DetailDecription = "Mô Tả",
                    Discount = 30000,
                    PercentDiscount = 50,
                    Price = 60000,
                    ShortDescription = "Mô tả ngắn ....",
                    Category = new Comman.Domain.Elly_Catalog.Category
                    {
                        Id = Guid.NewGuid(),
                        ParentId = Guid.NewGuid(),
                    }
                }
            };
        }

        private List<ProductTags> CreateProductTagsListData()
        {
            return new List<ProductTags>
            {
                new ProductTags
                {
                    Id = Guid.NewGuid(),
                    AssignedDate = DateTime.Now,
                    ProductId = "EC001",
                    TagId = new Guid(GuidContstants.NewTag)
                },
                new ProductTags
                {
                    Id = Guid.NewGuid(),
                    AssignedDate = DateTime.Now,
                    ProductId = "EC001",
                    TagId = new Guid(GuidContstants.FavoriteTag)
                },
                new ProductTags
                {
                    Id = Guid.NewGuid(),
                    AssignedDate = DateTime.Now,
                    ProductId = "A031",
                    TagId = new Guid(GuidContstants.bestSellers)
                },
                new ProductTags
                {
                    Id = Guid.NewGuid(),
                    AssignedDate = DateTime.Now,
                    ProductId = "A041",
                    TagId = new Guid(GuidContstants.promotions)
                }
            };
        }

        private List<Tags> CreateTagsListData()
        {
            return new List<Tags>
            {
                new Tags
                {
                    Id = new Guid(GuidContstants.NewTag),
                    Name = "New Tag",
                    CreatedAt = DateTime.Now,
                },
                new Tags{
                    Id = new Guid(GuidContstants.FavoriteTag),
                    Name = "Favorite Tag",
                    CreatedAt = DateTime.Now,
                }
            };
        }

        #endregion
    }
}
