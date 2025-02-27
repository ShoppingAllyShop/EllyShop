using Comman.Domain.Elly_Catalog;
using Comman.Domain.Elly_ContentManagement;
using ContentManagement.API.Controllers;
using ContentManagement.API.Interfaces;
using ContentManagement.API.Models.Responses;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace ContentManagement.API.UnitTest.Controller
{
    public class ContentManagementControllerTest
    {
        private ContentManagementController _contentManagementController;
        private Mock<IContentManagement> _mockcatalogServices = new Mock<IContentManagement>();
        private Mock<ILogger<ContentManagementController>> _mockLogger = new Mock<ILogger<ContentManagementController>>();

        public ContentManagementControllerTest()
        {
            _contentManagementController = new ContentManagementController(
              _mockcatalogServices.Object,
              _mockLogger.Object
            );

        }
        #region GetMainPageContent
        [Fact]
        public void GetMainPageContent_Success_ReturnOk_200()
        {
            //Arrange
            MainPageResponse mockResult = new MainPageResponse
            {
              LayoutData = CreateLayOutData(),
              MainPageData = CreateMainPageData()
            };

            _mockcatalogServices.Setup(x => x.GetContentMainPage()).Returns(mockResult);

            //Action
            var result = _contentManagementController.GetMainPageContent();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public void GetMainPageContent_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockcatalogServices.Setup(x => x.GetContentMainPage()).Throws(new Exception());

            //Act
            var result = _contentManagementController.GetMainPageContent();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region private

        private LayoutData CreateLayOutData()
        {
            return new LayoutData
            {
                HeaderData = CreateHeaderData(),
                FooterData = CreateFooterData()
            };
        }
        private MainPageData CreateMainPageData()
        {
            return new MainPageData
            {
                BranchList = CreateBranchListData(),
                NewsMediaList = CreateNewsMediaListData(),
                NewsList = CreateNewsListData(),
                PrizeList = CreatePrizeListData(),
                SilderList = CreateSildeListData(),
            };
        }
        private HeaderData CreateHeaderData()
        {
            return new HeaderData
            {
               Navigation = CreateNavData()
            };
        }
        private List<Navigation> CreateNavData()
        {
            return new List<Navigation>
            {
                new Navigation
                {
                     Id = Guid.NewGuid(),
                     IsActive = true,
                     Name = "Home",
                     NavContent = "abcder",
                     NavIndex = 0,
                     NavLevel = 1,
                     ParentId = Guid.NewGuid(),
                },
                 new Navigation
                {
                     Id = Guid.NewGuid(),
                     IsActive = true,
                     Name = "Home",
                     NavContent = "abcder",
                     NavIndex = 0,
                     NavLevel = 1,
                     ParentId = Guid.NewGuid(),
                }
            }; 
        }
        private FooterData CreateFooterData()
        {
            return new FooterData
            {
                SocialMedias = CreateSocialMediaData(),
                Policies = CreatePolicyData(),
                GeneralInfomations = CreateGeneralInfomationsData()
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
