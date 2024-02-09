using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;
using Moq.EntityFrameworkCore;
using System.Text;
using TabcorpTechTest.Constants;
using TabcorpTechTest.Controllers;
using TabcorpTechTest.Data;
using TabcorpTechTest.Models.Db;
using TabcorpTechTest.Models.Dto;

namespace TabcorpTechTest.Tests.Controllers;

public class SecurityControllerTest
{
    [Fact]
    public void TestUserLookupAndPasswordCheck()
    {
        var mockContext = new Mock<ApiContext>();
        mockContext.Setup(m => m.Users).ReturnsDbSet([new User() { UserName="test", Password="test2", Roles=[SecurityRoles.User]}]);

        var inMemorySettings = new Dictionary<string, string> {
            {"Jwt:Issuer", "http://issuer.jwt"},
            {"Jwt:Audience", "http://audience.jwt"},
            {"Jwt:Key", "MIICWwIBAAKBgQCGNOVFkPrFnzQoJlhzbsn3nauK4sXAEPfIK67qU0RHpOXcFIS9\r\n5RNYbuok4xKFV0BFDIg0NqW16yjJU1gRoMR049I1MK/5Xz90ee5kn77zYLcDLhU/\r\ns39IPDcoOMs2+RDa1IA/X8Grh1J4mu2WoYM3UWdVceXZoXXx2FPrXg9pywIDAQAB\r\nAoGAPVArMsYSm3iphnJGVK5X3SWOeowyFhZqbWvvpKRX/HdMgGhrYKooVW2O0T1g\r\nd8St2x3nmBsjR+JgpuHJyXvuZF+jzekx/ozkvA3OS4djpXB5MTuytrt4w68SO8D1\r\nug6F0OuHhmTOdIFO28fqLfzKU4F7OHIDsFrO8lO+wiQcAzECQQDkX6Skr6zsDwbF\r\nnZcfjY474LqXr3GluHKCfY7rrsZvFfaz0Jfjota1w2TmmV2ypFnC5jg/N2lglSCQ\r\nueWQ+vajAkEAlnEOGxa184Hi4Sh+leipEuS3/QIqlPnX/iB7+Pk4rcFElDZCh8i8\r\nue6FgkJ5HosjzmtHEOg7/ZyRrYKYokj6uQJAZZaKJdwj3wo8J/IXRKjyiX5JYqpf\r\nsqle/t8dkYe4q7eoe4qh1lgcjNRzcQTuIkZry4AfqzdZ/+W2i8q17Q1GYwJATMow\r\nVQtmnIDz+dHdq08y3f35HB/69EgDRCf4n8E2eRppku2PUBfanV1usGqVwE1tXXPM\r\ntoiT9oPwqAw9NLjjoQJATv7UyXmugS1fId0C/6R1t98kxupu66+1R8MVQvozWjZk\r\njLJ1kJErFH7oP6pU222aN1/CvsfEUiuRKx30L3RY5A=="},
        };

        IConfiguration configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(inMemorySettings)
            .Build();

        SecurityController controller = new SecurityController(mockContext.Object, configuration);

        Assert.Equal(Results.Unauthorized(), controller.Post(new UserDto()));
        Assert.Equal(Results.Unauthorized(), controller.Post(new UserDto() { UserName="test", Password="fail"}));
        Assert.Equal(Results.Unauthorized(), controller.Post(new UserDto() { UserName = "fail", Password = "test" }));

        Assert.NotEqual(Results.Unauthorized(), controller.Post(new UserDto() { UserName = "test", Password = "test2" }));
    }
}