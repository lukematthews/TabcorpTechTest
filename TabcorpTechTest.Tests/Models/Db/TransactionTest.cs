using TabcorpTechTest.Constants;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Tests.Models.Db
{

    public class TransactionTest
    {
        [Fact]
        public void TestCostCalculation()
        {
            Customer customer = new() { FirstName = "a", LastName = "b", Age = "c", Location = Location.Australia };
            Product product = new() { ProductCode = "product-1", Status = ProductStatus.Active, Cost = 10.10m };

            Transaction transaction = new() { Customer = customer, Product = product, Quantity = 0, TransactionTime = DateTime.Now };

            Assert.Equal(0m, transaction.GetCost());

            transaction.Quantity = 1;
            Assert.Equal(10.10m, transaction.GetCost());

            transaction.Quantity = 2;
            Assert.Equal(20.20m, transaction.GetCost());

            transaction.Product = null;
            Assert.Throws<InvalidOperationException>(() => transaction.GetCost());
        }
    }
}
