using System.ComponentModel.DataAnnotations;

namespace TabcorpTechTest.Models.Db
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public required DateTime TransactionTime { get; set; }
        public required Customer Customer { get; set; }
        public required long Quantity { get; set; }
        public required Product Product { get; set; }

        public decimal GetCost()
        {
            return Product == null ? throw new InvalidOperationException() : Quantity * Product.Cost;
        }
    }
}
