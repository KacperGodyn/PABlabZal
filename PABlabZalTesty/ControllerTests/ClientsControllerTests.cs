using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using PABlabZalApi.Controllers;
using PABlabZalApi.Core.Entities;
using PABlabZalApi.Core.Interfaces;
using Xunit;

namespace PABlabZalTesty.ControllerTests
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientService> _mockClientService;
        private readonly ClientsController _controller;

        public ClientsControllerTests()
        {
            _mockClientService = new Mock<IClientService>();
            _controller = new ClientsController(_mockClientService.Object);
        }

        [Fact]
        public async Task GetClients_ReturnsOkResult_WithListOfClients()
        {
            // Arrange
            var clients = new List<Client>
            {
                new Client { Id = 1, Name = "John Doe" },
                new Client { Id = 2, Name = "Jane Smith" }
            };

            _mockClientService.Setup(service => service.GetClientsAsync()).ReturnsAsync(clients);

            // Act
            var result = await _controller.GetClients();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnClients = Assert.IsType<List<Client>>(okResult.Value);
            Assert.Equal(2, returnClients.Count);
        }

        [Fact]
        public async Task GetClient_ReturnsOkResult_WithClient()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "John Doe" };
            _mockClientService.Setup(service => service.GetClientByIdAsync(1)).ReturnsAsync(client);

            // Act
            var result = await _controller.GetClient(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnClient = Assert.IsType<Client>(okResult.Value);
            Assert.Equal(1, returnClient.Id);
        }

        [Fact]
        public async Task GetClient_ReturnsNotFoundResult_WhenClientNotFound()
        {
            // Arrange
            _mockClientService.Setup(service => service.GetClientByIdAsync(1)).ReturnsAsync((Client)null);

            // Act
            var result = await _controller.GetClient(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateClient_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "John Doe" };

            _mockClientService.Setup(service => service.AddClientAsync(client)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateClient(client);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetClient), createdAtActionResult.ActionName);
            Assert.Equal(client.Id, ((Client)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task UpdateClient_ReturnsNoContentResult()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "John Doe" };

            _mockClientService.Setup(service => service.UpdateClientAsync(client)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateClient(1, client);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateClient_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var client = new Client { Id = 1, Name = "John Doe" };

            // Act
            var result = await _controller.UpdateClient(2, client);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteClient_ReturnsNoContentResult()
        {
            // Arrange
            _mockClientService.Setup(service => service.DeleteClientAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteClient(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
