using System.ComponentModel.DataAnnotations;

namespace TabcorpTechTest.Models.Db
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public required DateTime TransactionTime { get; set; }
        public required Customer CustomerId { get; set; }
        public required long Quantity { get; set; }
        public required Product ProductCode { get; set; }
    }
}
