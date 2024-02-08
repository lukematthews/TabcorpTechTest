using System.Text.Json.Serialization;

namespace TabcorpTechTest.Models.Db
{
    public class User
    {
        [JsonIgnore]
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
