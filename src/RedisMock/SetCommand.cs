using System;
using System.Linq;

namespace RedisMock
{
    public class SetCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "SET"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            if(context.Arguments.Count < 2)
            {
                context.WriteInvalidArguments();
                return;
            }
            var textValue = context.Arguments.Skip(1).First();
            context.Store.Set(key, new RedisValue { TextValue = textValue });
            context.WriteOk();
        }
    }
}
