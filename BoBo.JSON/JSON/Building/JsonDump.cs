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
        StackFrame[] frames = new StackTrace(exception, true).GetFrames() ?? Array.Empty<StackFrame>();
        foreach (var frame in frames.Reverse())
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
