using System;

namespace Kassandra.Core.Events
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