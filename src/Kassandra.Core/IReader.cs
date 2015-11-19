namespace Kassandra.Core
{
    public interface IReader
    {
        bool Read();
        TOutput ValueAs<TOutput>(string key, TOutput defValue = default(TOutput));
    }
}