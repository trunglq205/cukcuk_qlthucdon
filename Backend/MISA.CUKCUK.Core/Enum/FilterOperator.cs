using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Enum
{
    public enum FilterOperator
    {
        /// <summary>
        /// Chứa
        /// </summary>
        Contain,
        /// <summary>
        /// Bằng
        /// </summary>
        Equal,
        /// <summary>
        /// Bắt đầu bằng
        /// </summary>
        StartWith,
        /// <summary>
        /// Kết thúc bằng
        /// </summary>
        EndWith,
        /// <summary>
        /// Không chứa
        /// </summary>
        NotContain,
        /// <summary>
        /// Lớn hơn
        /// </summary>
        GreaterThan,
        /// <summary>
        /// Lớn hơn bằng
        /// </summary>
        GreaterThanOrEqual,
        /// <summary>
        /// Nhỏ hơn
        /// </summary>
        LessThan,
        /// <summary>
        /// Nhỏ hơn bằng
        /// </summary>
        LessThanOrEqual
    }
}
