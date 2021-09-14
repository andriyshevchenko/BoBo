using Newtonsoft.Json.Linq;
using System.Diagnostics;
using System.Text;

namespace BoBo.Formatting;

/// <summary>
/// Gets the entire stack trace consisting of exception's footprints (File, Method, LineNumber)
/// in a basic line-by-line format
/// </summary>
public class BasicFootprint : IFootprint
{
    private readonly Exception exception;

    public BasicFootprint(Exception exception)
    {
        this.exception = exception;
    }

    public IFootprint CopyItself(Exception exception)
    {
        return new BasicFootprint(exception);
    }

    public JToken MakeFootprint()
    {
        var frames = new StackTrace(exception, true).GetFrames();
        var traceStringBuilder = new StringBuilder();
        foreach (var frame in frames ?? Array.Empty<StackFrame>())
        {
            if (frame.GetFileLineNumber() > 0)
            {
                traceStringBuilder.AppendLine($"File: {frame.GetFileName()}");
                traceStringBuilder.AppendLine($"Method: {frame.GetMethod().Name}");
                traceStringBuilder.AppendLine($"LineNumber: {frame.GetFileLineNumber()}");
                traceStringBuilder.AppendLine(" ---> ");
            }
        }
        return new JValue(traceStringBuilder.ToString());
    }
}
