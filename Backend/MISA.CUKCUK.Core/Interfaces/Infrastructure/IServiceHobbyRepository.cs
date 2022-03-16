using MISA.CUKCUK.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Infrastructure
{
    public interface IServiceHobbyRepository
    {
        /// <summary>
        /// Lấy danh sách sở thích phục vụ theo ID thực đơn
        /// </summary>
        /// <param name="menuID"></param>
        /// <returns></returns>
        /// Created By LQTrung(26/02/2022)
        IEnumerable<object> GetByMenuID (Guid menuID);

        /// <summary>
        /// Thêm sở thích phục vụ
        /// </summary>
        /// <param name="serviceHobby"></param>
        /// <returns></returns>
        /// Created By LQTrung(26/02/2022)
        int Insert(ServiceHobby serviceHobby);

        /// <summary>
        /// Xóa sở thích phục vụ
        /// </summary>
        /// <param name="menuID"></param>
        /// <param name="hobbyID"></param>
        /// <returns></returns>
        /// Created By LQTrung(26/02/2022)
        int Delete(Guid menuID, Guid hobbyID);
    }
}
