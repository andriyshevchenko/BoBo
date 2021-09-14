using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace BoBo.Formatting;

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
public class JsonFootprint : IFootprint
{
    private readonly Exception exception;

    public JsonFootprint(Exception exception)
    {
        this.exception = exception;
    }

    public IFootprint MakeCopy(Exception exception)
    {
        return new JsonFootprint(exception);
    }

    public JToken MakeFootprint()
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
