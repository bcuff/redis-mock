using System;
using System.Linq;

namespace RedisMock
{
    public interface IStore
    {
        RedisValue Get(string key);
        void Set(string key, RedisValue value);
        bool Remove(string key);
        int Count { get; }
    }
}
