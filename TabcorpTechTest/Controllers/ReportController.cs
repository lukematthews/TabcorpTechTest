using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Services;


namespace TabcorpTechnicalTest.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    [Authorize(Roles = SecurityRoles.Reports)]
    public class ReportController(IReportService reportService) : ControllerBase
    {
        private readonly IReportService reportService = reportService;

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

        [HttpGet("locationCustomerCounts"), Authorize(Roles = SecurityRoles.Reports)]
        public IActionResult GetTransactionCustomerCountsForAustralia([FromQuery] Location[] locations)
        {
            return Ok(reportService.GetTransactionCountForLocation(locations));
        }
    }
}
