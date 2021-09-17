﻿using BoBo.Formatting.Text;
using Newtonsoft.Json.Linq;

namespace BoBo.JSON;

/// <summary>
/// Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
/// in a basic line-by-line format
/// </summary>
public class BasicDump : IFootprint
{
    public JToken MakeFootprint(Exception exception)
    {
        return new JValue($"{new SimpleDump(exception)}");
    }
}
