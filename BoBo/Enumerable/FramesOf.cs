using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace BoBo.Enumerable;

public class FramesOf : IEnumerable<StackFrame>
{
    private readonly Exception exception;

    public FramesOf(Exception exception)
    {
        this.exception = exception;
    }

    public IEnumerator<StackFrame> GetEnumerator()
    {
        StackFrame[] frames =
            new StackTrace(exception, true).GetFrames() ?? Array.Empty<StackFrame>();
        return frames
            .AsEnumerable()
            .Reverse()
            .GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
