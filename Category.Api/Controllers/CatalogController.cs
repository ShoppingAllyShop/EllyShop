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
            try
            {
                _logger.LogInformation("Start GetMainPageContent.....");
                var catalogList = await _catalogServices.GetMainPageContentAsync();
                _logger.LogInformation("GetMainPageContent successfully");
                return Ok(ApiResponseHelper.FormatSuccess(catalogList));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"GetMainPageContent api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Get MainPage Content operation took {stopwatch.ElapsedMilliseconds} ms.");
            }

        }
    }
}
