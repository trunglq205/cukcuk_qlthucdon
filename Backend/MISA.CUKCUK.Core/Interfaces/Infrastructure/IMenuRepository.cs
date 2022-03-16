using MISA.CUKCUK.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Infrastructure
{
    public interface IMenuRepository : IBaseRepository<Menu>
    {
        /// <summary>
        /// Kiểm tra mã thực đơn có tồn tại
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        /// Created By LQTrung(26/02/2022)
        bool CheckMenuCode(string menuCode);

        /// <summary>
        /// Phân trang, tìm kiếm
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// Created By LQTrung(26/02/2022)
        Page<Menu> Filter(FilterEntity filter);
    }
}
