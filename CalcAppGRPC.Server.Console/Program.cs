using CalcAppGRPC.Services.Services;
using CalcAppGRPC.Shared;
using Grpc.Core;
using System;

namespace CalcAppGRPC.ServerConsole
{
    class Program
    {
        const int Port = 50051;
        public static void Main(string[] args)
        {
            try
            {
                var server = new Grpc.Core.Server
                {
                    Services = { Calc.BindService(new CalcService()) },
                    Ports = { new ServerPort("localhost", Port, ServerCredentials.Insecure) }
                };
                server.Start();
                Console.WriteLine("Accounts server listening on port " + Port);
                Console.WriteLine("Press any key to stop the server...");
                Console.ReadKey();
                server.ShutdownAsync().Wait();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception encountered: {ex}");
            }
        }
    }
}
