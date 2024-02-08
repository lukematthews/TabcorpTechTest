using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private ApiContext _context;

        public CustomerController(ApiContext context)
        {
            _context = context;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public IEnumerable<Customer> Get()
        {
            return _context.Customers.ToList();
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Customer Get(int id)
        {
            return _context.Customers.Find(id);
        }

        // POST api/<CustomerController>
        [HttpPost]
        public void Post([FromBody] Customer value)
        {
            _context.Customers.Add(value);
            _context.SaveChanges();
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] Customer value)
        {
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _context.Customers.Remove(_context.Customers.Find(id));
            _context.SaveChanges();
        }
    }
}
