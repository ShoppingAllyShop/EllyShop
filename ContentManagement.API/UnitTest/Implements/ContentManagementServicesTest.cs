using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.TestHelpers;
using ContentManagement.API.Implements;
using Moq;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;
using Comman.Domain.Elly_ContentManagement;
using Xunit;

namespace Catalog.Api.UnitTest.Implements
{
    public class ContentManagementServicesTest
    {
        private Mock<IUnitOfWork<Elly_ContentManagementContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_ContentManagementContext>>();
        private Mock<ILogger<ContentManagementService>> _mockLogger = new Mock<ILogger<ContentManagementService>>();
       

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";
        private ContentManagementService CreateService()
        {
            return new ContentManagementService(
                _mockUnitOfWork.Object,
                _mockLogger.Object
                );
        }
        #region GetMainPageContent
        [Fact]
        public void GetMainPageContent_Success_ReturnData()
        {
            var service = CreateService();
            var productListData = CreateProductListData();
            var categoryListData = CreateCategoryListData();
            var colletionData = CreateColletionData();

            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(productListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(categoryListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(colletionData.MockDbSet().Object);


            //Act
            var result = service.GetContentMainPage();

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
