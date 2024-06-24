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
    public class PaymentsControllerTests
    {
        private readonly Mock<IPaymentService> _mockPaymentService;
        private readonly PaymentsController _controller;

        public PaymentsControllerTests()
        {
            _mockPaymentService = new Mock<IPaymentService>();
            _controller = new PaymentsController(_mockPaymentService.Object);
        }

        [Fact]
        public async Task GetPayments_ReturnsOkResult_WithListOfPayments()
        {
            // Arrange
            var payments = new List<Payment>
            {
                new Payment { Id = 1, Amount = 100.0m },
                new Payment { Id = 2, Amount = 200.0m }
            };

            _mockPaymentService.Setup(service => service.GetPaymentsAsync()).ReturnsAsync(payments);

            // Act
            var result = await _controller.GetPayments();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPayments = Assert.IsType<List<Payment>>(okResult.Value);
            Assert.Equal(2, returnPayments.Count);
        }

        [Fact]
        public async Task GetPayment_ReturnsOkResult_WithPayment()
        {
            // Arrange
            var payment = new Payment { Id = 1, Amount = 100.0m };
            _mockPaymentService.Setup(service => service.GetPaymentByIdAsync(1)).ReturnsAsync(payment);

            // Act
            var result = await _controller.GetPayment(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnPayment = Assert.IsType<Payment>(okResult.Value);
            Assert.Equal(1, returnPayment.Id);
        }

        [Fact]
        public async Task GetPayment_ReturnsNotFoundResult_WhenPaymentNotFound()
        {
            // Arrange
            _mockPaymentService.Setup(service => service.GetPaymentByIdAsync(1)).ReturnsAsync((Payment)null);

            // Act
            var result = await _controller.GetPayment(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreatePayment_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var payment = new Payment { Id = 1, Amount = 100.0m };

            _mockPaymentService.Setup(service => service.AddPaymentAsync(payment)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreatePayment(payment);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetPayment), createdAtActionResult.ActionName);
            Assert.Equal(payment.Id, ((Payment)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsNoContentResult()
        {
            // Arrange
            var payment = new Payment { Id = 1, Amount = 100.0m };

            _mockPaymentService.Setup(service => service.UpdatePaymentAsync(payment)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdatePayment(1, payment);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdatePayment_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var payment = new Payment { Id = 1, Amount = 100.0m };

            // Act
            var result = await _controller.UpdatePayment(2, payment);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeletePayment_ReturnsNoContentResult()
        {
            // Arrange
            _mockPaymentService.Setup(service => service.DeletePaymentAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeletePayment(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
