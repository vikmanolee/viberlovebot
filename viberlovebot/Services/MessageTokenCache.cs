using System.Collections.Generic;
using viberlovebot.Abstractions;

namespace viberlovebot.Services
{
    public class MessageTokenCache : IMessageTokenCache
    {
        private Queue<long> _handledMessageTokens;
        private int _capacity;

        public MessageTokenCache(int capacity)
        {
            _capacity = capacity >= 0 ? capacity : 0;
            _handledMessageTokens = new Queue<long>(_capacity);
        }

        public void Add(long messageToken)
        {
            if (_capacity == 0)
                return;

            if (_handledMessageTokens.Count < _capacity)
            {
                _handledMessageTokens.Enqueue(messageToken);
            }
            else
            {
                _handledMessageTokens.TryDequeue(out var _);
                _handledMessageTokens.Enqueue(messageToken);
            }
        }

        public bool Contains(long messageToken)
        {
            return _handledMessageTokens.Contains(messageToken);
        }

        public void Flush()
        {
            _handledMessageTokens.Clear();
        }
    }
}