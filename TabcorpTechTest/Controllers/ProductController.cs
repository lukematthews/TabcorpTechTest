using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TabcorpTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private ApiContext _context;

        public ProductController(ApiContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _context.Products.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var product = _context.Products.Find(id);
            return product == null ? BadRequest() : Ok(product);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] Product value)
        {
            _context.Products.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Product value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            _context.SaveChanges();
        }
    }
}
