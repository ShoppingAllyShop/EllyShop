using Catalog.Api.Implements;
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
    public class CollectionController : ControllerBase
    {
        private readonly ICollections _collectionServices;
        private readonly ILogger<CollectionController> _logger;
        public CollectionController(ICollections collectionServices, ILogger<CollectionController> logger)
        {
            _collectionServices = collectionServices;
            _logger = logger;
        }

        [HttpGet("get-collection")]
        public async Task<IActionResult> GetCollection()
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                _logger.LogInformation("Start get-collection API");
                var collectionList = await _collectionServices.GetCollection();
                _logger.LogInformation($"Done get-collection api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(collectionList));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetCollection api failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"GetCollection api failed.There is a exception!"));
            }
           
        }
    }
}
