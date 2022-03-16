using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Sorter
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string? Property { get; set; }
        /// <summary>
        /// Thứ tự sắp xếp
        /// </summary>
        public SortOrder Order { get; set; }
    }
}
