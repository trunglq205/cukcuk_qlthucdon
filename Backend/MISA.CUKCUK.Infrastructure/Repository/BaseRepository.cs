using Dapper;
using MISA.CUKCUK.Core.Attribute;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infrastructure.Repository
{
    public class BaseRepository<TypeEntity> : IBaseRepository<TypeEntity>
    {
        #region Fields
        //Khai báo thông tin CSDL:
        protected readonly string ConnectionString = Core.Resource.MISAResourceVN.ConnectionString;
        //Khai báo tên đối tượng
        string _className = typeof(TypeEntity).Name;
        //Lấy ra tất cả các thuộc tính:
        PropertyInfo[] _properties = typeof(TypeEntity).GetProperties();
        //Lấy ra thuộc tính được đánh dấu là primaryKey
        protected PropertyInfo _primaryKey = typeof(TypeEntity).GetProperties().First(x => x.IsDefined(typeof(PrimaryKey)));
        #endregion

        #region Methods
        /// <summary>
        /// Lấy toàn bộ danh sách
        /// </summary>
        /// <returns>Danh sách đối tượng</returns>
        /// Created by LQTrung(26/02/2022)
        public IEnumerable<TypeEntity> GetAll()
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Thực hiện truy vấn dữ liệu trong databse:
                var entities = sqlConnection.Query<TypeEntity>($"select * from {_className}");
                return entities;
            }
        }

        /// <summary>
        /// Lấy dữ liệu theo entityId
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Một đối tượng</returns>
        /// Created by LQTrung(26/02/2022)
        public TypeEntity GetById(Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Khai báo truy vấn:
                var sqlCommand = $"select * from {_className} where {_primaryKey.Name} = @{_primaryKey.Name}";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add($"@{_primaryKey.Name}", entityId);

                //Thực hiện truy vấn dữ liệu trong database:
                var entity = sqlConnection.QueryFirstOrDefault<TypeEntity>(sqlCommand, param: parameters);
                return entity;
            }
        }

        /// <summary>
        /// Thêm mới dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <returns>Số bản ghi đã thêm mới</returns>
        /// Created by LQTrung (26/02/2022)
        public virtual int Insert(TypeEntity entity)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                //tạo mới giá trị cho PrimaryKey:
                _primaryKey.SetValue(entity, Guid.NewGuid());

                //Khai báo truy vấn:
                var columns = new StringBuilder();
                var columnsValue = new StringBuilder();
                string delimiter = "";
                foreach (var property in _properties)
                {
                    //kiểm tra property được đánh dấu là MISAInsertQuery không
                    var insertQuery = Attribute.IsDefined(property, typeof(MISAInsertQuery));
                    if (insertQuery == true)
                    {
                        continue;
                    }

                    //Lấy tên của Property:
                    var propName = property.Name;
                    columns.Append($"{delimiter}{propName}");
                    columnsValue.Append($"{delimiter}@{propName}");
                    delimiter = ",";

                    //Add giá trị của Property vào Parameters
                    var value = property.GetValue(entity);
                    parameters.Add($"@{property.Name}", value);
                }

                var sqlCommand = $"insert into {_className} ({columns}) values ({columnsValue})";
                //Thực hiện thêm mới:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Cập nhật dữ liệu
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi đã cập nhật</returns>
        /// Created by LQTrung (26/01/2022)
        public virtual int Update(TypeEntity entity, Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                //Khai báo truy vấn:
                var sqlColumns = new StringBuilder();
                string delimiter = "";
                foreach (var property in _properties)
                {
                    if (property != _primaryKey)
                    {
                        //kiểm tra property được đánh dấu là MISAInsertQuery không
                        var insertQuery = Attribute.IsDefined(property, typeof(MISAInsertQuery));
                        if (insertQuery == true)
                        {
                            continue;
                        }

                        //Lấy tên của Property:
                        var propName = property.Name;
                        sqlColumns.Append($"{delimiter}{propName}=@{propName}");
                        delimiter = ",";
                        //Add giá trị của Property vào Parameters
                        var value = property.GetValue(entity);
                        parameters.Add($"@{property.Name}", value);
                    }
                }
                var sqlCommand = $"update {_className} set {sqlColumns} where {_primaryKey.Name} = @{_primaryKey.Name}";
                parameters.Add($"@{_primaryKey.Name}", entityId);
                //Thực hiện cập nhật:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Xóa dữ liệu
        /// </summary>
        /// <param name="entityId"></param>
        /// <returns>Số bản ghi đã xóa</returns>
        /// Created by LQTrung(21/01/2022)
        public int Delete(Guid entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                //Khai báo truy vấn:
                var sqlCommand = $"delete from {_className} where {_primaryKey.Name} = @{_primaryKey.Name}";

                parameters.Add($"@{_primaryKey.Name}", entityId);
                //Thực hiện xóa:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        /// <summary>
        /// Kiểm tra trùng
        /// </summary>
        /// <param name="propName"></param>
        /// <param name="propValue"></param>
        /// <param name="entityId"></param>
        /// <returns></returns>
        /// Created by LQTrung(26/02/2022)
        public bool CheckDuplicate(string propName, string propValue, Guid? entityId)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                var sqlCommand = string.Empty;
                if (entityId == null)
                {
                    //khai báo truy vấn check trùng cho Insert
                    sqlCommand = $"select {propName} from {_className} where {propName} = @{propName}";
                }
                else
                {
                    //khai báo truy vấn check trùng cho Update
                    sqlCommand = $"select {propName} from {_className} where {propName} = @{propName} and {_primaryKey.Name} != @{_primaryKey.Name}";
                    parameters.Add($"@{_primaryKey.Name}", entityId);
                }
                parameters.Add($"@{propName}", propValue);
                //Thực hiện truy vấn
                var res = sqlConnection.QueryFirstOrDefault(sqlCommand, param: parameters);
                if (res != null)
                {
                    return true;
                }
                return false;
            }
        }
        #endregion
    }
}
