using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SynelTask.Core.Kendo
{
    public class DataSourceRequest
    {
        public DataSourceRequest()
        {
            Take = 10;
            Skip = 0;
        }

        public bool All { get; set; }
        public int Take { get; set; }
        public int Skip { get; set; }
        public IEnumerable<Sort> Sort { get; set; }
        public Filter Filter { get; set; }
    }
}
