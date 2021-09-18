using System;
using System.Diagnostics;
using System.Text;

namespace BoBo.Formatting.Text;

public class SimpleDump
{
    private readonly Exception exception;

    public SimpleDump(Exception exception)
    {
        this.exception = exception;
    }

    public override string ToString()
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
                traceStringBuilder.AppendLine("---------->");
            }
        }
        return traceStringBuilder.ToString();
    }
}
