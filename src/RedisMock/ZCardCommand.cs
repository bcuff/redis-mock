using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RedisMock
{
    public class ZCardCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "ZCARD"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            var value = context.Store.Get(key);
            var set = value.Value as SortedDictionary<string, string>;
            var count = set == null ? 0 : set.Count;
            context.Output.WriteLine(count.ToString());
        }
    }
}
