namespace Kassandra.Core
{
    public interface ICommandBuilder
    {
        string ConnectionString { get; }

        ITypedCommand<TOutput> BuildCommand<TOutput>(string query, bool isStoredProcedure = true);

        ICommand BuildCommand(string query, bool isStoredProcedure = true);
        //public IteratorCommand BuildIteratorCommand(string query, bool isStoredProcedure = true);

        //public Transaction BuildTransaction();
    }
}