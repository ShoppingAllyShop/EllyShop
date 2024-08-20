using Category.Api.Interfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Category.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategory _categoryServices;
        public CategoryController(ICategory categoryServices)
        {
            _categoryServices = categoryServices;
        }

        // GET: api/<CategoryController>
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var categoryList = await _categoryServices.GetAll(); 
            return Ok(categoryList);
        }

        // GET api/<CategoryController>/5
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
