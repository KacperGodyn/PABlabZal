using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using PABlabZalApi;
using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Xunit;

namespace PABlabZalApi.Tests.Controller
{
    public class ClientsControllerAuthTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public ClientsControllerAuthTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Theory]
        [InlineData("/api/Clients")]
        [InlineData("/api/Clients/1")]
        public async Task Get_EndpointsRequireAuthorization(string url)
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            // Act
            var response = await client.GetAsync(url);

            // Assert
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task AuthorizedUser_CanAccessProtectedEndpoint()
        {
            // Arrange
            var client = _factory.CreateClient(new WebApplicationFactoryClientOptions
            {
                AllowAutoRedirect = false
            });

            var token = await GetAccessTokenAsync(client);

            // Add the token to the request header
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Act
            var response = await client.GetAsync("/api/Clients");

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var clients = await JsonSerializer.DeserializeAsync<List<Client>>(
                await response.Content.ReadAsStreamAsync(),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            Assert.NotNull(clients);
            Assert.NotEmpty(clients);
        }

        private async Task<string> GetAccessTokenAsync(HttpClient client)
        {
            // Simulate user authentication and obtain JWT token
            var response = await client.PostAsync("/Security/authenticate", new StringContent(
                JsonSerializer.Serialize(new { Username = "wsei", Password = "wsei" }),
                Encoding.UTF8, "application/json"));

            response.EnsureSuccessStatusCode();
            var tokenResponse = await response.Content.ReadAsStringAsync();

            // Extract the token from the response
            var token = JsonSerializer.Deserialize<Dictionary<string, string>>(tokenResponse)["token"];
            return token;
        }
    }
}
