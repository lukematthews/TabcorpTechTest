using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Db
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public string ProductCode { get; set; }
        public decimal? Cost { get; set; }
        public ProductStatus Status { get; set; }
    }
}
