namespace Kassandra.Data.SqlServer
{
    public class CommandContext
    {
        private readonly ConnectionSet _connectionSet;

        public CommandContext(ConnectionSet connectionSet)
        {
            _connectionSet = connectionSet;
        }

        public CommandBuilder GetBuilder(ConnectionMode mode)
        {
            switch (mode)
            {
                case ConnectionMode.Write:
                    return new CommandBuilder(_connectionSet.Write);
                default:
                    return new CommandBuilder(_connectionSet.ReadOnly);
            }
        }
    }
}