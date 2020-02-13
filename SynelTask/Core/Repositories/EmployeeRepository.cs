using SynelTask.Core.Data;
using SynelTask.Core.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Repositories
{
    /// <summary>
    /// An Employee Repository, that used to perform CRUD operations on table Employee
    /// </summary>
    public class EmployeeRepository : EntityRepository<Employee>
    {
        private readonly ApplicationDbContext dbcontext;

        /// <summary>
        /// Initializes an instance of DbContext
        /// </summary>
        /// <param name="context">An instance of DbContext</param>
        public EmployeeRepository(ApplicationDbContext context)
            : base(context)
        {
            this.dbcontext = context;
        }
    }
}
