using System;
using System.Threading.Tasks;
using CalcAppGRPC.Shared;
using Grpc.Net.Client;

namespace CalcAppGRPC.Client
{
    class Program
    {
        static async Task Main(string[] args)
        {
            using var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var calcClient = new Calc.CalcClient(channel);
            while (true)
            {
                Console.Write("Калькулятор\nA+B, A-B, A*B, A/B: ");
                var response = await calcClient.ExecuteAsync(new CalcRequest { Value = Console.ReadLine() });
                Console.WriteLine(response.Result);
            }
        }
    }
}
