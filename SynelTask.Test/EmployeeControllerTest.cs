using SynelTask.Core.Services;
using System;
using Xunit;
using SynelTask.Core.Repositories;
using SynelTask.Core.Data.Entities;
using SynelTask.Controllers;
using Microsoft.AspNetCore.Mvc;
using SynelTask.Core.Data;
using SynelTask.Core.Kendo;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;
using Moq;
using System.Threading;
using System.Collections.Generic;

namespace SynelTask.Test
{
    public class EmployeeControllerTest
    {
        private ApplicationDbContext _context;
        public ApplicationDbContext Context
        {
            get
            {
                if (_context != null)
                    return _context;

                var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Sample;Trusted_Connection=True;MultipleActiveResultSets=true")
                .Options;

                _context = new ApplicationDbContext(options);
                return _context;
            }
        }

        [Fact]
        public void Index_ReturnsAnEmptyViewResult()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Index();

            // Assert
            Assert.IsAssignableFrom<ViewResult>(result);
            Assert.Null((result as ViewResult).ViewName);
        }

        [Fact]
        public void GetEmployees_ReturnsDataSourceResultOfEmployees()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);
            var testFilter = "{\"take\":20,\"skip\":0,\"page\":1,\"pageSize\":20}";

            // Act
            var result = controller.GetEmployees(testFilter);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<DataSourceResult<Employee>>(okResult.Value);
            Assert.NotNull(model);
            Assert.NotEmpty(model.Data);
        }

        [Fact]
        public void GetEmployees_ReturnsListOfEmployees()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.GetEmployees(null);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<List<Employee>>(okResult.Value);
            Assert.NotNull(model);
            Assert.NotEmpty(model);
        }

        [Fact]
        public void Create_CreatesAnEmployeeAndReturnOkResult()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Create(GetTestEmployee(0));

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Create_ReturnBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);
            var employee = GetTestEmployee(0);
            controller.ModelState.AddModelError("Surname", "The field is required");

            // Act
            var result = controller.Create(employee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Update_UpdatesEmployeeAndReturnOkResult()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);
            var employee = service.Create(GetTestEmployee(0));
            employee.Surname = "updated surname";

            // Act
            var result = controller.Update(employee.Id, employee);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Update_ReturnBadRequestWhenEmployeeIsNull()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Update(-1, GetTestEmployee(-1));

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Update_ReturnBadRequestWhenModelStateIsInvalid()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);
            var employee = service.Create(GetTestEmployee(0));
            controller.ModelState.AddModelError("Surname", "The field is required");

            // Act
            var result = controller.Update(employee.Id, employee);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Delete_DeletesEmployeeAndReturnOkResult()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);
            var employee = service.Create(GetTestEmployee(0));

            // Act
            var result = controller.Delete(employee.Id);

            // Assert
            Assert.IsType<OkResult>(result);
        }

        [Fact]
        public void Delete_ReturnBadRequestResultWhenEmployeeIsNotFound()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Delete(-1);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Import_ImportsFileAndReturnsAddedRowsCount()
        {
            // Arrange
            var ms = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(GetFileText()));
            IFormFile file = new FormFile(ms, 0, ms.Length, "data", "dataset.csv");
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Import(file);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var model = Assert.IsAssignableFrom<int>(okResult.Value);
            Assert.True(model > 0);
        }

        [Fact]
        public void Import_ReturnsBadRequestWhenFileIsNull()
        {
            // Arrange
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Import(null);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        [Fact]
        public void Import_ReturnsBadRequestWhenFileExtensionIsNotMatch()
        {
            // Arrange
            var csvFile = File.OpenRead("dataset.csv");
            var file = new FormFile(csvFile, 0, 0, "file", "dataset.xlsx");
            var repo = new EmployeeRepository(Context);
            var service = new EmployeeService(repo);
            var controller = new EmployeeController(service);

            // Act
            var result = controller.Import(file);

            // Assert
            Assert.IsType<BadRequestResult>(result);
        }

        // A new employee for testing
        public Employee GetTestEmployee(int id)
        {
            return new Employee
            {
                Id = id,
                PayrollNumber = "test payrol number",
                Forenames = "test forenames",
                Surname = "test surname",
                DateOfBirth = DateTime.Now,
                Telephone = "123456789",
                Address = "test address",
                Address2 = "test address2",
                Mobile = "987654321",
                Postcode = "test postcode",
                EMail = "test@test.ru",
                StartDate = DateTime.Now
            };
        }

        // A string data for testing import .csv file
        public string GetFileText()
        {
            return @"Personnel_Records.Payroll_Number,Personnel_Records.Forenames,Personnel_Records.Surname,Personnel_Records.Date_of_Birth,Personnel_Records.Telephone,Personnel_Records.Mobile,Personnel_Records.Address,Personnel_Records.Address_2,Personnel_Records.Postcode,Personnel_Records.EMail_Home,Personnel_Records.Start_Date
                    COOP08,John,William,26/01/1955,12345678,987654231,12 Foreman road,London,GU12 6JW,nomadic20@hotmail.co.uk,18/04/2013
                    JACK13,Jerry,Jackson,11/5/1974,2050508,6987457,115 Spinney Road,Luton,LU33DF,gerry.jackson@bt.com,18/04/2013
                    ";
        }
    }
}
