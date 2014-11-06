using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace RedisMock
{
    public class CommandExecutionContext
    {

        public CommandExecutionContext(IStore store, IEnumerable<string> arguments, Stream output)
        {
            Store = store;
            Arguments = arguments.ToList();
            Output = new StreamWriter(output);
        }

        public IStore Store { get; private set; }

        public IReadOnlyCollection<string> Arguments { get; private set; }

        public TextWriter Output { get; set; }

        public void WriteInvalidArguments()
        {
            Output.WriteLine("Invalid arguments.");
        }

        public void WriteNil()
        {
            Output.WriteLine("(nil)");
        }

        public void WriteOk()
        {
            Output.WriteLine("OK");
        }
    }
}
