using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TabcorpTechTest.Models.Db;

namespace TabcorpTechTest.Models.Dto
{
    public class TransactionDto
    {
        public string ProductCode { get; set; } = "";
        public string TransactionTime { get; set; }
        public long CustomerId { get; set; }
        public long Quantity { get; set; }
    }
}
