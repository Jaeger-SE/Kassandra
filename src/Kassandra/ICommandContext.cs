namespace Kassandra
{
    public interface ICommandContext
    {
        ICommandBuilder GetBuilder(ConnectionMode mode);
    }
}