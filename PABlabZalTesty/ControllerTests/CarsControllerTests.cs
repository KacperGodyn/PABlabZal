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
    public class CarsControllerTests
    {
        private readonly Mock<ICarService> _mockCarService;
        private readonly CarsController _controller;

        public CarsControllerTests()
        {
            _mockCarService = new Mock<ICarService>();
            _controller = new CarsController(_mockCarService.Object);
        }

        [Fact]
        public async Task GetCars_ReturnsOkResult_WithListOfCars()
        {
            // Arrange
            var cars = new List<Car>
            {
                new Car { Id = 1, MadeBy = "Toyota", Model = "Camry" },
                new Car { Id = 2, MadeBy = "Honda", Model = "Accord" }
            };

            _mockCarService.Setup(service => service.GetCarsAsync()).ReturnsAsync(cars);

            // Act
            var result = await _controller.GetCars();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnCars = Assert.IsType<List<Car>>(okResult.Value);
            Assert.Equal(2, returnCars.Count);
        }

        [Fact]
        public async Task GetCar_ReturnsOkResult_WithCar()
        {
            // Arrange
            var car = new Car { Id = 1, MadeBy = "Toyota", Model = "Camry" };
            _mockCarService.Setup(service => service.GetCarByIdAsync(1)).ReturnsAsync(car);

            // Act
            var result = await _controller.GetCar(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnCar = Assert.IsType<Car>(okResult.Value);
            Assert.Equal(1, returnCar.Id);
        }

        [Fact]
        public async Task GetCar_ReturnsNotFoundResult_WhenCarNotFound()
        {
            // Arrange
            _mockCarService.Setup(service => service.GetCarByIdAsync(1)).ReturnsAsync((Car)null);

            // Act
            var result = await _controller.GetCar(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateCar_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var car = new Car { Id = 1, MadeBy = "Toyota", Model = "Camry" };

            _mockCarService.Setup(service => service.AddCarAsync(car)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateCar(car);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetCar), createdAtActionResult.ActionName);
            Assert.Equal(car.Id, ((Car)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task UpdateCar_ReturnsNoContentResult()
        {
            // Arrange
            var car = new Car { Id = 1, MadeBy = "Toyota", Model = "Camry" };

            _mockCarService.Setup(service => service.UpdateCarAsync(car)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateCar(1, car);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateCar_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var car = new Car { Id = 1, MadeBy = "Toyota", Model = "Camry" };

            // Act
            var result = await _controller.UpdateCar(2, car);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteCar_ReturnsNoContentResult()
        {
            // Arrange
            _mockCarService.Setup(service => service.DeleteCarAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteCar(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
