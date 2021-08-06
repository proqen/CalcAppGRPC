using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

namespace CalcAppGRPC.Server.Services
{
    public class CalcService : Calc.CalcBase
    {
        private async Task<CalcResponse> AdditionAsync(double[] value)
        => new CalcResponse { Result = (value[0] + value[1]).ToString() };

        private async Task<CalcResponse> DivisionAsync(double[] value)
        => value[1] == 0 ? new CalcResponse { Result = "Делить на ноль нельзя!" } 
        : new CalcResponse { Result = (value[0] / value[1]).ToString() };

        private async Task<CalcResponse> MultiplicationAsync(double[] value)
        => new CalcResponse { Result = (value[0] * value[1]).ToString() };

        private async Task<CalcResponse> SubtractionAsync(double[] value)
        => new CalcResponse { Result = (value[0] - value[1]).ToString() };

        private async Task<char> FindOperatorAsync(string value)
        {
            if (value.Contains('+')) return '+';
            if (value.Contains('-')) return '-';
            if (value.Contains('*')) return '*';
            if (value.Contains('/')) return '/';

            return '_';
        }
        public override async Task<CalcResponse> Execute(CalcRequest request, ServerCallContext context)
        => await FindOperatorAsync(request.Value) switch
            {
                '+' => await AdditionAsync(request.Value.Split('+').Select(double.Parse).ToArray()),
                '-' => await SubtractionAsync(request.Value.Split('-').Select(double.Parse).ToArray()),
                '*' => await MultiplicationAsync(request.Value.Split('*').Select(double.Parse).ToArray()),
                '/' => await DivisionAsync(request.Value.Split('/').Select(double.Parse).ToArray()),
                _ => new CalcResponse { Result = "Error" }
            };
    }
}
