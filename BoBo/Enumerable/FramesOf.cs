using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;

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
        var frames = new StackTrace(exception, true).GetFrames();
        if (frames == null)
        {
            yield break;
        }

        for (int i = frames.Length - 1; i >= 0; i--)
        {
            yield return frames[i];
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
