using System;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace RedisMock.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var store = new RedisStore();
            var host = new RedisHost(store);
            Run(host).ContinueWith(t => Environment.Exit(t.IsFaulted ? 1 : 0));
            System.Console.WriteLine("Press any key to quit...");
            System.Console.ReadKey();
        }

        static async Task Run(RedisHost host)
        {
            var listener = new TcpListener(IPAddress.Loopback, 12347);
            listener.Start();

            while (true)
            {
                var client = await listener.AcceptTcpClientAsync();
                HandleNewConnection(client, host);
            }
        
        }

        static async void HandleNewConnection(TcpClient client, RedisHost host)
        {
            using (client)
            using (var stream = client.GetStream())
            {
                await host.ExecuteSession(stream);
            }
        }
    }
}
