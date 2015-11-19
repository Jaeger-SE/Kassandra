using System;
using System.Collections.Generic;
using Kassandra.Events;

namespace Kassandra
{
    public interface ITypedCommand<TOutput> : ICommand
    {
        ITypedCommand<TOutput> SetMapper(Action<IReader, TOutput> mapper);
        ITypedCommand<TOutput> ObjectCreation(Func<IReader, TOutput> logic);

        IList<TOutput> QueryMany();
        TOutput QuerySingle();
        TOutput QueryScalar();

        #region Overrides parent return type

        new ITypedCommand<TOutput> SetTimeOut(int timeOut);
        new ITypedCommand<TOutput> AddParameter(string parameterName, object parameterValue, bool condition = true);

        new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<int> parameterValue,
            bool condition = true);

        new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<Guid> parameterValue,
            bool condition = true);

        new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<DateTime> parameterValue,
            bool condition = true);

        new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<TimeSpan> parameterValue,
            bool condition = true);

        #endregion

        #region Events

        ITypedCommand<TOutput> OnMappingExecuting(Action<MappingEventArgs<TOutput>> args);
        ITypedCommand<TOutput> OnMappingExecuted(Action<MappingEventArgs<TOutput>> args);

        #region Overrides parent return type

        new ITypedCommand<TOutput> OnError(Action<QueryErrorEventArgs> args);

        new ITypedCommand<TOutput> OnQueryExecuting(Action<QueryExecutionEventArgs> args);

        new ITypedCommand<TOutput> OnQueryExecuted(Action<QueryExecutionEventArgs> args);

        new ITypedCommand<TOutput> OnOperationComplete(Action<OperationCompleteEventArgs> args);

        #endregion

        #endregion
    }
}