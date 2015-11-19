using System;
using System.Collections.Generic;
using Kassandra.Events;

namespace Kassandra.Abstract
{
    public abstract class BaseCommand : ICommand
    {
        public abstract string Query { get; }

        public abstract ICommand SetTimeOut(int timeout);

        public abstract int ExecuteNonQuery();

        public abstract ICommand AddParameter(string parameterName, object parameterValue, bool condition = true);

        public abstract ICommand AddParameter(string parameterName, IEnumerable<int> parameterValue,
            bool condition = true);

        public abstract ICommand AddParameter(string parameterName, IEnumerable<Guid> parameterValue,
            bool condition = true);

        public abstract ICommand AddParameter(string parameterName, IEnumerable<DateTime> parameterValue,
            bool condition = true);

        public abstract ICommand AddParameter(string parameterName, IEnumerable<TimeSpan> parameterValue,
            bool condition = true);

        public ICommand OnError(Action<QueryErrorEventArgs> action)
        {
            OnErrorEvent += action;
            return this;
        }

        private event Action<QueryErrorEventArgs> OnErrorEvent;

        protected void OnErrorHandler(QueryErrorEventArgs args)
        {
            var handler = OnErrorEvent;
            handler?.Invoke(args);
        }

        private event Action<QueryExecutionEventArgs> OnQueryExecutingEvent;

        protected void OnQueryExecutingHandler(QueryExecutionEventArgs args)
        {
            Action<QueryExecutionEventArgs> handler = OnQueryExecutingEvent;
            handler?.Invoke(args);
        }

        public ICommand OnQueryExecuting(Action<QueryExecutionEventArgs> action)
        {
            OnQueryExecutingEvent += action;
            return this;
        }

        private event Action<QueryExecutionEventArgs> OnQueryExecutedEvent;

        protected void OnQueryExecutedHandler(QueryExecutionEventArgs args)
        {
            Action<QueryExecutionEventArgs> handler = OnQueryExecutedEvent;
            handler?.Invoke(args);
        }

        public ICommand OnQueryExecuted(Action<QueryExecutionEventArgs> action)
        {
            OnQueryExecutedEvent += action;

            return this;
        }

        private event Action<OperationCompleteEventArgs> OnOperationCompleteEvent;

        protected void OnOperationCompleteHandler(OperationCompleteEventArgs args)
        {
            Action<OperationCompleteEventArgs> handler = OnOperationCompleteEvent;
            handler?.Invoke(args);
        }

        public ICommand OnOperationComplete(Action<OperationCompleteEventArgs> action)
        {
            OnOperationCompleteEvent += action;
            return this;
        }
    }
}