using System;
using System.Linq;

namespace RedisMock
{
    public class IncrCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "INCR"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            var value = context.Store.Get(key) ?? new RedisValue { IntegerValue = 0 };
            value.IntegerValue++;
            context.Store.Set(key, value);
        }
    }
}
