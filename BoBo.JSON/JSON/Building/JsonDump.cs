using BoBo.Enumerable;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BoBo.Formatting.JSON;

/// <summary>
/// Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
/// in a JSON format.
/// For example,
/// [
///     {
///         "File": "Program.cs",
///         "Method": "Main",
///         "LineNumber": 31
///     }
/// ]
/// </summary>
public class JsonDump : IDump
{
    public JToken MakeDump(Exception exception)
    {
        var footprint = new JArray();
        foreach (var frame in new FramesOf(exception))
        {
            if (frame.GetFileLineNumber() > 0)
            {
                var methodName = frame.GetMethod()?.Name ?? string.Empty;
                footprint.Add(
                    new JObject()
                    {
                        { "File", frame.GetFileName() },
                        { "Method", methodName },
                        { "LineNumber", frame.GetFileLineNumber() },
                    });
            }
        }
        return footprint;
    }
}
