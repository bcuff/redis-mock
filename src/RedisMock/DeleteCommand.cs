using System;
using System.Linq;

namespace RedisMock
{
    public class DeleteCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "DEL"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            context.Store.Remove(key);
            context.WriteOk();
        }
    }
}
