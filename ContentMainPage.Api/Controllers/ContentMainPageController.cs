using ContentMainPage.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;
using CommonLib.Helpers;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ContentMainPage.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContentMainPageController : ControllerBase
    {
        private readonly IContentMainPage _contentMainPageServices;
        private readonly ILogger<ContentMainPageController> _logger;
        public ContentMainPageController(IContentMainPage contentManagement, ILogger<ContentMainPageController> logger)
        {
            _contentMainPageServices = contentManagement;
            _logger = logger;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var showRoomList = await _contentMainPageServices.GetAll();
            return Ok(ApiResponseHelper.FormatSuccess(showRoomList));
        }
        // GET: api/<ContentMainPageController>
        [HttpGet("get-mainpage")]
        public IActionResult GetMainPage()
        {
            var result = _contentMainPageServices.GetContentMainPage();
            return Ok(ApiResponseHelper.FormatSuccess(result));
        }

        // GET api/<ContentMainPageController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ContentMainPageController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ContentMainPageController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ContentMainPageController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
