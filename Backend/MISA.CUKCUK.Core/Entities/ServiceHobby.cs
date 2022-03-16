using MISA.CUKCUK.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class ServiceHobby
    {
        #region Constructor
        public ServiceHobby()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// ID thực đơn
        /// </summary>
        [PropertyName("ID thực đơn")]
        [NotEmpty]
        public Guid? MenuID { get; set; }

        /// <summary>
        /// ID sở thích
        /// </summary>
        [PropertyName("ID sở thích")]
        [NotEmpty]
        public Guid? HobbyID { get; set; }

        public virtual Menu? Menu { get; set; }
        public virtual Hobby? Hobby { get; set; }  

        /// <summary>
        /// Thời gian tạo bản ghi
        /// </summary>
        [PropertyName("Thời gian tạo")]
        public DateTime? CreatedDate => DateTime.Now;

        /// <summary>
        /// Người tạo bản ghi
        /// </summary>
        [PropertyName("Người tạo")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian chỉnh sửa
        /// </summary>
        [PropertyName("Thời gian chỉnh sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa
        /// </summary>
        [PropertyName("Người tạo")]
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
