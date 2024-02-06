using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Eventing.Reader;

namespace TabcorpTechTest.Models.Db
{
    public class Transaction
    {
        [Key]
        public long Id { get; set; }

        public DateTime TransactionTime { get; set; }
        public Customer CustomerId { get; set; }
        public long Quantity { get; set; }
        public Product ProductCode { get; set; }
    }
}
