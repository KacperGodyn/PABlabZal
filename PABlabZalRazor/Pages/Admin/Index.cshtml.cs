using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PABlabZalApi.Core.Entities;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace AdminPanel.Pages.Admin
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public IndexModel(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public List<Car> Cars { get; set; }
        public List<Client> Clients { get; set; }
        public List<Employee> Employees { get; set; }
        public List<Payment> Payments { get; set; }
        public List<Rental> Rentals { get; set; }

        public async Task OnGetAsync()
        {
            // cookies
            var accessToken = Request.Cookies["access_token"];

            if (string.IsNullOrEmpty(accessToken))
            {
                return;
            }

            var client = _httpClientFactory.CreateClient("api");
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

            Cars = await client.GetFromJsonAsync<List<Car>>("api/Cars");
            Clients = await client.GetFromJsonAsync<List<Client>>("api/Clients");
            Employees = await client.GetFromJsonAsync<List<Employee>>("api/Employees");
            Payments = await client.GetFromJsonAsync<List<Payment>>("api/Payments");
            Rentals = await client.GetFromJsonAsync<List<Rental>>("api/Rentals");
        }
    }
}
