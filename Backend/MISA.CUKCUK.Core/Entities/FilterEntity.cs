using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class FilterEntity
    {
        /// <summary>
        /// Danh sách điều kiện
        /// </summary>
        public IEnumerable<Condition>? Conditions { get; set; }
        /// <summary>
        /// Danh sách sắp xếp
        /// </summary>
        public IEnumerable<Sorter>? Sorters { get; set; }
        /// <summary>
        /// Số bản ghi trên trang
        /// </summary>
        public int? PageSize { get; set; }
        /// <summary>
        /// Số trang
        /// </summary>
        public int? PageIndex { get; set; }
    }
}
