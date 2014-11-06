using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedisMock
{
    public class ZRangeCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "ZRANGE"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            int start, stop;
            if (context.Arguments.Count < 3)
            {
                context.WriteInvalidArguments();
                return;
            }
            if (!int.TryParse(context.Arguments.Skip(1).First(), out start)
                || !int.TryParse(context.Arguments.Skip(2).First(), out stop))
            {
                context.WriteInvalidArguments();
                return;
            }
            var value = context.Store.Get(key);
            if (value == null || value.SetValue == null)
            {
                context.WriteNil();
                return;
            }
            var set = value.SetValue;
            if (start < 0)
            {
                start = set.Count + start;
                if (start < 0) start = 0;
            }
            if (stop < 0)
            {
                stop = set.Count + stop;
                if (stop < 0) stop = 0;
            }
            var results = stop < start ? set.Reverse() : set;
            if (stop < start)
            {
                var temp = start;
                start = stop;
                stop = temp;
            }
            var count = stop - start + 1;

            foreach (var result in results.Skip(start).Take(count))
            {
                context.Output.WriteLine(result);
            }
        }
    }
}
