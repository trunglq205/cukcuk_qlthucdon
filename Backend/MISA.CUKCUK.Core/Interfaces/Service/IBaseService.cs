﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Core.Interfaces.Service
{
    public interface IBaseService<TypeEntity>
    {
        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// Created by LQTrung(26/02/2022)
        int InsertService(TypeEntity entity);

        /// <summary>
        /// Sửa dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by LQTrung(26/02/2022)
        int UpdateService(TypeEntity entity, Guid entityId);
    }
}
