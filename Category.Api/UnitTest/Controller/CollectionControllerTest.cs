using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CollectionModel.Response;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalog.Api.UnitTest.Controller
{
    public class CollectionControllerTest
    {
        private CollectionController _collectionController;
        private Mock<ICollections> _mockCollectionServices = new Mock<ICollections>();
        private Mock<ILogger<CollectionController>> _mockLogger = new Mock<ILogger<CollectionController>>();

        private readonly string CollectionName = "Gucci";

        public CollectionControllerTest()
        {
            _collectionController = new CollectionController(
               _mockCollectionServices.Object,
              _mockLogger.Object
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
    }
}
