using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using Kassandra.Abstract;
using Kassandra.Events;

namespace Kassandra.Data.SqlServer
{
    internal class Command : BaseCommand
    {
        protected readonly DbCommand DbCommand;
        protected readonly DbConnection DbConnection;

        public Command(DbConnection connection, string commandName, bool isStoredProcedure)
        {
            Query = commandName;
            DbConnection = connection;
            var command = connection.CreateCommand();
            command.CommandText = commandName;
            if (isStoredProcedure)
            {
                command.CommandType = CommandType.StoredProcedure;
            }
            else
            {
                command.CommandType = CommandType.Text;
            }
            DbCommand = command;
        }

        public override string Query { get; }

        public override ICommand SetTimeOut(int timeout)
        {
            DbCommand.CommandTimeout = timeout;

            return this;
        }

        public override int ExecuteNonQuery()
        {
            try
            {
                OpenConnection();
                OnQueryExecutingHandler(new QueryExecutionEventArgs(this));
                var result = DbCommand.ExecuteNonQuery();
                OnQueryExecutedHandler(new QueryExecutionEventArgs(this));
                OnOperationCompleteHandler(new OperationCompleteEventArgs());
                return result;
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

        public override ICommand AddParameter(string parameterName, object parameterValue, bool condition = true)
        {
            if (!condition) return this;

            var parameter = DbCommand.CreateParameter();
            parameter.ParameterName = parameterName;
            parameter.Value = parameterValue;

            DbCommand.Parameters.Add(parameter);
            return this;
        }

        public override ICommand AddParameter(string parameterName, IEnumerable<int> parameterValue,
            bool condition = true)
        {
            // TODO
            return AddParameter(parameterName, (object) parameterValue, condition);
        }

        public override ICommand AddParameter(string parameterName, IEnumerable<Guid> parameterValue,
            bool condition = true)
        {
            // TODO
            return AddParameter(parameterName, (object) parameterValue, condition);
        }

        public override ICommand AddParameter(string parameterName, IEnumerable<DateTime> parameterValue,
            bool condition = true)
        {
            // TODO
            return AddParameter(parameterName, (object) parameterValue, condition);
        }

        public override ICommand AddParameter(string parameterName, IEnumerable<TimeSpan> parameterValue,
            bool condition = true)
        {
            // TODO
            return AddParameter(parameterName, (object) parameterValue, condition);
        }

        #region Connection handlers

        protected void OpenConnection()
        {
            if (DbConnection.State != ConnectionState.Open)
            {
                DbConnection.Open();
            }
        }

        protected void CloseConnection()
        {
            DbConnection.Close();
        }

        #endregion
    }
}