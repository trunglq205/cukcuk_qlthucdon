using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Page<TypeEntity>
    {
        #region Properties

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public int TotalPages { get; set; }
        /// <summary>
        /// Tổng só bản ghi
        /// </summary>
        public long TotalRecords { get; set; }
        /// <summary>
        /// Các bản ghi
        /// </summary>
        public IEnumerable<TypeEntity> Data { get; set; }
        #endregion

        #region Constructors
        public Page(int totalPages, long totalRecords, IEnumerable<TypeEntity> entity)
        {
            TotalPages = totalPages;
            TotalRecords = totalRecords;
            Data = entity;
        }
        #endregion
    }
}
