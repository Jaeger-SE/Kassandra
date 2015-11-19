using System;
using System.Collections.Generic;
using System.Data.Common;
using Kassandra.Events;

namespace Kassandra.Data.SqlServer
{
    internal class TypedCommand<TOutput> : Command, ITypedCommand<TOutput>
    {
        private Action<IReader, TOutput> _mapper;
        private Func<IReader, TOutput> _objectCreationLogic;

        public TypedCommand(DbConnection connection, string commandName, bool isStoredProcedure)
            : base(connection, commandName, isStoredProcedure)
        {
        }

        public ITypedCommand<TOutput> SetMapper(Action<IReader, TOutput> mapper)
        {
            _mapper = mapper;
            return this;
        }

        public ITypedCommand<TOutput> ObjectCreation(Func<IReader, TOutput> objectCreationLogic)
        {
            _objectCreationLogic = objectCreationLogic;
            return this;
        }

        public IList<TOutput> QueryMany()
        {
            try
            {
                OpenConnection();

                OnQueryExecutingHandler(new QueryExecutionEventArgs(this));
                IReader reader = new Reader(DbCommand.ExecuteReader());
                OnQueryExecutedHandler(new QueryExecutionEventArgs(this));
                var output = new List<TOutput>();
                while (reader.Read())
                {
                    var elt = _objectCreationLogic == null ? CreateObject() : _objectCreationLogic.Invoke(reader);
                    OnMappingExecutingHandler(new MappingEventArgs<TOutput>(elt));
                    _mapper?.Invoke(reader, elt);
                    OnMappingExecutedHandler(new MappingEventArgs<TOutput>(elt));
                    output.Add(elt);
                }
                OnOperationCompleteHandler(new OperationCompleteEventArgs());
                return output;
            }
            catch (Exception e)
            {
                OnErrorHandler(new QueryErrorEventArgs(e));
                throw;
            }
            finally
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    OnErrorHandler(new QueryErrorEventArgs(e));
                    throw;
                }
            }
        }

        public TOutput QuerySingle()
        {
            try
            {
                OpenConnection();

                OnQueryExecutingHandler(new QueryExecutionEventArgs(this));
                IReader reader = new Reader(DbCommand.ExecuteReader());
                OnQueryExecutedHandler(new QueryExecutionEventArgs(this));
                if (!reader.Read())
                {
                    return default(TOutput);
                }

                var elt = _objectCreationLogic == null ? CreateObject() : _objectCreationLogic.Invoke(reader);
                OnMappingExecutingHandler(new MappingEventArgs<TOutput>(elt));
                _mapper?.Invoke(reader, elt);
                OnMappingExecutedHandler(new MappingEventArgs<TOutput>(elt));
                OnOperationCompleteHandler(new OperationCompleteEventArgs());
                return elt;
            }
            catch (Exception e)
            {
                OnErrorHandler(new QueryErrorEventArgs(e));
                throw;
            }
            finally
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    OnErrorHandler(new QueryErrorEventArgs(e));
                    throw;
                }
            }
        }

        public TOutput QueryScalar()
        {
            try
            {
                OpenConnection();

                OnQueryExecutingHandler(new QueryExecutionEventArgs(this));
                var output = (TOutput) DbCommand.ExecuteScalar();
                OnQueryExecutedHandler(new QueryExecutionEventArgs(this));
                OnOperationCompleteHandler(new OperationCompleteEventArgs());
                return output;
            }
            catch (Exception e)
            {
                OnErrorHandler(new QueryErrorEventArgs(e));
                throw;
            }
            finally
            {
                try
                {
                    CloseConnection();
                }
                catch (Exception e)
                {
                    OnErrorHandler(new QueryErrorEventArgs(e));
                    throw;
                }
            }
        }

        public new ITypedCommand<TOutput> SetTimeOut(int timeOut)
        {
            base.SetTimeOut(timeOut);
            return this;
        }

        public new ITypedCommand<TOutput> AddParameter(string parameterName, object parameterValue,
            bool condition = true)
        {
            base.AddParameter(parameterName, parameterValue, condition);
            return this;
        }

        public new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<int> parameterValue,
            bool condition = true)
        {
            base.AddParameter(parameterName, parameterValue, condition);
            return this;
        }

        public new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<Guid> parameterValue,
            bool condition = true)
        {
            base.AddParameter(parameterName, parameterValue, condition);
            return this;
        }

        public new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<DateTime> parameterValue,
            bool condition = true)
        {
            base.AddParameter(parameterName, parameterValue, condition);
            return this;
        }

        public new ITypedCommand<TOutput> AddParameter(string parameterName, IEnumerable<TimeSpan> parameterValue,
            bool condition = true)
        {
            base.AddParameter(parameterName, parameterValue, condition);
            return this;
        }

        public new ITypedCommand<TOutput> OnError(Action<QueryErrorEventArgs> args)
        {
            base.OnError(args);
            return this;
        }

        public new ITypedCommand<TOutput> OnQueryExecuting(Action<QueryExecutionEventArgs> args)
        {
            base.OnQueryExecuting(args);
            return this;
        }

        public new ITypedCommand<TOutput> OnQueryExecuted(Action<QueryExecutionEventArgs> args)
        {
            base.OnQueryExecuted(args);
            return this;
        }

        public new ITypedCommand<TOutput> OnOperationComplete(Action<OperationCompleteEventArgs> args)
        {
            base.OnOperationComplete(args);
            return this;
        }

        public ITypedCommand<TOutput> OnMappingExecuting(Action<MappingEventArgs<TOutput>> args)
        {
            OnMappingExecutingEvent += args;

            return this;
        }

        public ITypedCommand<TOutput> OnMappingExecuted(Action<MappingEventArgs<TOutput>> args)
        {
            OnMappingExecutedEvent += args;

            return this;
        }

        private event Action<MappingEventArgs<TOutput>> OnMappingExecutedEvent;

        protected void OnMappingExecutedHandler(MappingEventArgs<TOutput> args)
        {
            var handler = OnMappingExecutedEvent;
            handler?.Invoke(args);
        }

        private event Action<MappingEventArgs<TOutput>> OnMappingExecutingEvent;

        protected void OnMappingExecutingHandler(MappingEventArgs<TOutput> args)
        {
            var handler = OnMappingExecutingEvent;
            handler?.Invoke(args);
        }

        private static TOutput CreateObject()
        {
            return (TOutput) Activator.CreateInstance(typeof (TOutput));
        }
    }
}