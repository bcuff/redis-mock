using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisMock
{
    public class RedisStore : IStore
    {
        readonly Dictionary<string, RedisValue> _store = new Dictionary<string,RedisValue>();

        public RedisValue Get(string key)
        {
            RedisValue result;
            if (_store.TryGetValue(key, out result)) return result;
            return null;
        }

        public void Set(string key, RedisValue value)
        {
            _store[key] = value;
        }

        public bool Remove(string key)
        {
            return _store.Remove(key);
        }

        public int Count
        {
            get { return _store.Count; }
        }
    }
}
