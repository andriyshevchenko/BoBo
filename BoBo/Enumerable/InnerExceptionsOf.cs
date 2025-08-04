using System;
using System.Collections;
using System.Collections.Generic;

namespace BoBo.Enumerable;

public class InnerExceptionsOf : IEnumerable<Exception>
{
    private readonly Exception exception;

    public InnerExceptionsOf(Exception exception)
    {
        this.exception = exception;
    }

    public IEnumerator<Exception> GetEnumerator()
    {
        for (var current = exception.InnerException; current != null; current = current.InnerException)
        {
            yield return current;
        }
    }

    IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}
