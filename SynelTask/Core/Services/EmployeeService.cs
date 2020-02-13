using SynelTask.Core.Data.Entities;
using SynelTask.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Services
{
    /// <summary>
    /// Performs CRUD and other operations on the Employee table
    /// </summary>
    public class EmployeeService : EntityService<Employee>
    {
        private readonly EmployeeRepository _repository;

        /// <summary>
        /// Initializes a new instance of EmployeeRepository class, 
        /// that performs database operations on the table Employee
        /// </summary>
        /// <param name="repository">A new instance of EmployeeRepository class</param>
        public EmployeeService(EmployeeRepository repository)
           : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Imports data of employees from given file path
        /// </summary>
        /// <param name="filePath">Path of .csv file that contains employees data</param>
        /// <returns>Returns count of imported rows</returns>
        public int Import(string filePath)
        {
            int addedRows = 0;
            if (File.Exists(filePath))
            {
                using (var reader = new StreamReader(filePath))
                {
                    var index = -1;
                    while (!reader.EndOfStream)
                    {
                        index++;
                        var line = reader.ReadLine();
                        var values = line.Split(',');

                        if (index == 0)
                            continue;

                        if (values.Length == 11)
                        {
                            Employee employee = new Employee
                            {
                                PayrollNumber = values[0],
                                Forenames = values[1],
                                Surname = values[2],
                                DateOfBirth = DateTime.ParseExact(values[3], "dd/M/yyyy", CultureInfo.InvariantCulture),
                                Telephone = values[4],
                                Mobile = values[5],
                                Address = values[6],
                                Address2 = values[7],
                                Postcode = values[8],
                                EMail = values[9],
                                StartDate = DateTime.ParseExact(values[10], "dd/M/yyyy", CultureInfo.InvariantCulture)
                            };
                            addedRows++;
                            _repository.Add(employee);
                        }
                    }
                    if (_repository.Commit())
                        return addedRows;
                    else
                        return 0;
                }
            }
            return 0;
        }
    }
}
