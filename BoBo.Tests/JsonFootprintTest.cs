
using BoBo.Formatting;
using Newtonsoft.Json.Linq;
using Xunit;
using Xunit.Abstractions;

namespace BoBo.Tests;

public class JsonFootprintTest
{
    [Fact]
    public void JsonFootprint_ShouldReturnOutput()
    {
        var footprint = new JsonFootprint(new Exception("not empty"));
        Assert.NotNull(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldNotReturnEmptyString()
    {
        var footprint = new JsonFootprint(new Exception("not empty"));
        Assert.NotEmpty(footprint.MakeFootprint().ToString());
    }
    
    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_NoInnerException()
    {
        var footprint = new JsonFootprint(new Exception("not empty"));
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
        var footprint = new JsonFootprint(
            new Exception("not empty", new ArgumentException("argument wrong")));
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
