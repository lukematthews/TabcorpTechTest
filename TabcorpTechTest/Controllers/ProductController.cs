using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TabcorpTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = SecurityRoles.Admin)]

    public class ProductController : ControllerBase
    {
        private ApiContext _context;

        public ProductController(ApiContext context)
        {
            _context = context;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            return product == null ? BadRequest() : Ok(product);
        }

        // POST api/<ProductController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            _context.SaveChanges();
        }
    }
}
