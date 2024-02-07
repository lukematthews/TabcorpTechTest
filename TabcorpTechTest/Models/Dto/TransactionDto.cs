namespace TabcorpTechTest.Models.Dto
{
    public class TransactionDto
    {
        public string ProductCode { get; set; } = "";
        public required string TransactionTime { get; set; }
        public long CustomerID { get; set; }
        public long Quantity { get; set; }
    }
}
