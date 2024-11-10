using Catalog.Api.Controllers;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.ProductDetailModel.Response;
using Catalog.Api.Models.ProductModel;
using Catalog.Api.Models.ProductModel.Respone;
using Comman.Domain.Elly_Catalog;
using Comman.Domain.Models;
using Common.Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Catalog.Api.UnitTest.Controller
{
    public class ProductControllerTest
    {
        private ProductController _productController;
        private Mock<IUnitOfWork<Elly_CatalogContext>> _mockUnitOfWork = new Mock<IUnitOfWork<Elly_CatalogContext>>();
        private Mock<IProduct> _mockProductServices = new Mock<IProduct>();
        private Mock<ILogger<ProductController>> _mockLogger = new Mock<ILogger<ProductController>>();

        private readonly string ProductId1 = "EC001";
        private readonly string ProductName1 = "Sản phẩm 1 - EC001";

        private readonly Guid newProducts = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid bestSellers = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid favourites = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        private readonly Guid promotions = new Guid("af24e5b0-a03d-4301-b54d-2229867604a9");
        public ProductControllerTest()
        {
            _productController = new ProductController(
              _mockUnitOfWork.Object,
              _mockProductServices.Object,
              _mockLogger.Object);
        }
        //#region Product_Success
        //[Fact]
        //public void Product_Success_ReturnOk_200()
        //{
        //    //Arrange
        //    //var service = CreateService();
        //    //var productListData = CreateProductList();
        //    ProductListByTagModel mockResult = new ProductListByTagModel
        //    {
        //        ProductId = ProductId1,
        //        ProductName = ProductName1,
        //        Price = 1000,
        //        Discount = 500,
        //        PercentDiscount = 50,
        //        ProductImages = new List<ProductImages>
        //            {
        //                new ProductImages
        //                  {
        //                       Id = Guid.NewGuid(),
        //                       ImageIndex = 1,
        //                       Picture = "hinh 1",
        //                       ProductId = ProductId1
        //                  },
        //                new ProductImages
        //                  {
        //                       Id = Guid.NewGuid(),
        //                       ImageIndex = 2,
        //                       Picture = "hinh 2",
        //                       ProductId = ProductId1
        //                  }
        //            },
        //        CreatedAt = DateTime.Now,
        //        UpdatedAt = DateTime.Now,
        //        TagDescription = "Description",
        //        TagName = "Name",
        //        TagTitle = "Title",
        //    };
        //    _mockProductServices.Setup(x => x.GetMainPageProduct(It.IsAny<MainPageProductResponse>())).ReturnsAsync(mockResult);


        //    var result = _productController.GetMainpageData();

        //    //Assert
        //    var statusCodeResult = Assert.IsType<OkObjectResult>(result);
        //    Assert.Equal(200, statusCodeResult.StatusCode);
        //}
        //#endregion

        #region CreateProductList

        #endregion
    }
}
