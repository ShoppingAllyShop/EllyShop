using AutoMapper;
using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CategoryModel.Request;
using Catalog.Api.Models.CollectionModel.Request;
using Catalog.Api.Models.CollectionModel.Response;
using Catalog.Api.UnitTest.InitData;
using Category.Api.Controllers;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.Constants;
using CommonLib.Exceptions;
using CommonLib.Models.Base;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace Catalog.Api.UnitTest.Controller
{
    public class CollectionControllerTest
    {
        private CollectionController _collectionController;
        private Mock<ICollections> _mockCollectionServices = new Mock<ICollections>();
        private Mock<ILogger<CollectionController>> _mockLogger = new Mock<ILogger<CollectionController>>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly string CollectionName = "Gucci";
        private readonly Guid Id = new Guid("67ff74b8-25c7-4065-aeef-2fd15a235255");

        public CollectionControllerTest()
        {
            _collectionController = new CollectionController(
               _mockCollectionServices.Object,
              _mockLogger.Object,
              _mockMapper.Object
            );
        }

        #region GetCollection
        [Fact]
        public async Task GetCollection_Success_ReturnOk_200()
        {
            //Arrange
            List<Collection> mockResult = new List<Collection>
            {
                new Collection
                {
                Id = Guid.NewGuid(),
                Name = CollectionName,
                Description = "testing abc",
                }             
            };
            _mockCollectionServices.Setup(x => x.GetCollectionAsync()).ReturnsAsync(mockResult);

            //Action
            var result = await _collectionController.GetCollection();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetCollection_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            CollectionResponse mockResult = new CollectionResponse
            {
                Id = Guid.NewGuid(),
                Name = CollectionName,
                Discscription = "testing abc",
            };
            _mockCollectionServices.Setup(x => x.GetCollectionAsync()).Throws(new Exception());

            //Action
            var result = await _collectionController.GetCollection();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region AddCollectionController
        [Fact]
        public async Task AddCategory_Success_ReturnOk_200()
        {
            //Arrange

            var returnAsync = "abcdef";

            _mockCollectionServices.Setup(x => x.AddCollectionAsync(It.IsAny<CollectionRequest>())).ReturnsAsync(returnAsync);

            //Act
            var requestModel = new CollectionRequest
            {
                Id = Guid.NewGuid(),
                Name = "abc",
                Description = null,
            };

            var result = await _collectionController.AddCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task AddCategory_Failed_ThrowBusinessException()
        {
            //Arrange

            _mockCollectionServices.Setup(x => x.AddCollectionAsync(It.IsAny<CollectionRequest>())).ThrowsAsync(new BusinessException());

            //Act
            var requestModel = new CollectionRequest
            {
                Id = Guid.NewGuid(),
                Name = "abc",
                Description = null,
            };

            var result = await _collectionController.AddCollection(requestModel);

            //ActAssert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task AddCategory_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockCollectionServices.Setup(x => x.AddCollectionAsync(It.IsAny<CollectionRequest>())).Throws(new Exception());

            //Act
            var requestModel = new CollectionRequest
            {
                Id = Guid.NewGuid(),
                Name = "abc",
                Description = null,
            };
            var result = await _collectionController.AddCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region DeletecollectionController
        [Fact]
        public async Task DeleteCategory_Success_ReturnOk_200()
        {
            //Arrange
            var mockResult = CreateDeleteCollectionResponse();
            var requestModel = CreateDeleteCollectionRequest();

            _mockCollectionServices.Setup(x => x.DeleteCollectionAsync(It.IsAny<DeleteCollectionRequest>())).ReturnsAsync(mockResult);

            //Act

            var result = await _collectionController.DeleteCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_Failed_ThrowBusinessException_ReturnBadRequest_400()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.DeleteCollectionAsync(It.IsAny<DeleteCollectionRequest>())).Throws(new BusinessException());

            var requestModel = CreateDeleteCollectionRequest();

            //Act
            var result = await _collectionController.DeleteCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task DeleteUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.DeleteCollectionAsync(It.IsAny<DeleteCollectionRequest>())).Throws(new Exception());

            //Act
            var requestModel = CreateDeleteCollectionRequest();
            var result = await _collectionController.DeleteCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region EditCollectionController
        [Fact]
        public async Task EditCollection_Success_ReturnOk_200()
        {
            //Arrange
            Collection mockResult = new Collection()
            {
                Id = Id,
                Name = "test",
                Description = "test2",
            };
            var requestModel = CreateEditCollectionRequest();

            _mockCollectionServices.Setup(x => x.EditCollectionAsync(It.IsAny<CollectionRequest>())).ReturnsAsync(mockResult);

            //Act
            var result = await _collectionController.EditCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task EditCollection_Failed_ThrowValidationException_ReturnBadRequest_400()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.EditCollectionAsync(It.IsAny<CollectionRequest>())).Throws(new BusinessException());
            var requestModel = CreateEditCollectionRequest();

            //Act
            var result = await _collectionController.EditCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, statusCodeResult.StatusCode);
        }
        [Fact]
        public async Task UpdateUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.EditCollectionAsync(It.IsAny<CollectionRequest>())).Throws(new Exception());

            //Act
            var requestModel = CreateEditCollectionRequest();
            var result = await _collectionController.EditCollection(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region GetDataAdminCollectionPageAsync
        [Fact]
        public async Task GetDataAdminCollectionPage_Success_ReturnOk_200()
        {
            //Arrange
            DataCollectionResponse mockResult = new DataCollectionResponse
            {
                CollectionData = new SearchCollectionResponse
                {
                    Paging = new PagingResponseBase
                    {
                        PageNumber = 1,
                        PageSize = 10,
                        SortBy = "Name",
                        SortOrder = "asc",
                        TotalItems = 30
                    },
                    CollectionList = CreateCollectionList()
                }
            };

            _mockCollectionServices.Setup(x => x.GetDataAdminCollectionPageAsync()).ReturnsAsync(mockResult);


            //Act
            var result = await _collectionController.GetDataCollectionPage();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task GetDataAdminCollectionPage_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.GetDataAdminCollectionPageAsync()).Throws(new Exception());
            //Act
            var result = await _collectionController.GetDataCollectionPage();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }


        #endregion

        #region SearchEmployeeUser
        [Fact]
        public async Task SearchEmployeeUser_Success_ReturnOk_200()
        {
            //Arrange
            SearchCollectionResponse mockResult = CreateSearchCollectionResponse();

            _mockCollectionServices.Setup(x => x.SearchCollectionAsync(It.IsAny<int>(), It.IsAny<int>()
                    , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(mockResult);


            //Act
            var result = await _collectionController.SearchCollection(null, null, null, null, null);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task SearchEmployeeUser_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            _mockCollectionServices.Setup(x => x.SearchCollectionAsync(It.IsAny<int>(), It.IsAny<int>()
                   , It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).ThrowsAsync(new Exception());
            //Act
            var result = await _collectionController.SearchCollection(1, 10, null, null, null);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region private
        private List<Collectioninfo> CreateCollectionList()
        {
            return new List<Collectioninfo>
               {
                   new Collectioninfo
                   {
                       Id = Guid.NewGuid(),
                       Name = "Name1",
                       Description = "test"
                   },
                   new Collectioninfo
                   {
                      Id = Guid.NewGuid(),
                       Name = "Name2",
                       Description = "test1"
                   },
               };
        }
        private SearchCollectionResponse CreateSearchCollectionResponse()
        {
            return new SearchCollectionResponse
            {
                Paging = new PagingResponseBase
                {
                    PageNumber = 1,
                    PageSize = 10,
                    SortBy = "Name",
                    SortOrder = "asc",
                    TotalItems = 30
                },
                CollectionList = CreateCollectionList()
            };
        }

        private DeleteCollectionReponse CreateDeleteCollectionResponse() 
        {
            return new DeleteCollectionReponse
            {
                PagingCollectionList = CreateSearchCollectionResponse(),
            };
        }
        private DeleteCollectionRequest CreateDeleteCollectionRequest()
        {
            return new DeleteCollectionRequest
            {
                Id = Id,
                PageNumber = 1,
                PageSize = 10,
                SortBy = "UserName",
                SortOrder = "asc",
                SearchInput = "",
            };
        }
        private CollectionRequest CreateEditCollectionRequest()
        {
            return new CollectionRequest
            {
                Id = Id,
                Name = "test3",
                Description = "test1",
            };
        }
        #endregion
    }
}