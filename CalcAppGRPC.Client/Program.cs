using System;
using System.Threading.Tasks;
using CalcAppGRPC.Shared;
using CalcAppGRPC.Shared.Enums;
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
                var value = Console.ReadLine();
                var response = await ExecuteAsync(value, calcClient);
                Console.WriteLine(response);
            }
        }
        private static async Task<CalcOperation> FindOperatorAsync(string value)
        {
            if (value.Contains((char)CalcOperation.Addition)) return CalcOperation.Addition;
            if (value.Contains((char)CalcOperation.Subtraction)) return CalcOperation.Subtraction;
            if (value.Contains((char)CalcOperation.Multiplication)) return CalcOperation.Multiplication;
            if (value.Contains((char)CalcOperation.Division)) return CalcOperation.Division;

            return CalcOperation.None;
        }

        private static async Task<string> ExecuteAsync(string value, Calc.CalcClient calcClient)
        {
            var calcOperation = await FindOperatorAsync(value);
            var par = value.Split((char)calcOperation);

            if (double.Parse(par[1]) == 0) return "Делить на ноль нельзя!";

            var request = new CalcRequest { Value1 = double.Parse(par[0]), Value2 = double.Parse(par[1]) };
            return calcOperation switch
            {
                CalcOperation.Addition => (await calcClient.AdditionAsync(request)).Result.ToString(),
                CalcOperation.Subtraction => (await calcClient.SubtractionAsync(request)).Result.ToString(),
                CalcOperation.Multiplication => (await calcClient.MultiplicationAsync(request)).Result.ToString(),
                CalcOperation.Division => (await calcClient.DivisionAsync(request)).Result.ToString(),
                _ => "Error"
            };
        }
    }
}
