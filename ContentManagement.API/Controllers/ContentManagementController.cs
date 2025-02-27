using ContentManagement.API.Implements;
using ContentManagement.API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Comman.Domain.Models;
using CommonLib.Helpers;
using System.Diagnostics;
using CommonLib.Exceptions;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContentManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentManagementController : ControllerBase
    {
        private readonly IContentManagement _contentManagementServices;
        private readonly ILogger<ContentManagementController> _logger;
        public ContentManagementController(IContentManagement contentManagement, ILogger<ContentManagementController> logger)
        {
            _contentManagementServices = contentManagement;
            _logger = logger;
        }

        // GET: api/<ContentManagementController>
        [HttpGet("main-page-content")]
        public IActionResult GetMainPageContent()
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();  // Bắt đầu tính thời gian
            try
            {               
                _logger.LogInformation("Start main-page-content API");
                var result = _contentManagementServices.GetContentMainPage();
                _logger.LogInformation($"Done main-page-content api successfully.It took {stopwatch.ElapsedMilliseconds} ms to complete.");
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }           
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(500, ApiResponseHelper.FormatError($"main-page-content api failed.There is a exception!"));
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"Get main page content operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }
    }
}
