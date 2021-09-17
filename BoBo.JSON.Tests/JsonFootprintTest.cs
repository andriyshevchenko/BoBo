using Newtonsoft.Json.Linq;
using System;
using Xunit;

namespace BoBo.JSON.Tests;

public class JsonFootprintTest
{
    [Fact]
    public void JsonFootprint_ShouldReturnOutput()
    {
        var footprint = new RecursiveDump(new BasicDump());
        Assert.NotNull(footprint.MakeFootprint(new Exception("not empty")).ToString());
    }

    [Fact]
    public void JsonFootprint_ShouldNotReturnEmptyString()
    {
        var footprint = new RecursiveDump(new BasicDump());
        Assert.NotEmpty(footprint.MakeFootprint(new Exception("not empty")).ToString());
    }

    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_NoInnerException()
    {
        var footprint = new RecursiveDump(new BasicDump());
        Assert.True(
            JToken.DeepEquals(
                new JObject
                {
                    { "Message", "not empty" },
                    { "Footprint", "" }
                },
                footprint.MakeFootprint(new Exception("not empty"))
            )
        );
    }

    [Fact]
    public void JsonFootprint_ShouldBeValidJSON_InnerException()
    {
        var footprint = new RecursiveDump(new BasicDump());
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
                footprint.MakeFootprint(new Exception("not empty", new ArgumentException("argument wrong")))
            )
        );
    }
}
