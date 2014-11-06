using System;
using System.IO;
using System.Linq;

namespace RedisMock
{
    public abstract class KeyedCommand : ICommand
    {
        public abstract string Name { get;  }

        public void Execute(CommandExecutionContext context)
        {
            var key = context.Arguments.FirstOrDefault();
            if (key == null)
            {
                context.WriteInvalidArguments();
                return;
            }
            Execute(key, context);
        }

        protected abstract void Execute(string key, CommandExecutionContext context);
    }
}
