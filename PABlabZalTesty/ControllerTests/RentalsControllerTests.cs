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
    public class RentalsControllerTests
    {
        private readonly Mock<IRentalService> _mockRentalService;
        private readonly RentalsController _controller;

        public RentalsControllerTests()
        {
            _mockRentalService = new Mock<IRentalService>();
            _controller = new RentalsController(_mockRentalService.Object);
        }

        [Fact]
        public async Task GetRentals_ReturnsOkResult_WithListOfRentals()
        {
            // Arrange
            var rentals = new List<Rental>
            {
                new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) },
                new Rental { Id = 2, StartDate = DateTime.Now.AddDays(-8), EndDate = DateTime.Now.AddDays(-3) }
            };

            _mockRentalService.Setup(service => service.GetRentalsAsync()).ReturnsAsync(rentals);

            // Act
            var result = await _controller.GetRentals();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnRentals = Assert.IsType<List<Rental>>(okResult.Value);
            Assert.Equal(2, returnRentals.Count);
        }

        [Fact]
        public async Task GetRental_ReturnsOkResult_WithRental()
        {
            // Arrange
            var rental = new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) };
            _mockRentalService.Setup(service => service.GetRentalByIdAsync(1)).ReturnsAsync(rental);

            // Act
            var result = await _controller.GetRental(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnRental = Assert.IsType<Rental>(okResult.Value);
            Assert.Equal(1, returnRental.Id);
        }

        [Fact]
        public async Task GetRental_ReturnsNotFoundResult_WhenRentalNotFound()
        {
            // Arrange
            _mockRentalService.Setup(service => service.GetRentalByIdAsync(1)).ReturnsAsync((Rental)null);

            // Act
            var result = await _controller.GetRental(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateRental_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var rental = new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) };

            _mockRentalService.Setup(service => service.AddRentalAsync(rental)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateRental(rental);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetRental), createdAtActionResult.ActionName);
            Assert.Equal(rental.Id, ((Rental)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task UpdateRental_ReturnsNoContentResult()
        {
            // Arrange
            var rental = new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) };

            _mockRentalService.Setup(service => service.UpdateRentalAsync(rental)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateRental(1, rental);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateRental_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var rental = new Rental { Id = 1, StartDate = DateTime.Now.AddDays(-10), EndDate = DateTime.Now.AddDays(-5) };

            // Act
            var result = await _controller.UpdateRental(2, rental);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteRental_ReturnsNoContentResult()
        {
            // Arrange
            _mockRentalService.Setup(service => service.DeleteRentalAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteRental(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
