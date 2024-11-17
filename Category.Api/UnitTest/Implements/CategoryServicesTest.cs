using AutoMapper;
using Catalog.Api.Implements;
using Catalog.Api.Models.CategoryModel.Request;
using Category.Api.Implements;
using Comman.Domain.Elly_Catalog;
using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using CommonLib.TestHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestPlatform.ObjectModel.DataCollection;
using Moq;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using Xunit;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.UnitTest.Implements
{
    public class CategoryServicesTest
    {
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<ILogger<CategoryServices>> _mockLogger = new Mock<ILogger<CategoryServices>>();
        private Mock<IMapper> _mockMapper = new Mock<IMapper>();
        
        private CategoryServices CreateService()
        {
            return new CategoryServices(
                _mockUnitOfWork.Object,
                _mockLogger.Object,
                _mockMapper.Object
                );
        }

        #region AddCategoryAsync
        [Fact]
        public async Task AddCategoryAsync_Success_ReturnName()
        {
            //Sua lai
            //Arrange
            var service = CreateService();
            var categoryListData = CreateCategoryListData();
            var request = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "Test3",
                Gender = true
            };

            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(categoryListData.MockDbSet().Object);

            //Act
            var result = await service.AddCategoryAsync(request);

            //Expected
            Assert.NotEqual(string.Empty, result);
        }


        [Fact]
        public async Task AddCategoryAsync_Failed_ExistCategory_ThrowException()
        {
            //Arrange
            var service = CreateService();
            var categoryListData = CreateCategoryListData();
            var request = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "Test1",
                Gender = true
            };

            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(categoryListData.MockDbSet().Object);

            //Expected
            await Assert.ThrowsAsync<BusinessException>(() => service.AddCategoryAsync(request));
        }
        #endregion

        #region DeleteCategoryAsync

        [Fact]
        public async Task DeleteCategoryAsync_Success_ReturnName()
        {
            //Arrange
            var service = CreateService();
            var deleteCategoryData = CreateDeleteCategoryData();
            var request = new CategoryRequest
            {
                Id = new Guid("b7d1e464-bfe8-47b7-8838-e57d73935d0a"),
                Name = "Test1",

            };
            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(deleteCategoryData.MockDbSet().Object);

            //Act
            var result = await service.DeleteCategoryAsync(request.Id, request.Name);

            //Expected
            Assert.NotEmpty(result);
        }

        [Fact]
        public async Task DeleteCategoryAsync_Failed_NotExistCategory_ReturnException()
        {
            //Arrange
            var service = CreateService();
            var deleteCategoryData = CreateDeleteCategoryData();
            var request = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                Name = "Test1",

            };
            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(deleteCategoryData.MockDbSet().Object);          

            //Act && Expected
            await Assert.ThrowsAsync<BusinessException>(() => service.DeleteCategoryAsync(request.Id, request.Name));
        }
        #endregion

        #region EditCategoryAsync
        [Fact]
        public async Task EditCategoryAsync_Success_ReturnCategory()
        {
            //Arrange
            var service = CreateService();
            var editCategoryData = CreateEditCategoryData();
            var expectedMapperResult = new CategoryEntity 
            {
                Id = new Guid("bb0a48f8-2afa-4269-9073-c62db36a91d2"),
            };
            var request = new CategoryRequest
            {
                Id = new Guid("bb0a48f8-2afa-4269-9073-c62db36a91d2"),
            };

            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(editCategoryData.MockDbSet().Object);
            _mockMapper.Setup(m => m.Map<CategoryEntity>(It.IsAny<CategoryRequest>())).Returns(expectedMapperResult);

            //Act
            var result = await service.EditCategoryAsync(request);

            //Expected
            Assert.NotNull(result);
            Assert.IsType<CategoryEntity>(result);
        }

        [Fact]
        public async Task EditCategoryAsync_Failed_NotExistCategory_ReturnException()
        {
            //Arrange
            var service = CreateService();
            var editCategoryData = CreateEditCategoryData();
            var request = new CategoryRequest
            {
                Id = Guid.NewGuid(),
            };

            _mockUnitOfWork.Setup(x => x.Repository<CategoryEntity>()).Returns(editCategoryData.MockDbSet().Object);

            //Expected
            await Assert.ThrowsAsync<BusinessException>(() => service.EditCategoryAsync(request));
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
                    Name = "Test1",
                    Gender = true,

                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    CategoryLevel = 1,
                    Name = "Test22",
                    Gender = true,
                }
            };
        }
        private List<CategoryEntity> CreateDeleteCategoryData()
        {
            return new List<CategoryEntity>
            {
                new CategoryEntity
                {
                    Id = new Guid("b7d1e464-bfe8-47b7-8838-e57d73935d0a"),
                    Name = "Test1",

                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    Name = "Test1",
                }
            };
        }
     
        private List<CategoryEntity> CreateEditCategoryData()
        {
            return new List<CategoryEntity>
            {
                new CategoryEntity
                {
                   
                    Id = new Guid("bb0a48f8-2afa-4269-9073-c62db36a91d2"),
                    CategoryLevel = 1,
                    Name = "Test3",
                    ParentId = Guid.NewGuid(),
                    Gender = true

                },
                new CategoryEntity
                {
                    Id = Guid.NewGuid(),
                    CategoryLevel = 1,
                    Name = "Test1",
                    ParentId = Guid.NewGuid(),
                    Gender = true
                }
            };
        }
        #endregion
    }
}
