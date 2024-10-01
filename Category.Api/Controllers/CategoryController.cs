using Category.Api.Interfaces;
using Category.Api.Models.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Enums;
using CommonLib.Exceptions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using CommonLib.Helpers;
using System.Diagnostics;
//using System.Text.Json;
//using System.Text.Json.Serialization;
using System.Net.Http.Json;
using System.Xml;
using Newtonsoft.Json;
using Comman.Domain.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Category.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryServices;
        private readonly ILogger<CategoryController> _logger;
        public CategoryController(ICategory categoryServices, ILogger<CategoryController> logger)
        {
            _categoryServices = categoryServices;
            _logger = logger;
        }

        // GET: api/<CategoryController>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _categoryServices.GetAll();
            return Ok(ApiResponseHelper.FormatSuccess(categoryList));
        }


  //      // GET: api/<ProductCont>
  //      [HttpGet("get-product")]
  //      public async Task<IActionResult> GetProduct(string id)
  //      {
  //          var stopwatch = Stopwatch.StartNew();
  //          try
  //          {
  //              var product = await _categoryServices.GetProductAsync(id);

  //              var productSerialize = JsonConvert.SerializeObject(product,
  //     Newtonsoft.Json.Formatting.Indented,
  //     new JsonSerializerSettings()
  //     {
  //         ReferenceLoopHandling = ReferenceLoopHandling.Ignore
  //     }
  //);
  //              Product ClientDeserialize = JsonConvert.DeserializeObject<Product>(productSerialize);
                
  //              return Ok(ApiResponseHelper.FormatSuccess(ClientDeserialize));
  //          }
  //          catch (BusinessException ex)
  //          {
  //              _logger.LogError(ex.Message);
  //              return BadRequest(ex.Message);
  //          }
  //          catch (Exception e)
  //          {
  //              _logger.LogError(e.Message);
  //              return BadRequest($"Quá trình thêm danh mục xảy ra lỗi");
  //          }
  //          finally
  //          {
  //              _logger.LogInformation($"Get product by {id} operation took {stopwatch.ElapsedMilliseconds} ms.");
  //          }
  //      }

        //GET api/<CategoryController>/5
        [HttpGet("GetById")]
        public async Task<IActionResult> GetById(Guid id)
        {
            return Ok("value");
        }

        // GET api/<CategoryController>/5
        [HttpGet("GetByName")]
        public async Task<IActionResult> GetByName(string id)
        {
            return Ok("value");
        }


        [Route("/api/admin/category/add")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest requestModel)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = await _categoryServices.AddCategoryAsync(requestModel);
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                _logger.LogError(ex.Message);
                return Ok(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Quá trình thêm danh mục xảy ra lỗi");
            }
            finally
            {
                _logger.LogInformation($"Add new category operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        [Route("/api/admin/category/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCategory([FromBody] CategoryRequest req)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = await _categoryServices.DeleteCategoryAsync(req.Id, req.Name);
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Quá trình xóa danh mục xảy ra lỗi");
            }
            finally
            {
                _logger.LogInformation($"Add new category operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        [Route("/api/admin/category/edit")]
        [HttpPost]
        public async Task<IActionResult> EditCategory([FromBody] CategoryRequest req)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                var result = await _categoryServices.EditCategoryAsync(req);
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex.Message);
                return Ok(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Quá trình chỉnh sửa danh mục xảy ra lỗi");
            }
            finally
            {
                _logger.LogInformation($"Edit category operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
        // POST api/<CategoryController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
