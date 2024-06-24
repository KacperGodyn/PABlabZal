using Microsoft.AspNetCore.Mvc.RazorPages;
using PABlabZalApi.Grpc;
using System.Threading.Tasks;

namespace PABlabZalRazor.Pages.Grpc
{
    public class IndexModel : PageModel
    {
        private readonly CarService.CarServiceClient _grpcClient;

        public IndexModel(CarService.CarServiceClient grpcClient)
        {
            _grpcClient = grpcClient;
        }

        public string Message { get; private set; } = "Initial message";

        public async Task OnPostAsync()
        {
            var response = await _grpcClient.GetCarsAsync(new Empty());
            Message = $"Retrieved {response.Cars.Count} cars";
        }
    }
}
