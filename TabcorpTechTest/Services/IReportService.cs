using TabcorpTechTest.Constants;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Services
{
    public interface IReportService
    {
        public List<CustomerCostTotalDto> GetCustomerCostTotals();

        public List<ProductCostTotalDto> GetProductCostTotals();

        public List<LocationCustomerStatsDto> GetTransactionCountForLocation(Location[] locations);
    }
}
