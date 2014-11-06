using System;
using System.Linq;

namespace RedisMock
{
    public interface ICommand
    {
        string Name { get; }
        void Execute(CommandExecutionContext context);
    }
}
