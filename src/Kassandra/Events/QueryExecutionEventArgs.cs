namespace Kassandra.Events
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