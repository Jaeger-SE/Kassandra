namespace Kassandra.Events
{
    public class MappingEventArgs<TOutput>
    {
        public MappingEventArgs(TOutput item)
        {
            Item = item;
        }

        public TOutput Item { get; }
    }
}