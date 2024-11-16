using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CatalogModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.UnitTest.Controller
{
    public class CataLogControllerTest
    {
        private CatalogController _catalogController;
        private Mock<ICatalog> _mockcatalogServices = new Mock<ICatalog>();
        private Mock<ILogger<CatalogController>> _mockLogger = new Mock<ILogger<CatalogController>>();

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";
        public CataLogControllerTest()
        {
            _catalogController = new CatalogController(
              _mockcatalogServices.Object,
              _mockLogger.Object
            );

        }
        #region GetMainPageContent
        [Fact]
        public async Task GetMainPageContent_Success_ReturnOk_200()
        {
            //Arrange
            CatalogDataResponse mockResult = new CatalogDataResponse
            {
                ProductList = new MainPageProductResponse
                {
                    BestSellers = CreateProductListByTagModelData(),
                    Favourites = CreateProductListByTagModelData(),
                    NewProducts = CreateProductListByTagModelData(),
                    Promotions = CreateProductListByTagModelData()
                },
                CollectionsList = CreateColletionData(),
                CategoryList = CreateCategoryListData(),

            };

            _mockcatalogServices.Setup(x => x.GetMainPageContentAsync()).ReturnsAsync(mockResult);

            //Action
            var result = await _catalogController.GetMainPageContent();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetMainPageContent_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockcatalogServices.Setup(x => x.GetMainPageContentAsync()).Throws(new Exception());

            var result = await _catalogController.GetMainPageContent();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region private
        private List<CategoryEntity> CreateCategoryListData()
        {
            return new List<CategoryEntity>
            {
                new CategoryEntity
                {
                   Id = Guid.NewGuid(),
                   CategoryLevel = 1,
                   Gender = true,
                   Name = "testbale",
                   ParentId = Guid.NewGuid(),
                },
                new CategoryEntity
                {
                   Id = Guid.NewGuid(),
                   CategoryLevel = 1,
                   Gender = true,
                   Name = "testbale2",
                   ParentId = Guid.NewGuid(),
                },
            };
        }
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
        private IEnumerable<Collection> CreateColletionData()
        {
            return new List<Collection>
            {
                new Collection
                {
                    Id = Guid.NewGuid(),
                    Name = "Túi Nữ",
                    Description = "sản phẩm độc nhất"
                },
                new Collection
                {
                    Id = Guid.NewGuid(),
                    Name = "Túi Nam",
                    Description = "sản phẩm độc nhất 2"
                },
            };
        }
        #endregion
    }
}
