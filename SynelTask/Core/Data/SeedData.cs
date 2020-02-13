using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Data
{
    /// <summary>
    /// Initializes primary data when database created
    /// </summary>
    public static class SeedData
    {
        /// <summary>
        /// Method for initialization of primary data
        /// </summary>
        public static void Initialize(ApplicationDbContext context)
        {
            // Look for any employees.
            if (!context.Employees.Any())
            {
                // TODO: Seed employee data
            }

            context.SaveChanges();
        }
    }
}
