using System;
using System.Linq;

namespace RedisMock
{
    public class GetCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "GET"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            var value = context.Store.Get(key);
            if (value == null || value.TextValue == null)
            {
                context.WriteNil();
            }
            else
            {
                context.Output.WriteLine(value.TextValue);
            }
        }
    }
}
