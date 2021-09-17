using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BoBo.JSON;

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
public class JsonDump : IFootprint
{
    public JToken MakeFootprint(Exception exception)
    {
        var footprint = new JArray();
        var frames = new StackTrace(exception, true).GetFrames();
        foreach (var frame in frames ?? Array.Empty<StackFrame>())
        {
            if (frame.GetFileLineNumber() > 0)
            {
                footprint.Add(
                    new JObject()
                    {
                        { "File", frame.GetFileName() },
                        { "Method", frame.GetMethod().Name },
                        { "LineNumber", frame.GetFileLineNumber() },
                    });
            }
        }
        return footprint;
    }
}
