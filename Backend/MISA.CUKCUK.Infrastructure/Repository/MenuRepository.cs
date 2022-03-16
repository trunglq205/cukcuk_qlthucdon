using Dapper;
using MISA.CUKCUK.Core.Entities;
using MISA.CUKCUK.Core.Interfaces.Infrastructure;
using MySqlConnector;
using SqlKata.Compilers;
using SqlKata.Execution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.Infrastructure.Repository
{
    public class MenuRepository : BaseRepository<Menu>, IMenuRepository
    {

        /// <summary>
        /// Kiểm tra mã thực đơn có tồn tại trong database
        /// </summary>
        /// <param name="menuCode"></param>
        /// <returns></returns>
        /// Created by LQTrung(21/01/2022)
        public bool CheckMenuCode(string menuCode)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                // Khai báo truy vấn:
                var sqlCommand = "select * from menu where MenuCode = @MenuCode";
                DynamicParameters parameters = new DynamicParameters();
                parameters.Add("@MenuCode", menuCode);

                // Thực hiện truy vấn:
                var res = sqlConnection.QueryFirstOrDefault(sqlCommand, parameters);

                if (res != null)
                {
                    return true;
                }
                else return false;
            }
        }

        /// <summary>
        /// Phân trang, tìm kiếm
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        /// Created by LQTrung(28/02/2022)
        public Page<Menu> Filter(FilterEntity filter)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                var db = new QueryFactory(sqlConnection, new MySqlCompiler());
                // Khai báo truy vấn:
                var query = db.Query("view_menu");
                var conditions = filter.Conditions;
                var sorters = filter.Sorters;
                if (conditions != null && conditions.Any())
                {
                    foreach (var condition in conditions)
                    {
                        var column = condition.Property;
                        var value = condition.Value;
                        switch (condition.Operator)
                        {
                            case Core.Enum.FilterOperator.Contain:
                                query.WhereContains(column, value);
                                break;
                            case Core.Enum.FilterOperator.Equal:
                                query.Where(column, value);
                                break;
                            case Core.Enum.FilterOperator.StartWith:
                                query.WhereStarts(column, value);
                                break;
                            case Core.Enum.FilterOperator.EndWith:
                                query.WhereEnds(column, value);
                                break;
                            case Core.Enum.FilterOperator.NotContain:
                                query.WhereNotContains(column, value);
                                break;
                            case Core.Enum.FilterOperator.GreaterThan:
                                query.Where(column, ">", value);
                                break;
                            case Core.Enum.FilterOperator.GreaterThanOrEqual:
                                query.Where(column, ">=", value);
                                break;
                            case Core.Enum.FilterOperator.LessThan:
                                query.Where(column, "<", value);
                                break;
                            case Core.Enum.FilterOperator.LessThanOrEqual:
                                query.Where(column, "<=", value);
                                break;
                            default:
                                throw new Exception("Invalid operator");
                        }
                    }
                }
                if (sorters != null && sorters.Any())
                {
                    foreach (var sorter in sorters)
                    {
                        var column = sorter.Property;
                        switch (sorter.Order)
                        {
                            case Core.Enum.SortOrder.Ascending:
                                query.OrderBy(column);
                                break;
                            case Core.Enum.SortOrder.Descending:
                                query.OrderByDesc(column);
                                break;
                            default:
                                throw new Exception("Invalid sort order");
                        }
                    }
                }
                var pr = query.Paginate<Menu>(filter.PageIndex ?? 1, filter.PageSize ?? 25);
                var records = new List<Menu>(pr.PerPage);
                foreach (var record in pr.List)
                {
                    records.Add(record);
                }
                return new Page<Menu>(pr.TotalPages, pr.Count, records);
            }
        }

        /// <summary>
        /// Thêm mới thực đơn
        /// </summary>
        /// <param name="menu"></param>
        /// <returns></returns>
        /// Created by LQTrung(21/01/2022)
        public override int Insert(Menu menu)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var sqlTransaction = sqlConnection.BeginTransaction();
                DynamicParameters parameters = new DynamicParameters();


                // Thêm mới thực đơn
                menu.MenuID = Guid.NewGuid();
                var sqlCommand = "Proc_InsertMenu";
                parameters.Add("@m_MenuID", menu.MenuID);
                parameters.Add("@m_MenuCode", menu.MenuCode);
                parameters.Add("@m_MenuName", menu.MenuName);
                parameters.Add("@m_MenuGroup", menu.MenuGroup);
                parameters.Add("@m_Unit", menu.Unit);
                parameters.Add("@m_Price", menu.Price);
                parameters.Add("@m_Investment", menu.Investment);
                parameters.Add("@m_Description", menu.Description);
                parameters.Add("@m_CookingAddress", menu.CookingAddress);
                parameters.Add("@m_ShowInMenus", menu.ShowInMenus);
                parameters.Add("@m_Picture", menu.Picture);
                parameters.Add("@m_CreatedBy", menu.CreatedBy);

                var res = sqlConnection.Execute(sqlCommand, parameters, transaction: sqlTransaction, commandType: System.Data.CommandType.StoredProcedure);

                // Thêm mới sở thích phục vụ
                var serviceHobbies = menu.ServiceHobbies;

                foreach (var service in serviceHobbies)
                {
                    service.MenuID = menu.MenuID;
                    var sqlCommandServiceHobby = "insert into servicehobby (MenuID, HobbyID) values (@MenuID, @HobbyID)";
                    parameters.Add("@MenuID", service.MenuID);
                    parameters.Add("@HobbyID", service.Hobby.HobbyID);
                    var res2 = sqlConnection.Execute(sqlCommandServiceHobby, parameters, transaction: sqlTransaction);
                    res += res2;
                }
                sqlTransaction.Commit();
                sqlConnection.Close();
                return res;
            }
        }

        /// <summary>
        /// Cập nhật thông tin thực đơn
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="menuID"></param>
        /// <returns></returns>
        /// Created by LQTrung(21/01/2022)
        public override int Update(Menu menu, Guid menuID)
        {
            using (var sqlConnection = new MySqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                var sqlTransaction = sqlConnection.BeginTransaction();
                DynamicParameters parameters = new DynamicParameters();

                // Sửa thông tin thực đơn
                var res = base.Update(menu, menuID);

                // Truy vấn xóa các sở thích phục vụ theo thực đơn ID
                var sqlCommandDel = "delete from servicehobby where MenuID = @MenuID";
                parameters.Add("@MenuID", menuID);

                sqlConnection.Execute(sqlCommandDel, parameters, transaction: sqlTransaction);

                // Thêm mới các sở thích phục vụ
                var serviceHobbies = menu.ServiceHobbies;
                foreach (var service in serviceHobbies)
                {
                    service.MenuID = menuID;
                    var sqlCommandServiceHobby = "insert into servicehobby (MenuID, HobbyID) values (@MenuID, @HobbyID)";
                    parameters.Add("@MenuID", service.MenuID);
                    parameters.Add("@HobbyID", service.Hobby.HobbyID);
                    var res2 = sqlConnection.Execute(sqlCommandServiceHobby, parameters, transaction: sqlTransaction);
                    res += res2;
                }
                sqlTransaction.Commit();
                sqlConnection.Close();
                return res;
            }
        }
    }
}
