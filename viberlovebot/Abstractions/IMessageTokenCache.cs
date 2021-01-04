namespace viberlovebot.Abstractions
{
    public interface IMessageTokenCache
    {
        void Add(long messageToken);
        bool Contains(long messageToken);
    }
}