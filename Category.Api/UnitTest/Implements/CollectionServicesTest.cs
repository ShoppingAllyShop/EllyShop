using Catalog.Api.Implements;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.TestHelpers;
using Moq;
using Xunit;

namespace Catalog.Api.UnitTest.Implements
{
    public class CollectionServicesTest
    {
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<ILogger<CollectionService>> _mockLogger = new Mock<ILogger<CollectionService>>();

        private CollectionService CreateService()
        {
            return new CollectionService(
                _mockUnitOfWork.Object,
                _mockLogger.Object
                );
        }
        #region GetCollection
        [Fact]
        public async Task GetCollection_Success_ReturnData()
        {
            var service = CreateService();
            var colletionData = CreateColletionData();
            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(colletionData.MockDbSet().Object);

            //Act
            var result = await service.GetCollectionAsync();

            //Assert
            Assert.NotNull(result);
        }
        #endregion
        #region private
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