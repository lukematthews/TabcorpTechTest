using Moq;
using Moq.EntityFrameworkCore;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;
using TabcorpTechTest.Services;

namespace TabcorpTechTest.Tests.Services
{
    public class ReportServiceTest
    {
        [Fact]
        public void TestGetCustomerCostTotals()
        {
            var mockContext = new Mock<ApiContext>();
            IEnumerable<Customer> customers = [
                new Customer() { Id = 10001, FirstName = "Tony", LastName = "Stark", Age = "tony.stark@gmail.com", Location = Location.Australia },
                new Customer() { Id = 10002, FirstName = "Bruce", LastName = "Banner", Age = "tony.stark@gmail.com", Location = Location.US },
                new Customer() { Id = 10003, FirstName = "Steve", LastName = "Rogers", Age = "tony.stark@gmail.com", Location = Location.Australia },
                new Customer() { Id = 10004, FirstName = "Wanda", LastName = "Maximoff", Age = "tony.stark@gmail.com", Location = Location.US },
                new Customer() { Id = 10005, FirstName = "Natasha", LastName = "Romanoff", Age = "tony.stark@gmail.com", Location = Location.Canada }];
            mockContext.Setup(m => m.Customers).ReturnsDbSet(customers);

            IEnumerable<Product> products = [
                new Product() { ProductCode = "PRODUCT_001", Cost = 50, Status = ProductStatus.Active },
                new Product() { ProductCode = "PRODUCT_002", Cost = 100, Status = ProductStatus.Inactive },
                new Product() { ProductCode = "PRODUCT_003", Cost = 200, Status = ProductStatus.Active },
                new Product() { ProductCode = "PRODUCT_004", Cost = 10, Status = ProductStatus.Inactive },
                new Product() { ProductCode = "PRODUCT_005", Cost = 500, Status = ProductStatus.Active },
            ];
            mockContext.Setup(m => m.Products).ReturnsDbSet(products);
            mockContext.Setup(m => m.Transactions).ReturnsDbSet([]);

            ReportService reportService = new(mockContext.Object);

            List<CustomerCostTotalDto> customerCostTotalDtos = reportService.GetCustomerCostTotals();
            // should contain all customers
            Assert.Equal(customers.Count(), customerCostTotalDtos.Count);
            // no transactions yet - everything is zero.
            foreach (CustomerCostTotalDto c in customerCostTotalDtos)
            {
                Assert.Equal(0, c.Count);
                Assert.Equal(0, c.TotalCost);
            }
        }
    }
}
