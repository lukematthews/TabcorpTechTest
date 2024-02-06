﻿using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;
using TabcorpTechTest.Services;


namespace TabcorpTechnicalTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
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
        [HttpPost]
        public IActionResult Post([FromBody] TransactionDto value)
        {
            Transaction transaction = transactionService.ToTransaction(value);
            transactionService.SaveTransaction(transaction);
            return Ok();
        }
    }
}
