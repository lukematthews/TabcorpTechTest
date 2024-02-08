using TabcorpTechTest.Constants;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Services
{
    public class ReportService(ApiContext context) : IReportService
    {
        private readonly ApiContext _context = context;

        public List<CustomerCostTotalDto> GetCustomerCostTotals()
        {
            var totalsList = _context.Customers.Select(c => new CustomerCostTotalDto() { CustomerID = c.Id, TotalCost = 0 }).ToList();

            var totals = (from t in _context.Transactions
                          join c in _context.Customers on t.Customer.Id equals c.Id
                          group t by t.Customer
                         into g
                          select new CustomerCostTotalDto { CustomerID = g.Key.Id, TotalCost = g.Sum(t => t.GetCost()) });
            return totals.UnionBy(totalsList, x => x.CustomerID).ToList();
        }

        public List<ProductCostTotalDto> GetProductCostTotals()
        {
            var totalsList = _context.Products.Select(c => new ProductCostTotalDto() { ProductId = c.ProductCode, TotalCost = 0 }).ToList();

            var totals = (from t in _context.Transactions
                          join c in _context.Products on t.Product.ProductCode equals c.ProductCode
                          group t by t.Product
                         into g
                          select new ProductCostTotalDto { ProductId = g.Key.ProductCode, TotalCost = g.Sum(t => t.GetCost()) }).ToList();
            return totals.UnionBy(totalsList, x => x.ProductId).ToList();
        }

        public List<LocationCustomerStatsDto> GetTransactionCountForLocation(Location[] locations)
        {
            if (locations.Length == 0)
            {
                locations = [Location.Australia];
            }
            return (from l in locations
                    select (GetStatsForLocation(l)))
                    .ToList();
        }

        private LocationCustomerStatsDto GetStatsForLocation(Location location)
        {
            var totalsList = (from c in _context.Customers
                              where location == c.Location
                              select new CustomerCostTotalDto() { CustomerID = c.Id, TotalCost = 0, Count = 0 }).ToList();

            var customersInLocation = (from t in _context.Transactions
                                       where location == t.Customer.Location
                                       group t by t.Customer
                                      into g
                                       select new CustomerCostTotalDto() { CustomerID = g.Key.Id, Count = g.Count() }).ToList();
            List<CustomerCostTotalDto> customers = customersInLocation.UnionBy(totalsList, x => x.CustomerID).ToList();
            return new LocationCustomerStatsDto { Location = location, Customers = customers, TransactionCount = customers.Sum(c => c.Count) };
        }
    }
}
