using MISA.CUKCUK.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Condition
    {
        /// <summary>
        /// Tên thuộc tính
        /// </summary>
        public string Property { get; set; }
        /// <summary>
        /// Giá trị
        /// </summary>
        public object Value { get; set; }
        /// <summary>
        /// Toán tử
        /// </summary>
        public FilterOperator Operator { get; set; }
    }
}
