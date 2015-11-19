using System;
using System.Collections.Generic;
using Kassandra.Events;

namespace Kassandra
{
    public interface ICommand
    {
        string Query { get; }
        ICommand SetTimeOut(int timeout);
        int ExecuteNonQuery();

        #region Parameters

        ICommand AddParameter(string parameterName, object parameterValue, bool condition = true);
        ICommand AddParameter(string parameterName, IEnumerable<int> parameterValue, bool condition = true);
        ICommand AddParameter(string parameterName, IEnumerable<Guid> parameterValue, bool condition = true);
        ICommand AddParameter(string parameterName, IEnumerable<DateTime> parameterValue, bool condition = true);
        ICommand AddParameter(string parameterName, IEnumerable<TimeSpan> parameterValue, bool condition = true);

        #endregion

        #region Events

        ICommand OnError(Action<QueryErrorEventArgs> action);
        ICommand OnQueryExecuting(Action<QueryExecutionEventArgs> action);
        ICommand OnQueryExecuted(Action<QueryExecutionEventArgs> action);
        ICommand OnOperationComplete(Action<OperationCompleteEventArgs> action);

        #endregion
    }
}