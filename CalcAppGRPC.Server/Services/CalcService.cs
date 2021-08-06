using System.Threading.Tasks;
using CalcAppGRPC.Shared;
using Grpc.Core;

namespace CalcAppGRPC.Server.Services
{
    public class CalcService : Calc.CalcBase
    {
        public override async Task<CalcResponse> Addition(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 + request.Value2 };

        public override async Task<CalcResponse> Division(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value2 == 0 ? 0 : request.Value1 / request.Value2 };

        public override async Task<CalcResponse> Multiplication(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 * request.Value2 };

        public override async Task<CalcResponse> Subtraction(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 - request.Value2 };
    }
}
