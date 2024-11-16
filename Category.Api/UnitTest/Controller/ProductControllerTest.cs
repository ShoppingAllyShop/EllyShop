using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CollectionModel.Response;
using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Catalog.Api.UnitTest.InitData;
using Comman.Domain.Elly_Catalog;
using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections;
using Xunit;

namespace Catalog.Api.UnitTest.Controller
{
    public class ProductControllerTest
    {
        private ProductController _productController;
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<IProduct> _mockProductServices = new Mock<IProduct>();
        private Mock<ILogger<ProductController>> _mockLogger = new Mock<ILogger<ProductController>>();

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";

        private readonly Guid newProducts = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid bestSellers = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid favourites = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid promotions = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        public ProductControllerTest()
        {
            _productController = new ProductController(
              _mockUnitOfWork.Object,
              _mockProductServices.Object,
              _mockLogger.Object);
        }

        #region ProductController
        [Fact]
        public void ProductController_Success_ReturnOk_200()
        {
            //Arrange
            MainPageProductResponse mockResult = new MainPageProductResponse
            {
                BestSellers = CreateProductListByTagModelData(),
                Favourites = CreateProductListByTagModelData(),
                NewProducts = CreateProductListByTagModelData(),
                Promotions = CreateProductListByTagModelData()
            };

            _mockProductServices.Setup(x => x.GetMainPageProduct()).Returns(mockResult);

            //Action
            var result = _productController.GetMainpageData();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public void ProductController_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockProductServices.Setup(x => x.GetMainPageProduct()).Throws(new Exception());


            var result = _productController.GetMainpageData();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion


        #region ProductDetail
        [Fact]
        public async Task ProductDetail_Success_ReturnOk_200()
        {
            var id = "EC60";

            //Arrange
            ProductDetailData mockResult = new ProductDetailData
            {
               GuideList = MockData.CreateGuideDataData(),
               GuaranteeList = MockData.CreateGuaranteeData(),
               Favourites = CreateProductListByTagModelData(),
               NewProducts = CreateProductListByTagModelData(),
               Product = CreateProductData(),
               RatingList = MockData.CreateRatingData()

            };

            _mockProductServices.Setup(x => x.GetProductDetailAsync(id)).ReturnsAsync(mockResult);

            //Action
            var result = await _productController.GetProductDetail(id);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task ProductDetail_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            var id = "EC101";

            _mockProductServices.Setup(x => x.GetProductDetailAsync(id)).Throws(new Exception());


            var result = await _productController.GetProductDetail(id);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion




        #region private
        private List<ProductListByTagModel> CreateProductListByTagModelData()
        {
            return new List<ProductListByTagModel>
            {
                new ProductListByTagModel
                {
                    ProductId = ProductId1,
                    ProductName = "New Tag",
                    Price = 100000,
                    Discount = 50000,
                    PercentDiscount = 50,
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
                    },
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TagDescription = "Description",
                TagName = "Name",
                TagTitle = "Title",
                },
                new ProductListByTagModel{

                    ProductId = ProductId1,
                    ProductName = "New Tag",
                    Price = 100000,
                    Discount = 50000,
                    PercentDiscount = 50,
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
                    },
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                TagDescription = "Description",
                TagName = "Name",
                TagTitle = "Title",
                }
            };
        }
        private ProductInfoData CreateProductData()
        {
            return new ProductInfoData
            {
                Id = ProductId1,
                Name = "sản phẩm abc",
                DetailDecription = "Mô Tả",
                Discount = 599000,
                PercentDiscount = 50,
                Price = 1299000,
                ShortDescription = "Mô tả ngắn abcde",
                ProductImages = new List<ProductImageModel>
               {
                   new ProductImageModel
                   {
                       Id = Guid.NewGuid(),
                       ImageIndex = 1,
                       Picture = "hinh 1",
                       ProductId = ProductId1
                  }
               },
            };
        }
        #endregion
    }
}
