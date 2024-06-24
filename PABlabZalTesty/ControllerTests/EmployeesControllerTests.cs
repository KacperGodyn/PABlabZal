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
    public class EmployeesControllerTests
    {
        private readonly Mock<IEmployeeService> _mockEmployeeService;
        private readonly EmployeesController _controller;

        public EmployeesControllerTests()
        {
            _mockEmployeeService = new Mock<IEmployeeService>();
            _controller = new EmployeesController(_mockEmployeeService.Object);
        }

        [Fact]
        public async Task GetEmployees_ReturnsOkResult_WithListOfEmployees()
        {
            // Arrange
            var employees = new List<Employee>
            {
                new Employee { Id = 1, Name = "John Doe" },
                new Employee { Id = 2, Name = "Jane Smith" }
            };

            _mockEmployeeService.Setup(service => service.GetEmployeesAsync()).ReturnsAsync(employees);

            // Act
            var result = await _controller.GetEmployees();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnEmployees = Assert.IsType<List<Employee>>(okResult.Value);
            Assert.Equal(2, returnEmployees.Count);
        }

        [Fact]
        public async Task GetEmployee_ReturnsOkResult_WithEmployee()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe" };
            _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync(employee);

            // Act
            var result = await _controller.GetEmployee(1);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result.Result);
            var returnEmployee = Assert.IsType<Employee>(okResult.Value);
            Assert.Equal(1, returnEmployee.Id);
        }

        [Fact]
        public async Task GetEmployee_ReturnsNotFoundResult_WhenEmployeeNotFound()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.GetEmployeeByIdAsync(1)).ReturnsAsync((Employee)null);

            // Act
            var result = await _controller.GetEmployee(1);

            // Assert
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task CreateEmployee_ReturnsCreatedAtActionResult()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe" };

            _mockEmployeeService.Setup(service => service.AddEmployeeAsync(employee)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.CreateEmployee(employee);

            // Assert
            var createdAtActionResult = Assert.IsType<CreatedAtActionResult>(result);
            Assert.Equal(nameof(_controller.GetEmployee), createdAtActionResult.ActionName);
            Assert.Equal(employee.Id, ((Employee)createdAtActionResult.Value).Id);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsNoContentResult()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe" };

            _mockEmployeeService.Setup(service => service.UpdateEmployeeAsync(employee)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.UpdateEmployee(1, employee);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task UpdateEmployee_ReturnsBadRequest_WhenIdMismatch()
        {
            // Arrange
            var employee = new Employee { Id = 1, Name = "John Doe" };

            // Act
            var result = await _controller.UpdateEmployee(2, employee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public async Task DeleteEmployee_ReturnsNoContentResult()
        {
            // Arrange
            _mockEmployeeService.Setup(service => service.DeleteEmployeeAsync(1)).Returns(Task.CompletedTask);

            // Act
            var result = await _controller.DeleteEmployee(1);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }
    }
}
