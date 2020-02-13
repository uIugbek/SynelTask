using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Data.Entities
{
    /// <summary>
    /// Entity Model of Employees Table
    /// </summary>
    [Table("Employees")]
    public class Employee : EntityBase
    {
        /// <summary>
        /// Payroll Number of Employee
        /// </summary>
        public string PayrollNumber { get; set; }

        /// <summary>
        /// Full name of Employee
        /// </summary>
        public string Forenames { get; set; }

        /// <summary>
        /// Surname of Employee
        /// </summary>
        public string Surname { get; set; }

        /// <summary>
        /// Date of birth of Employee
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Telephone of Employee
        /// </summary>
        public string Telephone { get; set; }

        /// <summary>
        /// Mobile Telephone of Employee
        /// </summary>
        public string Mobile { get; set; }

        /// <summary>
        /// Address of Employee
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Additional data of address of Employee
        /// </summary>
        public string Address2 { get; set; }

        /// <summary>
        /// Postal code of Employee
        /// </summary>
        public string Postcode { get; set; }

        /// <summary>
        /// EMail address of Employee
        /// </summary>
        [DataType(DataType.EmailAddress)]
        public string EMail { get; set; }

        /// <summary>
        /// Start date of employment
        /// </summary>
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }
    }
}
