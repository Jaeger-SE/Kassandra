using System;
using System.Data.Common;
using System.Data.SqlClient;
using Kassandra.Core;

namespace Kassandra.Data.SqlServer
{
    public class CommandBuilder : ICommandBuilder
    {
        public CommandBuilder(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public string ConnectionString { get; }

        public ITypedCommand<TOutput> BuildCommand<TOutput>(string query, bool isStoredProcedure = true)
        {
            return new TypedCommand<TOutput>(BuildNewConnection(), query, isStoredProcedure);
        }

        public ICommand BuildCommand(string query, bool isStoredProcedure = true)
        {
            return new Command(BuildNewConnection(), query, isStoredProcedure);
        }

        private DbConnection BuildNewConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}