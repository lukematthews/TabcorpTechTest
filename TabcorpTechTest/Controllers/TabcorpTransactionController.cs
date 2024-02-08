using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Constants;
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

        [HttpGet]
        [Authorize(Roles = SecurityRoles.Admin)]
        public IEnumerable<TransactionDto> Get()
        {
            return transactionService.GetAllTransactions();
        }

        [HttpPost, Authorize(Roles = SecurityRoles.User)]
        public IActionResult Post([FromBody] TransactionDto value)
        {
            try
            {
                Transaction transaction = transactionService.ToTransaction(value);
                List<ValidationResult> validationResults = transactionService.ValidateTransaction(transaction);
                if (validationResults.Count > 0)
                {
                    return BadRequest(validationResults.Select(v => v.ErrorMessage).ToList());
                }
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
