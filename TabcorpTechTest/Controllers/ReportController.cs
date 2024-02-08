using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Services;


namespace TabcorpTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class ReportController(IReportService reportService) : ControllerBase
    {
        private readonly IReportService reportService = reportService;

        // GET api/<ReportController>
        [HttpGet("customerCostTotals")]
        public IActionResult GetCustomerTotalCosts()
        {
            return Ok(reportService.GetCustomerCostTotals());
        }

        [HttpGet("productCostTotals")]
        public IActionResult GetProductTotalCosts()
        {
            return Ok(reportService.GetProductCostTotals());
        }

        [HttpGet("locationCustomerCounts")]
        public IActionResult GetTransactionCustomerCountsForAustralia([FromQuery] Location[] locations)
        {
            return Ok(reportService.GetTransactionCountForLocation(locations));
        }


    }
}
