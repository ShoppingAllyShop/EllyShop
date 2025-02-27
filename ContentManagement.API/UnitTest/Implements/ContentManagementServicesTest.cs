using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.TestHelpers;
using ContentManagement.API.Implements;
using Moq;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;
using Comman.Domain.Elly_ContentManagement;
using Xunit;
using Common.Infrastructure;
using ContentManagement.API.Models.Responses;

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
            var navigationData = CreateNavigationData();
            var newsListData = CreateNewsListData();
            var socialMediaListData = CreateSocialMediaData();
            var generalInfomationListData = CreateGeneralInfomationsData();
            var policyListData = CreatePolicyData();
            var branchListData = CreateBranchListData();
            var newsMediaListData = CreateNewsMediaListData();
            var prizeData = CreatePrizeListData();
            var sildeData = CreateSildeListData();

            _mockUnitOfWork.Setup(x => x.Repository<Product>()).Returns(productListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(categoryListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(colletionData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Navigation>()).Returns(navigationData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<News>()).Returns(newsListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<SocialMedia>()).Returns(socialMediaListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Policy>()).Returns(policyListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<GeneralInfomation>()).Returns(generalInfomationListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Branch>()).Returns(branchListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<NewsMedia>()).Returns(newsMediaListData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Prize>()).Returns(prizeData.MockDbSet().Object);
            _mockUnitOfWork.Setup(x => x.Repository<Silde>()).Returns(sildeData.MockDbSet().Object);
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
        private List<Navigation> CreateNavigationData()
        {
            return new List<Navigation>
            {
                new Navigation
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 1",
                    NavContent = "Content 1"
                },
                new Navigation
                {
                    Id = Guid.NewGuid(),
                    Name = "Name 2",
                    NavContent = "Content 2"
                },
            };
        }
        private List<SocialMedia> CreateSocialMediaData()
        {
            return new List<SocialMedia>
            {
                new SocialMedia
                {
                     Id = Guid.NewGuid(),
                     Name = "Home",
                     Picture = "Hình 1",
                     Url = ""
                },
                 new SocialMedia
                {
                     Id = Guid.NewGuid(),
                     Name = "Home",
                     Picture = "Hình 2",
                     Url = ""
                }
            };
        }
        private List<Policy> CreatePolicyData()
        {
            return new List<Policy>
            {
                new Policy
                {
                     Id = Guid.NewGuid(),
                     Title = "Home",
                     Url = ""
                },
                 new Policy
                {
                     Id = Guid.NewGuid(),
                     Title = "Home1",
                     Url = ""
                }
            };
        }
        private List<GeneralInfomation> CreateGeneralInfomationsData()
        {
            return new List<GeneralInfomation>
            {
                new GeneralInfomation
                {
                     Id = Guid.NewGuid(),
                     Title = "Home",
                     Url = ""
                },
                 new GeneralInfomation
                {
                     Id = Guid.NewGuid(),
                     Title = "Home2",
                     Url = ""
                }
            };
        }
        private List<Branch> CreateBranchListData()
        {
            return new List<Branch>
            {
              new Branch
              {
                  Address = "1233123",
                  BranchName = "HCM",
                  CityCode = 70000,
                  CityName = "HCM CITY",
                  Id = Guid.NewGuid(),
                  Region = "Miền Nam"
              },
              new Branch
              {
                  Address = "123434123",
                  BranchName = "Hà Nội",
                  CityCode = 70000,
                  CityName = "HCM CITY",
                  Id = Guid.NewGuid(),
                  Region = "Miền Bắc"
              }
            };
        }
        private List<News> CreateNewsListData()
        {
            return new List<News>
            {
              new News
              {
                  Id = Guid.NewGuid(),
                  CreatedAt = DateTime.Now,
                  Image = "Hình 1",
                  NewContent = "Abv",
                  Title = "Title",
                  Url = ""
              },
              new News
              {
                  Id = Guid.NewGuid(),
                  CreatedAt = DateTime.Now,
                  Image = "Hình 1",
                  NewContent = "Abv",
                  Title = "Title",
                  Url = ""
              }
            };
        }
        private List<NewsMedia> CreateNewsMediaListData()
        {
            return new List<NewsMedia>
            {
              new NewsMedia
              {
                  Id = Guid.NewGuid(),
                  CreatedAt = DateTime.Now,
                  Image = "Hình 1",
                  NewsMediaContent = "Avb22",
                  Title = "Title",
                  Url = ""
              },
              new NewsMedia
              {
                  Id = Guid.NewGuid(),
                  CreatedAt = DateTime.Now,
                  Image = "Hình 1",
                  NewsMediaContent = "abssw22",
                  Title = "Title",
                  Url = ""
              }
            };
        }
        private List<Prize> CreatePrizeListData()
        {
            return new List<Prize>
            {
              new Prize
              {
                  Id = Guid.NewGuid(),
                  Image = "Hình 1",
                  CreateAt = DateTime.Now,
                  NewContent = "abvsd",
                  Title = "Title",
                  Url = ""
              },
              new Prize
              {
                  Id = Guid.NewGuid(),
                  Image = "Hình 1",
                  CreateAt = DateTime.Now,
                  NewContent = "abvsd111",
                  Title = "Title",
                  Url = ""
              }
            };
        }
        private List<Silde> CreateSildeListData()
        {
            return new List<Silde>
            {
              new Silde
              {
                  Id = Guid.NewGuid(),
                  Picture = "hình 1",
                  PictureIndex = 0,
                  Position = "abc"
              },
              new Silde
              {
                  Id = Guid.NewGuid(),
                  Picture = "hình 2",
                  PictureIndex = 1,
                  Position = "abc"
              }
            };
        }
        #endregion
    }
}
