using Microsoft.EntityFrameworkCore;
using SynelTask.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Data
{
    /// <summary>
    /// A ApplicationDbContext represents a session with the database and 
    /// can be used to query and save instances of your entities.
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Initializes options for ApplicationDbContext
        /// </summary>
        /// <param name="options"></param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        /// <summary>
        /// Employees table
        /// </summary>
        public DbSet<Employee> Employees { get; set; }
    }
}
