using System;
using System.Linq;

namespace RedisMock
{
    public class DbSizeCommand : ICommand
    {
        public string Name
        {
            get { return "DBSIZE"; }
        }

        public void Execute(CommandExecutionContext context)
        {
            context.Output.WriteLine(context.Store.Count);
        }
    }
}
