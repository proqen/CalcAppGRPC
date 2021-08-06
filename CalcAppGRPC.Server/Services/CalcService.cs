using System.Linq;
using System.Threading.Tasks;
using CalcAppGRPC.Shared;
using Grpc.Core;

namespace CalcAppGRPC.Server.Services
{
    public class CalcService : Calc.CalcBase
    {
        private async Task<string> AdditionAsync(double[] value)
        => (value[0] + value[1]).ToString();

        private async Task<string> DivisionAsync(double[] value)
        => value[1] == 0 ? "Делить на ноль нельзя!"
        : (value[0] / value[1]).ToString();

        private async Task<string> MultiplicationAsync(double[] value)
        => (value[0] * value[1]).ToString();

        private async Task<string> SubtractionAsync(double[] value)
        => (value[0] - value[1]).ToString();

        private async Task<char> FindOperatorAsync(string value)
        {
            if (value.Contains('+')) return '+';
            if (value.Contains('-')) return '-';
            if (value.Contains('*')) return '*';
            if (value.Contains('/')) return '/';

            return '_';
        }
        public override async Task<CalcResponse> Execute(CalcRequest request, ServerCallContext context)
        => new CalcResponse
        {
            Result = await FindOperatorAsync(request.Value) switch
            {
                '+' => await AdditionAsync(request.Value.Split('+').Select(double.Parse).ToArray()),
                '-' => await SubtractionAsync(request.Value.Split('-').Select(double.Parse).ToArray()),
                '*' => await MultiplicationAsync(request.Value.Split('*').Select(double.Parse).ToArray()),
                '/' => await DivisionAsync(request.Value.Split('/').Select(double.Parse).ToArray()),
                _ => "Error"
            }
        };
    }
}
