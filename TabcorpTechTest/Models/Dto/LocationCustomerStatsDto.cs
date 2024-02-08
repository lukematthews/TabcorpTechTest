using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Dto
{
    public class LocationCustomerStatsDto
    {
        public Location Location { get; set; }
        public List<CustomerCostTotalDto> Customers { get; set; }
        public decimal TransactionCount { get; set; }
    }
}
