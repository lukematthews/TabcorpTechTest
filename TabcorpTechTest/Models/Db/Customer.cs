using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Db
{
    public class Customer
    {
        [Key]
        public long CustomerID { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Age { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public required Location Location { get; set; }
    }
}
