using System.Net.Http.Json;
using TabcorpTechTest.Models.Dto;
using Xunit.Abstractions;

namespace TabcorpTechTest.Tests.Controllers
{
    public class TabcorpTransactionControllerLoadTest
    {
        private readonly ITestOutputHelper output;

        public TabcorpTransactionControllerLoadTest(ITestOutputHelper output)
        {
            this.output = output;
        }

        [Fact]
        public void testPostTransactionSpeed()
        {
            var count = 50000;
            HttpClient client = new HttpClient();
            var transactions = new List<TransactionDto>();
            for (int i = 0; i < count; i++)
            {
                transactions.Add(new TransactionDto() { CustomerId = 10001, ProductCode = "PRODUCT_001", Quantity = 1, TransactionTime = "2024/02/06 13:53" });
            }

            transactions.ForEach(async t => await client.PostAsJsonAsync("https://localhost:7238/TabcorpTransaction", t));
        }

    }
}
