using System;
using System.Linq;

namespace RedisMock
{
    public class ZAddCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "ZADD"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            if (context.Arguments.Count < 3)
            {
                context.WriteInvalidArguments();
                return;
            }
            var score = context.Arguments.Skip(1).First();
            var member = context.Arguments.Skip(2).First();
            var value = context.Store.Get(key) ?? new RedisValue();
            var set = value.ToSet();
            set[score] = member;
            context.Store.Set(key, value);
            context.WriteOk();
        }
    }
}
