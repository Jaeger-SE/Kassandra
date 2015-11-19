using System;

namespace Kassandra.Events
{
    public class QueryErrorEventArgs
    {
        public QueryErrorEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public Exception Exception { get; }
    }
}