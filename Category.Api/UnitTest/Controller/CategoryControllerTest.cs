using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CategoryModel.Request;
using Catalog.Api.Models.ProductModel.Respone;
using Category.Api.Controllers;
using Category.Api.Interfaces;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using static CommonLib.Constants.AppEnums;
using CategoryEntity = Comman.Domain.Elly_Catalog.Category;

namespace Catalog.Api.UnitTest.Controller
{
    public class CategoryControllerTest
    {
        private CategoryController _categoryController;
        private Mock<ICategory> _mockCategoryServices = new Mock<ICategory>();
        private Mock<ILogger<CategoryController>> _mockLogger = new Mock<ILogger<CategoryController>>();

        public CategoryControllerTest()
        {
            _categoryController = new CategoryController(
              _mockCategoryServices.Object,
              _mockLogger.Object
              );
        }

        #region CategoryController
        [Fact]
        public async Task CategoryController_GetAll_Success_ReturnOk_200()
        {
            //Arrange
            var mockResult = new List<CategoryEntity>
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
                }
             };

            _mockCategoryServices.Setup(x => x.GetAllAsync()).ReturnsAsync(mockResult);

            //Action
            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CategoryController_GetAll_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockCategoryServices.Setup(x => x.GetAllAsync()).Throws(new Exception());


            var result = await _categoryController.GetAll();

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region CategoryController_AddCategory
        [Fact]
        public async Task CategoryController_AddCategory_Success_ReturnOk_200()
        {
            //Arrange

            var returnAsync = "abcdef";

            _mockCategoryServices.Setup(x => x.AddCategoryAsync(It.IsAny<CategoryRequest>())).ReturnsAsync(returnAsync);

            //Act
            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };

            var result = await _categoryController.AddCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CategoryController_AddCategory_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockCategoryServices.Setup(x => x.AddCategoryAsync(It.IsAny<CategoryRequest>())).Throws(new Exception());

            //Act
            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };
            var result = await _categoryController.AddCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region CategoryController_DeleteCategory
        [Fact]
        public async Task CategoryController_DeleteCategory_Success_ReturnOk_200()
        {
            //Arrange
            var DeleteCateId = Guid.NewGuid();
            var DeleteCateName = "abc";

            _mockCategoryServices.Setup(x => x.DeleteCategoryAsync(DeleteCateId, DeleteCateName)).ReturnsAsync("absse");

            //Act
            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };

            var result = await _categoryController.DeleteCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CategoryController_DeleteCategory_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange
            var DeleteCateId = Guid.NewGuid();
            var DeleteCateName = "";

            _mockCategoryServices.Setup(x => x.DeleteCategoryAsync(DeleteCateId, DeleteCateName)).Throws(new Exception());

            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };

            var result = await _categoryController.DeleteCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

        #region CategoryController_EditCategory
        [Fact]
        public async Task CategoryController_EditCategory_Success_ReturnOk_200()
        {
            //Arrange
            var mockResult = new CategoryEntity
            {
                   Id = Guid.NewGuid(),
                   CategoryLevel = 1,
                   Gender = true,
                   Name = "testbale2",
                   ParentId = Guid.NewGuid(),
            };

            _mockCategoryServices.Setup(x => x.EditCategoryAsync(It.IsAny<CategoryRequest>())).ReturnsAsync(mockResult);

            //Act
            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };

            var result = await _categoryController.EditCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, statusCodeResult.StatusCode);
        }

        [Fact]
        public async Task CategoryController_EditCategory_Failed_ThrowException_ReturnInternalServerError_500()
        {
            //Arrange

            _mockCategoryServices.Setup(x => x.EditCategoryAsync(It.IsAny<CategoryRequest>())).Throws(new Exception());

            var requestModel = new CategoryRequest
            {
                Id = Guid.NewGuid(),
                CategoryLevel = 1,
                Name = "abc",
                Gender = true,
                ParentId = Guid.NewGuid()
            };

            var result = await _categoryController.EditCategory(requestModel);

            //Assert
            var statusCodeResult = Assert.IsType<ObjectResult>(result);
            Assert.Equal(500, statusCodeResult.StatusCode);
        }
        #endregion

    }
}
