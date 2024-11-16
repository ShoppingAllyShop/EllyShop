using AutoMapper;
using Catalog.Api.Implements;
using Catalog.Api.Interfaces;
using Category.Api.Interfaces;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.TestHelpers;
using Moq;
using Xunit;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.UnitTest.Implements
{
    public class CatalogServicesTest
    {
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<ILogger<CatalogService>> _mockLogger = new Mock<ILogger<CatalogService>>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();
        private Mock<IProduct> _mockProduct = new Mock<IProduct>();
        private Mock<ICollections> _mockCollections = new Mock<ICollections>();
        private Mock<ICategory> _mockCategory = new Mock<ICategory>();

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";
        private CatalogService CreateService()
        {
            return new CatalogService(
                _mockUnitOfWork.Object,
                _mockProduct.Object,
                _mockCollections.Object,
                _mockCategory.Object,
                _mockLogger.Object,
                _mockMapper.Object
                );
        }
        #region GetMainPageContent
        [Fact]
        public async Task GetMainPageContent_Success_ReturnData()
        {
            var service = CreateService();
            var productListData = CreateProductListData();
            var categoryListData = CreateCategoryListData();
            var colletionData = CreateColletionData();

            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(productListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(categoryListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(colletionData.MockDbSet().Object);


            //Act
            var result = await service.GetMainPageContentAsync();

            //Assert
            Assert.NotNull(result);
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
        private List<Collection> CreateColletionData()
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
