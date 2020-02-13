using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SynelTask.Core.Data;
using SynelTask.Core.Data.Entities;
using SynelTask.Core.Kendo;
using SynelTask.Core.Repositories;
using SynelTask.Core.Services;

namespace SynelTask.Controllers
{
    /// <summary>
    /// Performs CRUD and other operations on the Employee table
    /// </summary>
    public class EmployeeController : Controller
    {
        private readonly EmployeeService _service;

        /// <summary>
        /// Initializes a new instance of the EmployeeService class.
        /// EmployeeService used to perform CRUD and other additional operations on the Employee table
        /// </summary>
        /// <param name="service">An instance of the EmployeeService class</param>
        public EmployeeController(EmployeeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Index Page of Controller
        /// </summary>
        /// <returns>Returns ViewResult</returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Gets list of Employees ordered by Surname
        /// </summary>
        /// <param name="filters">
        /// filter params such as take, skip, sort and filter for kendo grid
        /// </param>
        /// <returns>
        /// If filters exist returns DataSourceResult of employees for kendo grid, 
        /// else returns list of employees
        /// </returns>
        [HttpGet]
        public IActionResult GetEmployees([FromQuery] string filters)
        {
            var query = _service.GetAllAsQueryable().OrderBy(o => o.Surname);

            if (!string.IsNullOrEmpty(filters))
            {
                var _filters = Newtonsoft.Json.JsonConvert.DeserializeObject<DataSourceRequest>(filters);
                var data = query.ToDataSourceResult(_filters.Take, _filters.Skip, _filters.Sort, _filters.Filter, _filters.All);

                return Ok(data);
            }
            return Ok(query.ToList());
        }

        /// <summary>
        /// Creates a new employee
        /// </summary>
        /// <param name="employee">Data model of employee</param>
        /// <returns>If creates a new employee successfully returns OkResult, else returns BadRequestResult</returns>
        [HttpPost]
        public IActionResult Create([FromBody]Employee employee)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _service.Create(employee);
                    return Ok();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Updates an existing employee
        /// </summary>
        /// <param name="id">employee's id</param>
        /// <param name="employee">Data model of employee</param>
        /// <returns>If updates employee data successfully returns OkResult, else returns BadRequestResult</returns>
        [HttpPut]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            var ent = _service.GetSingle(id);

            if (ent == null)
                return BadRequest();

            if (ModelState.IsValid)
            {
                try
                {
                    if (_service.Update(employee))
                        return Ok();
                    else
                        return BadRequest();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        /// <summary>
        /// Deletes an existing employee by id
        /// </summary>
        /// <param name="id">employee's id</param>
        /// <returns>If deletes employee successfully returns OkResult, else returns BadRequestResult</returns>
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            try
            {
                if (_service.Delete(id))
                    return Ok();
                else
                    return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Imports data of employees from .csv file
        /// </summary>
        /// <param name="file">.csv file that contains employees data</param>
        /// <returns>If imports employees successfully returns count of imported rows, else returns BadRequestResult</returns>
        [HttpPost]
        public IActionResult Import(IFormFile file)
        {
            if (file == null)
                return BadRequest();

            string permittedExtension = ".csv";
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtension.Equals(ext))
                return BadRequest();

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            var filePath = Path.Combine(path, Path.GetRandomFileName() + ext);
            using (var stream = System.IO.File.Create(filePath))
            {
                file.CopyTo(stream);
            }

            int addedRows = _service.Import(filePath);
            if (addedRows > 0)
                return Ok(addedRows);

            return BadRequest();
        }
    }
}
