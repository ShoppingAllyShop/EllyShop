using Catalog.Api.Interfaces;
using Category.Api.Controllers;
using Category.Api.Implements;
using Category.Api.Interfaces;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Catalog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly ICatalog _catalogServices;
        private readonly ILogger<CatalogController> _logger;
        public CatalogController(ICatalog catalogServices, ILogger<CatalogController> logger)
        {
            _catalogServices = catalogServices;
            _logger = logger;
        }

        // GET: api/<CategoryController>
        [HttpGet("main-page-content")]
        public  async Task<IActionResult> GetMainPageContent()
        {
            var stopwatch = Stopwatch.StartNew();
            _logger.LogInformation("Start GetMainPageContent.....");
            try
            {
                var catalogList = await _catalogServices.GetMainPageContent();
                _logger.LogInformation("GetMainPageContent successfully");
                return Ok(ApiResponseHelper.FormatSuccess(catalogList));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Quá trình GetMainPageContent xảy ra lỗi");
            }
            finally
            {
                _logger.LogInformation($"Get MainPage Content operation took {stopwatch.ElapsedMilliseconds} ms.");
            }

        }

        // GET: api/<CatalogController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<CatalogController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<CatalogController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<CatalogController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<CatalogController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
