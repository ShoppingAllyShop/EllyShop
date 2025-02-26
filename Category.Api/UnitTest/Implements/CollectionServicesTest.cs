using AutoMapper;
using Catalog.Api.Implements;
using Catalog.Api.Models.CategoryModel.Request;
using Catalog.Api.Models.CollectionModel.Request;
using Catalog.Api.Models.CollectionModel.Response;
using Catalog.Api.UnitTest.InitData;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using CommonLib.TestHelpers;
using Moq;
using Xunit;

namespace Catalog.Api.UnitTest.Implements
{
    public class CollectionServicesTest
    {
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<ILogger<CollectionService>> _mockLogger = new Mock<ILogger<CollectionService>>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();

        private readonly Guid Id = new Guid("67ff74b8-25c7-4065-aeef-2fd15a235255");

        private CollectionService CreateService()
        {
            return new CollectionService(
                _mockUnitOfWork.Object,
                _mockLogger.Object,
                _mockMapper.Object
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

        #region AddCollectionAsync
        [Fact]
        public async Task AddCollectionAsync_Success_ReturnName()
        {
            //Arrange
            var service = CreateService();
            var collectionListData = CreateColletionData();
            var request = new CollectionRequest
            {
                Id = Id,
                Name = "Test3",
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(collectionListData.MockDbSet().Object);

            //Act
            var result = await service.AddCollectionAsync(request);

            //Expected
            Assert.NotEqual(string.Empty, result);
        }

        [Fact]
        public async Task AddCollectionAsync_Failed_ThrowBusinessException()
        {
            //Arrange
            var service = CreateService();
            var collectionListData = CreateColletionData();
            var request = new CollectionRequest
            {
                Id = new Guid("67ff74b8-25c7-4065-aeef-2fd15a235255"),
                Name = "Túi Nữ",
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(collectionListData.MockDbSet().Object);

            //Expected
            await Assert.ThrowsAsync<BusinessException>(() => service.AddCollectionAsync(request));
        }
        #endregion

        #region editCollection
        [Fact]
        public async Task EditCollection_Success()
        {
            //Arrange
            var service = CreateService();
            var editCollectionData = CreateColletionData();
            var expectedMapperResult = new Collection
            {
                Id = new Guid("67ff74b8-25c7-4065-aeef-2fd15a235255"),
            };
            var request = new CollectionRequest
            {
                Id = Id,
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(editCollectionData.MockDbSet().Object);
            _mockMapper.Setup(m => m.Map<Collection>(It.IsAny<CollectionRequest>())).Returns(expectedMapperResult);

            //Act
            var result = await service.EditCollectionAsync(request);

            //Expected
            Assert.NotNull(result);
            Assert.IsType<Collection>(result);
        }
        [Fact]
        public async Task EditCollection_Failed_ThrowBusinessException()
        {
            //Arrange
            var service = CreateService();
            var editCollectionData = CreateColletionData();
            var request = new CollectionRequest
            {
                Id = Guid.NewGuid(),
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(editCollectionData.MockDbSet().Object);

            //Act
            await Assert.ThrowsAsync<BusinessException>(() => service.EditCollectionAsync(request));
        }
        #endregion

        #region deleteCollection
        [Fact]
        public async Task DeleteCollectionAsync_Success()
        {
            //Arranges
            var service = CreateService();
            var collectionList = CreateColletionData();
            var requestModel = new DeleteCollectionRequest
            {
                Id = Id,
                PageNumber = 1,
                PageSize = 10,
                SearchInput = ""
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(collectionList.MockDbSet().Object);

            //Act
            var result = await service.DeleteCollectionAsync(requestModel);

            //Assert
            Assert.NotNull(result);
            Assert.IsType<DeleteCollectionReponse>(result);
        }
        [Fact]
        public async Task DeleteCollectionAsync_Failed_ThrowBusinessException()
        {
            //Arranges
            var service = CreateService();
            var collectionList = CreateColletionData();
            var requestModel = new DeleteCollectionRequest
            {
                Id = Guid.NewGuid(),
                PageNumber = 1,
                PageSize = 10,
                SearchInput = ""
            };

            _mockUnitOfWork.Setup(x => x.Repository<Collection>()).Returns(collectionList.MockDbSet().Object);

            //Act
            await Assert.ThrowsAsync<BusinessException>(() => service.DeleteCollectionAsync(requestModel));
        }
        #endregion

        #region private
        private List<Collection> CreateColletionData()
        {
            return new List<Collection>
            {
                new Collection
                {
                    Id = Id,
                    Name = "Túi Nữ",
                    Description = "sản phẩm độc nhất"
                },
                new Collection
                {
                    Id = Id,
                    Name = "Túi Nam",
                    Description = "sản phẩm độc nhất 2"
                },
            };
        }
        #endregion
    }
}