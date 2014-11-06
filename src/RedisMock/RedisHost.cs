using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace RedisMock
{
    public class RedisHost
    {
        static readonly Regex _whitespace = new Regex(@"\s+", RegexOptions.Compiled);

        static IEnumerable<ICommand> GetDefaultCommands()
        {
            return Assembly.GetExecutingAssembly()
                .GetTypes()
                .Where(t => typeof(ICommand).IsAssignableFrom(t))
                .Where(t => !t.IsAbstract && t.IsClass)
                .Select(t => (ICommand)Activator.CreateInstance(t));
        }

        readonly IStore _store;
        readonly Dictionary<string, ICommand> _commands;
        readonly object _syncRoot = new object();

        public RedisHost(IStore store, IEnumerable<ICommand> commands = null)
        {
            if(store == null) throw new ArgumentNullException("store");
            _store = store;
            _commands = (commands ?? GetDefaultCommands())
                .ToDictionary(c => c.Name, StringComparer.OrdinalIgnoreCase);
        }

        public async Task ExecuteSession(Stream stream)
        {
            using (var reader = new StreamReader(stream))
            {
                while (true)
                {
                    var line = await reader.ReadLineAsync();
                    if (line == null) break;
                    var arguments = _whitespace.Split(line);
                    var context = new CommandExecutionContext(_store, arguments.Skip(1), stream);
                    if (arguments.Length == 0)
                    {
                        context.WriteInvalidArguments();
                        await context.Output.FlushAsync();
                        continue;
                    }
                    ICommand command;
                    if (!_commands.TryGetValue(arguments[0], out command))
                    {
                        context.Output.WriteLine("Unknown command.");
                        await context.Output.FlushAsync();
                        continue;
                    }
                    try
                    {
                        // this lock scope could be reduced to allow for greater concurrency
                        // however I didn't feel like it was worth the time for this exercise.
                        lock (_syncRoot)
                        {
                            command.Execute(context);
                        }
                    }
                    catch (Exception ex)
                    {
                        context.Output.WriteLine("Error: " + ex);
                    }
                    await context.Output.FlushAsync();
                }
            }
        }
    }
}
