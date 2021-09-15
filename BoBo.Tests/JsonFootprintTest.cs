using BoBo.Formatting;
using Newtonsoft.Json.Linq;
using Xunit;

namespace BoBo.Tests;

public class JsonFootprintTest
{
    [Fact]
    public void JsonFootprint_ShouldReturnOutput()
    {
        var footprint = new JsonDetails(
            new Exception("not empty"),
            new BasicFootprint()
        );
        Assert.NotNull(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldNotReturnEmptyString()
    {
        var footprint = new JsonDetails(
            new Exception("not empty"),
            new BasicFootprint()
        );
        Assert.NotEmpty(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_NoInnerException()
    {
        var footprint = new JsonDetails(
            new Exception("not empty"),
            new BasicFootprint()
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
        var footprint = new JsonDetails(
            new Exception("not empty", new ArgumentException("argument wrong")),
            new BasicFootprint()
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
