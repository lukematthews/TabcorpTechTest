namespace TabcorpTechTest.Models.Dto
{
    public class TransactionDto
    {
        public string ProductCode { get; set; } = "";
        public required string TransactionTime { get; set; }
        public long CustomerId { get; set; }
        public long Quantity { get; set; }
    }
}
