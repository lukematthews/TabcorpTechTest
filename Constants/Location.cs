using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace TabcorpTechTest.Constants
{
    public enum Location
    {
        [EnumMember(Value = "Australia")]
        AUSTRALIA,
        [EnumMember(Value = "US")]
        US,
        [EnumMember(Value = "Canada")]
        CANADA
    }
}
