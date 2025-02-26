using Category.Api.Interfaces;
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
using Catalog.Api.Interfaces;
using Catalog.Api.Implements;
using Catalog.Api.Models.CategoryModel.Request;

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
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start get-all Category API");
                var categoryList = await _categoryServices.GetAllAsync();
                _logger.LogInformation($"Done get-all category api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(categoryList));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"GetAll api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($" Get-all category api operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }


        [Route("/api/admin/category/add")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CategoryRequest requestModel)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start AddCategory API");
                var result = await _categoryServices.AddCategoryAsync(requestModel);
                _logger.LogInformation($"Done add category api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
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
                return StatusCode(500, ApiResponseHelper.FormatError($"AddCategory api failed.There is a exception!"));
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
                _logger.LogInformation("Start DeleteCategory API");
                var result = await _categoryServices.DeleteCategoryAsync(req.Id, req.Name);
                _logger.LogInformation($"Done delete category api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"DeleteCategory api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Delete category operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        [Route("/api/admin/category/edit")]
        [HttpPost]
        public async Task<IActionResult> EditCategory([FromBody] CategoryRequest req)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start EditCategory API");
                var result = await _categoryServices.EditCategoryAsync(req);
                _logger.LogInformation($"Done edit category api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
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
                return StatusCode(500, ApiResponseHelper.FormatError($"EditCategory api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Edit category operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
