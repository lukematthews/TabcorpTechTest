using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Dto;
using System.Linq;
using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Services
{
    public class ReportService(ApiContext context) : IReportService
    {
        private readonly ApiContext _context = context;

        public List<CustomerCostTotalDto> GetCustomerCostTotals()
        {
            var totalsList = _context.Customers.Select(c => new CustomerCostTotalDto() { CustomerID= c.Id, TotalCost = 0}).ToList();

            var totals = from t in _context.Transactions
                         join c in _context.Customers on t.Customer.Id equals c.Id
                         group t by t.Customer
                         into g
                         select new CustomerCostTotalDto { CustomerID = g.Key.Id, TotalCost = g.Sum(t => t.Quantity * t.Product.Cost) };
            return totals.ToList().UnionBy(totalsList, x => x.CustomerID).ToList();
        }

        public List<ProductCostTotalDto> GetProductCostTotals()
        {
            var totalsList = _context.Products.Select(c => new ProductCostTotalDto() { ProductId = c.ProductCode, TotalCost = 0 }).ToList();

            var totals = from t in _context.Transactions
                         join c in _context.Products on t.Product.ProductCode equals c.ProductCode
                         group t by t.Product
                         into g
                         select new ProductCostTotalDto { ProductId = g.Key.ProductCode, TotalCost = g.Sum(t => t.Quantity * t.Product.Cost) };
            return totals.ToList().UnionBy(totalsList, x => x.ProductId).ToList();
        }

        public List<CustomerCostTotalDto> GetTransactionCountForLocation(Location location)
        {
            var totalsList = from c in _context.Customers
                             where c.Location == location
                             select new CustomerCostTotalDto() { CustomerID = c.Id, TotalCost = 0 };

            var customersInLocation = from t in _context.Transactions
                                      where t.Customer.Location == location
                                      group t by t.Customer
                                      into g
                                      select new CustomerCostTotalDto() { CustomerID = g.Key.Id, Count = g.Count() };
            return customersInLocation.ToList().UnionBy(totalsList.ToList(), x => x.CustomerID).ToList();
        }
    }
}
