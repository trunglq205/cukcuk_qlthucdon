using MISA.CUKCUK.Core.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Entities
{
    public class Menu
    {
        #region Constructor
        public Menu()
        {

        }
        #endregion

        #region Property
        /// <summary>
        /// ID thực đơn
        /// </summary>
        [PrimaryKey]
        [PropertyName("ID thực đơn")]
        public Guid? MenuID { get; set; }

        /// <summary>
        /// Mã thực đơn
        /// </summary>
        [PropertyName("Mã thực đơn")]
        [NotEmpty]
        [Unique]
        public string? MenuCode { get; set; }

        /// <summary>
        /// Tên thực đơn
        /// </summary>
        [PropertyName("Tên thực đơn")]
        [NotEmpty]
        public string? MenuName { get; set; }

        /// <summary>
        /// Nhóm thực đơn
        /// </summary>
        [PropertyName("Nhóm thực đơn")]
        public string? MenuGroup { get; set; }

        /// <summary>
        /// Đơn vị tính
        /// </summary>
        [PropertyName("Đơn vị tính")]
        [NotEmpty]
        public string? Unit { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        [PropertyName("Giá bán")]
        [NotEmpty]
        [Number]
        public decimal? Price { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        [PropertyName("Giá vốn")]
        [Number]
        public decimal? Investment { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        [PropertyName("Mô tả")]
        public string? Description { get; set; }

        /// <summary>
        /// Chế biến tại
        /// </summary>
        [PropertyName("Chế biến tại")]
        public string? CookingAddress { get; set; }

        /// <summary>
        /// Xuất hiện trong thực đơn
        /// </summary>
        [PropertyName("Hiện trong thực đơn")]
        public bool? ShowInMenus { get; set; }

        /// <summary>
        /// Ảnh đại diện
        /// </summary>
        [PropertyName("Ảnh đại diện")]
        public string? Picture { get; set; }

        [MISAInsertQuery]
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
