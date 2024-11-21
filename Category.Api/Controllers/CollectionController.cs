using AutoMapper;
using Catalog.Api.Implements;
using Catalog.Api.Interfaces;
using Catalog.Api.Models.CollectionModel.Request;
using Category.Api.Controllers;
using Category.Api.Implements;
using Category.Api.Interfaces;
using CommonLib.Exceptions;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
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
        public CollectionController(ICollections collectionServices, ILogger<CollectionController> logger, IMapper mapper)
        {
            _collectionServices = collectionServices;
            _logger = logger;
        }
        [HttpGet("/api/admin/collection")]
        public async Task<IActionResult> GetDataCollectionPage()
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                _logger.LogInformation("Start get-collection API");
                var collectionList = await _collectionServices.GetDataAdminCollectionPageAsync();
                _logger.LogInformation($"Done get-collection api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(collectionList));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetCollection api failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"GetCollection api failed.There is a exception!"));
            }

        }
        [HttpGet("get-collection")]
        public async Task<IActionResult> GetCollection()
        {
            try
            {
                var stopwatch = Stopwatch.StartNew();
                _logger.LogInformation("Start get-collection API");
                var collectionList = await _collectionServices.GetCollectionAsync();
                _logger.LogInformation($"Done get-collection api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(collectionList));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "GetCollection api failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"GetCollection api failed.There is a exception!"));
            }

        }
        [Route("/api/admin/collection/add")]
        [HttpPost]
        public async Task<IActionResult> AddCollection([FromBody] CollectionRequest requestModel)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start AddCategory API");
                var result = await _collectionServices.AddCollectionAsync(requestModel);
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
                return StatusCode(500, ApiResponseHelper.FormatError($"AddCollection api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Add new collection operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
        [Route("/api/admin/collection/edit")]
        [HttpPost()]
        public async Task<IActionResult> EditCollection([FromBody] CollectionRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start EditCollection API");
                var result = await _collectionServices.EditCollectionAsync(request);
                _logger.LogInformation($"Done edit collection api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                _logger.LogWarning(ex.Message);
                return BadRequest(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"EditCollection api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Edit collection operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        [Route("/api/admin/collection/delete")]
        [HttpPost]
        public async Task<IActionResult> DeleteCollection([FromBody] DeleteCollectionRequest request)
        {
            var stopwatch = Stopwatch.StartNew();
            try
            {
                _logger.LogInformation("Start DeleteCollection API");
                var result = await _collectionServices.DeleteCollectionAsync(request);
                _logger.LogInformation($"Done delete collection api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }
            catch (BusinessException ex)
            {
                return BadRequest(ApiResponseHelper.FormatError(ex.Message));
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"DeleteCollection api failed.There is a exception!"));
            }
            finally
            {
                _logger.LogInformation($"Delete collection operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        [Route("/api/admin/collection/search-collection")]
        [HttpGet()]
        public async Task<IActionResult> SearchCollection(int? pageNumber, int? pageSize, string? sortBy,
                                                            string? sortOrder, string? searchInput)
        {
            try
            {
                _logger.LogInformation($"Start search collection");
                var stopwatch = Stopwatch.StartNew();

                var result = await _collectionServices.SearchCollectionAsync(pageNumber, pageSize, sortBy, sortOrder, searchInput);
                _logger.LogInformation($"Create account successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(new { CollectionData = result }));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Call SearchEmployeeUser Api was failed.There is a exception!");
                return StatusCode(500, ApiResponseHelper.FormatError($"Call Api SearchEmployeeUser thất bại"));
            }
        }
    }
}
