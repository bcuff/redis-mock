using System;
using System.Linq;

namespace RedisMock
{
    public class ZRankCommand : KeyedCommand
    {
        public override string Name
        {
            get { return "ZRANK"; }
        }

        protected override void Execute(string key, CommandExecutionContext context)
        {
            if(context.Arguments.Count < 2)
            {
                context.WriteInvalidArguments();
                return;
            }
            var member = context.Arguments.Skip(1).First();
            var value = context.Store.Get(key);
            if (value == null || value.SetValue == null)
            {
                context.WriteNil();
                return;
            }
            var set = value.SetValue;
            // todo: improve this lookup by changing the set data structure so it's better than O(n)
            var score = set.Where(pair => pair.Value == member).Select(pair => pair.Key);
            context.Output.WriteLine(score.ToString());
        }
    }
}
