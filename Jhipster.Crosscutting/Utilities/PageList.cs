using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jhipster.Crosscutting.Utilities
{
    public class PagedList<T> where T : class
    {
        public PagedList()
        {
            Data = new List<T>();
        }

        public PagedList(IEnumerable<T> _Data, int _TotalCount)
        {
            Data = _Data;
            TotalCount = _TotalCount;
        }
        public IEnumerable<T> Data { get; set; }
        public int TotalCount { get; set; }
    }
}
