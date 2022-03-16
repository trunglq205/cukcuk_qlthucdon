using MISA.CUKCUK.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Hobby
    {
        #region Constructor
        public Hobby()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// ID sở thích
        /// </summary>
        [PrimaryKey]
        [PropertyName("ID sở thích")]
        public Guid HobbyID { get; set; }

        /// <summary>
        /// Tên sở thích
        /// </summary>
        [PropertyName("Tên sở thích")]
        [NotEmpty]
        public string HobbyName { get; set; }

        /// <summary>
        /// Tiền thu thêm
        /// </summary>
        [PropertyName("Tiền thu thêm")]
        [NotEmpty]
        public decimal ExtraPrice { get; set; }

        public virtual IEnumerable<ServiceHobby>? ServiceHobbies { get; set; }

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
        /// Thời gian chỉnh sửa bản ghi
        /// </summary>
        [PropertyName("Thời gian chỉnh sửa")]
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người chỉnh sửa bản ghi
        /// </summary>
        [PropertyName("Người chỉnh sửa")]
        public string? ModifiedBy { get; set; }
        #endregion
    }
}
