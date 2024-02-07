using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Dto
{
    public class LocationCustomerStatsDto
    {
        public Location location { get; set; }
        public List<CustomerCostTotalDto> customers { get; set; }
    }
}
