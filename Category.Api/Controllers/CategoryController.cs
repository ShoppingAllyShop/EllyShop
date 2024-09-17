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
        [HttpGet("GetAll")]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _categoryServices.GetAll(); 
            return Ok(categoryList);
        }

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

        // POST api/<CategoryController>
        [Route("api/admin/[controller]")]
        [HttpPost("add")]
        public async Task<IActionResult> AddCategory([FromBody] AddCategoryRequest requestModel)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {                
                var result = await _categoryServices.AddCategoryAsync(requestModel);
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ApiResponseHelper.FormatError(ex.Message));
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
