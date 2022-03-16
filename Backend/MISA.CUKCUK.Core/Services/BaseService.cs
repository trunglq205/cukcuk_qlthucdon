﻿using MISA.CUKCUK.Core.Attribute;
using MISA.CUKCUK.Core.Exception;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MISA.CUKCUK.Core.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Services
{
    public class BaseService<TypeEntity> : IBaseService<TypeEntity>
    {
        #region Fields
        IBaseRepository<TypeEntity> _baseRepository;
        #endregion

        #region Constructor
        public BaseService(IBaseRepository<TypeEntity> baseRepository)
        {
            _baseRepository = baseRepository;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Xử lí nghiệp vụ thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số bản ghi đã thêm mới</returns>
        /// Created by LQTrung(26/02/2022)
        public virtual int InsertService(TypeEntity entity)
        {
            var entityId = Guid.Empty;
            //Xử lý validate dữ liệu:
            //Validate chung:
            ValidateData(entity, entityId);
            //Validate riêng:
            ValidateEntity(entity);

            //Thực hiện thêm mới vào database:
            var res = _baseRepository.Insert(entity);
            return res;
        }

        /// <summary>
        /// Xứ lí nghiệp vụ cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi đã cập nhật</returns>
        /// Created by LQTrung(26/02/2022)
        public virtual int UpdateService(TypeEntity entity, Guid entityId)
        {
            //Xử lý validate dữ liệu:
            //Validate chung:
            ValidateData(entity, entityId);
            //Validate riêng:
            ValidateEntity(entity);

            //Thực hiện thêm mới vào database:
            var res = _baseRepository.Update(entity, entityId);
            return res;
        }
        #endregion

        #region Methods Support
        /// <summary>
        /// Lấy ra tên hiển thị của thuộc tính
        /// </summary>
        /// <param name="property"></param>
        /// <returns></returns>
        protected string GetDisplayName(PropertyInfo property)
        {
            var nameDisplay = string.Empty;
            //Lấy ra tên của property:
            var propertyNames = property.GetCustomAttributes(typeof(PropertyName), true);
            if (propertyNames.Length > 0)
            {
                nameDisplay = (propertyNames[0] as PropertyName).Name;
            }
            return nameDisplay;
        }

        /// <summary>
        /// Validate dữ liệu chung
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <exception cref="MISAValidateException"></exception>
        /// Created by LQTrung(26/02/2022)
        protected void ValidateData(TypeEntity entity, Guid? entityId)
        {
            //Lấy ra tất cả các property
            var properties = typeof(TypeEntity).GetProperties();

            //Lấy ra các property được đánh dấu không được phép để trống - có Attribute là NotEmpty:
            var propNotEmpties = typeof(TypeEntity).GetProperties().Where(p => System.Attribute.IsDefined(p, typeof(NotEmpty)));
            foreach (var property in propNotEmpties)
            {
                var propValue = property.GetValue(entity);
                var propName = property.Name;
                //Lấy ra tên của thuộc tính
                var nameDisplay = GetDisplayName(property);
                //Kiểm tra xem thuộc tính có bị trống hay không?
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    nameDisplay = nameDisplay == string.Empty ? propName : nameDisplay;
                    throw new MISAValidateException(String.Format(Core.Resource.MISAResourceVN.Property_NotEmpty, nameDisplay));
                }
            }

            //Lấy ra các property được đánh dấu không được trùng lặp - có Attribute là Unique:
            var propUnique = typeof(TypeEntity).GetProperties().Where(p => System.Attribute.IsDefined(p, typeof(Unique)));
            foreach (var property in propUnique)
            {
                var propValue = property.GetValue(entity);
                var propName = property.Name;
                //Lấy ra tên của thuộc tính
                var nameDisplay = GetDisplayName(property);
                //Kiểm tra xem thuộc tính có bị trùng lặp hay không?
                var isDuplicate = _baseRepository.CheckDuplicate(propName, propValue.ToString(), entityId);
                if (isDuplicate)
                {
                    nameDisplay = nameDisplay == string.Empty ? propName : nameDisplay;
                    throw new MISAValidateException(String.Format(Core.Resource.MISAResourceVN.Property_Unique, nameDisplay));
                }
            }


            //Kiểm tra định dạng số
            var propNumber = typeof(TypeEntity).GetProperties().Where(p => System.Attribute.IsDefined(p, typeof(Number)));
            foreach (var property in propNumber)
            {
                var propValue = property.GetValue(entity);
                var propName = property.Name;
                //Lấy ra tên của thuộc tính
                var nameDisplay = GetDisplayName(property);
                if (propValue != null)
                {
                    if (!decimal.TryParse(propValue.ToString(), out decimal num))
                    {
                        nameDisplay = nameDisplay == string.Empty ? propName : nameDisplay;
                        throw new MISAValidateException(String.Format(Core.Resource.MISAResourceVN.ErrorNumber, nameDisplay));
                    }
                }
            }
        }

        /// <summary>
        /// Validate dữ liệu riêng
        /// </summary>
        /// <param name="entity"></param>
        /// Created by LQTrung(26/02/2022)
        protected virtual void ValidateEntity(TypeEntity entity)
        {

        }
        #endregion
    }
}
