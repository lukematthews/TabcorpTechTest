using System.ComponentModel.DataAnnotations;
using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Db
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        public required string ProductCode { get; set; }
        public required decimal Cost { get; set; }
        public required ProductStatus Status { get; set; }
    }
}
