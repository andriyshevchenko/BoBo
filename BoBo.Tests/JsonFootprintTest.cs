using BoBo.Formatting;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BoBo.Tests;

public class JsonFootprintTest
{
    [Fact]
    public void JsonFootprint_ShouldReturnOutput()
    {
        var exception = new Exception("not empty");
        var footprint = new JsonDetails(
            exception,
            new BasicFootprint(exception)
        );
        Assert.NotNull(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldNotReturnEmptyString()
    {
        var exception = new Exception("not empty");
        var footprint = new JsonDetails(
            exception,
            new BasicFootprint(exception)
        );
        Assert.NotEmpty(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_NoInnerException()
    {
        var exception = new Exception("not empty");
        var footprint = new JsonDetails(
            exception,
            new BasicFootprint(exception)
        );
        Assert.True(
            JToken.DeepEquals(
                new JObject
                {
                    { "Message", "not empty" },
                    { "Footprint", "" }
                },
                footprint.MakeFootprint()
            )
        );
    }

    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_InnerException()
    {
        var exception = new Exception("not empty", new ArgumentException("argument wrong"));
        var footprint = new JsonDetails(
            exception,
            new BasicFootprint(exception)
        );
        Assert.True(
            JToken.DeepEquals(
                new JObject
                {
                    { "Message", "not empty" },
                    { "Footprint", "" },
                    { "InnerException",
                        new JObject() {
                            { "Message", "argument wrong" },
                            { "Footprint", "" },
                        }
                    }
                },
                footprint.MakeFootprint()
            )
        );
    }
}
