using System.ComponentModel.DataAnnotations;

namespace TabcorpTechTest.Models.Dto
{
    public class TransactionDto
    {
        public string ProductCode { get; set; } = "";
        public required string TransactionTime { get; set; }
        public long CustomerID { get; set; }
        [Range(1, double.MaxValue)]
        public long Quantity { get; set; }
    }
}
