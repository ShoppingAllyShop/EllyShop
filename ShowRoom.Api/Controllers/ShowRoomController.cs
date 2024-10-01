using Comman.Domain.Models;
using CommonLib.Helpers;
using Microsoft.AspNetCore.Mvc;
using ShowRoom.Api.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShowRoom.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShowRoomController : ControllerBase
    {
        private readonly IShowRoom _showRoomServices;
        private readonly ILogger<ShowRoomController> _logger;
        public ShowRoomController(IShowRoom showRoomServices, ILogger<ShowRoomController> logger)
        {
            _showRoomServices = showRoomServices;
            _logger = logger;
        }

        // GET: api/<ShowRoomController>
        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            var showRoomList = await _showRoomServices.GetAll();
            return Ok(ApiResponseHelper.FormatSuccess(showRoomList));
        }

        // GET api/<ShowRoomController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ShowRoomController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ShowRoomController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ShowRoomController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
