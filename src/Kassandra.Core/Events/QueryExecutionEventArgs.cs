namespace Kassandra.Core.Events
{
    public class QueryExecutionEventArgs
    {
        public QueryExecutionEventArgs(ICommand command)
        {
            Command = command;
        }

        public ICommand Command { get; }
    }
}