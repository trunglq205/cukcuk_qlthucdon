using Dapper;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infrastructure.Repository
{
    public class ServiceHobbyRepository : IServiceHobbyRepository
    {
        string ConnectionString = Core.Resource.MISAResourceVN.ConnectionString;
        public int Delete(Guid menuID, Guid hobbyID)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();

                //Khai báo truy vấn:
                var sqlCommand = $"delete from servicehobby where MenuID = @MenuID and HobbyID = @HobbyID";

                parameters.Add($"@MenuID", menuID);
                parameters.Add($"@HobbyID", hobbyID);
                //Thực hiện xóa:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }

        public IEnumerable<object> GetByMenuID(Guid menuID)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                //Khai báo truy vấn:
                var sqlCommand = "select * from view_servicehobby where MenuID = @MenuID";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuID", menuID);

                //Thực hiện truy vấn dữ liệu trong database:
                var serviceHobbies = sqlConnection.Query<object>(sqlCommand, parameters);
                return serviceHobbies;
            }
        }

        public int Insert(ServiceHobby serviceHobby)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                DynamicParameters parameters = new DynamicParameters();
                //Khai báo truy vấn:
                var columns = new StringBuilder();
                var columnsValue = new StringBuilder();
                string delimiter = "";
                var properties = typeof(ServiceHobby).GetProperties();
                foreach (var property in properties)
                {
                    //Lấy tên của Property:
                    var propName = property.Name;
                    columns.Append($"{delimiter}{propName}");
                    columnsValue.Append($"{delimiter}@{propName}");
                    delimiter = ",";

                    //Add giá trị của Property vào Parameters
                    var value = property.GetValue(serviceHobby);
                    parameters.Add($"@{property.Name}", value);
                }
                var sqlCommand = $"insert into servicehobby ({columns}) values ({columnsValue})";
                //Thực hiện thêm mới:
                var res = sqlConnection.Execute(sqlCommand, param: parameters);
                return res;
            }
        }
    }
}
