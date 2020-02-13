using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Kendo
{
    public class DataSourceResult<T>
    {
        /// <summary>
        /// Represents a single page of processed data.
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// The total number of records available.
        /// </summary>
        public int Total { get; set; }
    }
}
