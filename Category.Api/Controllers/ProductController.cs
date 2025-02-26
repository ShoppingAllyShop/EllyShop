using Catalog.Api.Implements;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.ProductDetailModel.Response;
using Category.Api.Controllers;
using Category.Api.Interfaces;
using Comman.Domain.Elly_Catalog;
using Common.Infrastructure;
using Common.Infrastructure.Interfaces;
using CommonLib.Exceptions;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProduct _productServices;
        private readonly IUnitOfWork<Elly_CatalogContext> _unitOfWork;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IUnitOfWork<Elly_CatalogContext> unitOfWork,IProduct productServices, ILogger<ProductController> logger)
        {
            _unitOfWork = unitOfWork;
            _productServices = productServices;
            _logger = logger;
        }
        //// GET: api/<ProductContent>
        //[HttpGet("get-product-ById")]
        //public async Task<IActionResult> GetProductById(string id)
        //{
        //    var stopwatch = Stopwatch.StartNew();
        //    try
        //    {
        //        var product = await _productServices.GetProductAsync(id);
        //        //var productSerialize = JsonConvert.SerializeObject(product,
        //        //    Newtonsoft.Json.Formatting.Indented,
        //        //    new JsonSerializerSettings()
        //        //    {
        //        //        ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        //        //    }
        //        //  );
        //        //Product ClientDeserialize = JsonConvert.DeserializeObject<Product>(productSerialize);

        //        return Ok(ApiResponseHelper.FormatSuccess(product));
        //    }
        //    catch (BusinessException ex)
        //    {
        //        _logger.LogError(ex.Message);
        //        return BadRequest(ex.Message);
        //    }
        //    catch (Exception e)
        //    {
        //        _logger.LogError(e.Message);
        //        return BadRequest($"Quá trình thêm danh mục xảy ra lỗi");
        //    }
        //    finally
        //    {
        //        _logger.LogInformation($"Get product by {id} operation took {stopwatch.ElapsedMilliseconds} ms.");
        //    }
        //}

        // GET: api/<ProductContent>
        [HttpGet("get-product-detail")]
        public async Task<IActionResult> GetProductDetail(string id)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start get-product-detail API");
                var result = await _productServices.GetProductDetailAsync(id);
                _logger.LogInformation($"Done get-product-detail api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"GetProductDetail api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Get product operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }


        [HttpGet("get-mainpage-data")]
        public IActionResult GetMainpageData()
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start get-mainpage-data API");
                var result =  _productServices.GetMainPageProduct();
                _logger.LogInformation($"Done get-mainpage-data api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"GetMainpageData api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Get product operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
