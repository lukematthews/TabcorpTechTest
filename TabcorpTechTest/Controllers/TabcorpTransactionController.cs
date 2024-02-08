using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;
using TabcorpTechTest.Services;


namespace TabcorpTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Authorize]

    public class TabcorpTransactionController(ITransactionService transactionService) : ControllerBase
    {
        private readonly ITransactionService transactionService = transactionService;

        // GET: api/<TabcorpTransactionController>
        [HttpGet]
        public IEnumerable<TransactionDto> Get()
        {
            return transactionService.GetAllTransactions();
        }

        // POST api/<TabcorpTransactionController>
        [HttpPost, Authorize(Roles ="admin")]
        public IActionResult Post([FromBody] TransactionDto value)
        {
            try
            {
                Transaction transaction = transactionService.ToTransaction(value);
                transactionService.SaveTransaction(transaction);
            }
            catch (Exception e)
            {
                if (e is CustomerNotFoundException || e is ProductNotFoundException)
                {
                    return BadRequest();
                }
                else
                {
                    throw;
                }

            }
            return Ok();
        }
    }
}
