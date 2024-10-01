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
                var result = _contentManagementServices.GetContentMainPage();              
                return Ok(ApiResponseHelper.FormatSuccess(result));
            }           
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return BadRequest($"Quá trình load data main page xảy ra lỗi");
            }
            finally
            {
                stopwatch.Stop();
                _logger.LogInformation($"Get main page content operation took {stopwatch.ElapsedMilliseconds} ms.");
            }
        }

        //// GET: api/<ContentMainPageController>
        //[HttpGet("get-mainpage")]
        //public IActionResult GetMainPage()
        //{
        //    var result = _contentManagementServices.GetContentMainPage();
        //    return Ok(ApiResponseHelper.FormatSuccess(result));
        //}

        // GET api/<ContentManagementController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContentManagementController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContentManagementController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContentManagementController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
