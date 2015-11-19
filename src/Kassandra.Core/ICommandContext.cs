namespace Kassandra.Core
{
    public interface ICommandContext
    {
        ICommandBuilder GetBuilder(ConnectionMode mode);
    }
}