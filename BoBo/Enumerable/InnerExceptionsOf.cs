using System;
using System.Collections;
using System.Collections.Generic;

namespace BoBo.Formatting.Enumerable;

public class InnerExceptionsOf : IEnumerable<Exception>
{
    class Enumerator : IEnumerator<Exception>
    {
        private readonly Exception exception;
        private Exception current;

        public Enumerator(Exception exception)
        {
            this.exception = exception;
            current = exception;
        }

        public Exception Current => current;

        object IEnumerator.Current => current;

        public void Dispose()
        {
            Reset();
        }

        public bool MoveNext()
        {
            bool canMove = current.InnerException != null;
            if (canMove)
            {
                current = current.InnerException;
            }
            return canMove;
        }

        public void Reset()
        {
            current = exception;
        }
    }

    private readonly Exception exception;

    public InnerExceptionsOf(Exception exception)
    {
        this.exception = exception;
    }

    public IEnumerator<Exception> GetEnumerator()
    {
        return new Enumerator(exception);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return new Enumerator(exception);
    }
}
