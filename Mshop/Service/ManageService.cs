using Mshop.DB;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Mshop.Service
{
    public class ManageService
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        public static Task<DataTable> GetLoginUserRole(string userName)
        {
            DataTable dt = new DataTable();
            return Task.Run(() =>
            {
                Mobile_ShopEntities db = new Mobile_ShopEntities();
                string sql = @"select U.Id as UserID,R.Name as Role
                             from AspNetUsers U
							 inner join AspNetUserRoles ur on U.Id=ur.UserId
							 inner join AspNetRoles R on R.Id=ur.RoleId
			                 where U.Email=@Email";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.SelectCommand.Parameters.AddWithValue("@Email", userName);
                adpt.Fill(dt);
                return dt;
            });
        }

        public static Task<DataTable> GetRoleByDep(string userRole)
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = string.Empty;
                if (userRole.Equals("Admin"))
                {
                    sql = @"select Id,Name from AspNetRoles where  Id<>2 and Id<>3 ORDER BY Id";
                }
                else if (userRole.Equals("Phone_Sale"))
                {
                    sql = @"select Id,Name from AspNetRoles where Id<>1 and Id<>3 ORDER BY Id";
                }
                else if (userRole.Equals("Phone_Service"))
                {
                    sql = @"select Id,Name from AspNetRoles where Id<>1 and Id<>2 ORDER BY Id";
                }
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;
            });
        }
    }
}