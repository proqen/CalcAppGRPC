using System.Threading.Tasks;
using CalcAppGRPC.Shared;
using Grpc.Core;

namespace CalcAppGRPC.Services.Services
{
    public class CalcService : Calc.CalcBase
    {
        public override async Task<CalcResponse> Addition(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 + request.Value2, StatusCode = (int)Shared.Enums.StatusCode.Ok, StatusMessage = Shared.Enums.StatusCode.Ok.ToString() };

        public override async Task<CalcResponse> Division(CalcRequest request, ServerCallContext context)
        => request.Value2 == 0 ? new CalcResponse { Result = 0, StatusCode = (int)Shared.Enums.StatusCode.Error, StatusMessage = "Делить на ноль нельзя!" }
        : new CalcResponse { Result = request.Value1 / request.Value2, StatusCode = (int)Shared.Enums.StatusCode.Ok, StatusMessage = Shared.Enums.StatusCode.Ok.ToString() };

        public override async Task<CalcResponse> Multiplication(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 * request.Value2, StatusCode = (int)Shared.Enums.StatusCode.Ok, StatusMessage = Shared.Enums.StatusCode.Ok.ToString() };

        public override async Task<CalcResponse> Subtraction(CalcRequest request, ServerCallContext context)
        => new CalcResponse { Result = request.Value1 - request.Value2, StatusCode = (int)Shared.Enums.StatusCode.Ok, StatusMessage = Shared.Enums.StatusCode.Ok.ToString() };
    }
}
