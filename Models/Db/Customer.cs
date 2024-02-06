using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using TabcorpTechTest.Constants;

namespace TabcorpTechTest.Models.Db
{
    public class Customer
    {
        [Key]
        public long CustomerID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Age { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public Location Location { get; set; }
    }
}
