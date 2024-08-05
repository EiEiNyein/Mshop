using Mshop.DB;
using Mshop.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mshop.Controllers
{
    public class MasterDataController : ApiController
    {
        #region Customer 25 Feb 22
        [HttpGet]
        [ActionName("GetCustomerInfo")]
        public async Task<IHttpActionResult> GetCustomerInfo()
        {
            DataTable dt = await QueryDAL.GetCustomerInfo();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteCustomer")]
        public async Task<IHttpActionResult> DeleteCustomer(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteCustomer(id);
            return Ok(msg);
        }

        #endregion

        #region MobileType Feb 28
        [HttpGet]
        [ActionName("GetMobileType")]
        public async Task<IHttpActionResult> GetMobileType()
        {
            DataTable dt = await QueryDAL.GetMobileType();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteMobileType")]
        public async Task<IHttpActionResult> DeleteMobileType(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteMobileType(id);
            return Ok(msg);
        }

        #endregion

        #region MobileModel Feb 28
        [HttpGet]
        [ActionName("GetMobileModel")]
        public async Task<IHttpActionResult> GetMobileModel()
        {
            DataTable dt = await QueryDAL.GetMobileModel();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetModel")]
        public async Task<IHttpActionResult> GetModel(string id)
        {

            string sql = "select Typeid,Model,Price from MobileModel where Mobileid=@mobileid";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@mobileid", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }


        [HttpGet]
        [ActionName("GetPrice")]
        public async Task<IHttpActionResult> GetPrice(int id)
        {

            string sql = "select Typeid,Model,Price,Color,Warranty from MobileModel where Typeid=@typeid";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@typeid", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }
        [HttpGet]
        [ActionName("GetMobile")]
        public async Task<IHttpActionResult> GetMobile(int id)
        {

            string sql = "select * from MobileType m inner join MobileModel l on l.Mobileid=m.Mobileid where l.Typeid=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteModel")]
        public async Task<IHttpActionResult> DeleteModel(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteModel(id);
            return Ok(msg);
        }
        #endregion

        #region Employee Feb 28
        [HttpGet]
        [ActionName("GetEmployee")]
        public async Task<IHttpActionResult> GetEmployee()
        {
            DataTable dt = await QueryDAL.GetEmployee();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetEmployeeName")]
        public async Task<IHttpActionResult> GetEmployeeName()
        {
            DataTable dt = await QueryDAL.GetEmployeeName();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteEmployee")]
        public async Task<IHttpActionResult> DeleteEmployee(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteEmployee(id);
            return Ok(msg);
        }
        #endregion

        #region SaleInfo Feb 28
        [HttpGet]
        [ActionName("GetSaleInfo")]
        public async Task<IHttpActionResult> GetSaleInfo()
        {
            DataTable dt = await QueryDAL.GetSaleInfo();
            return Ok(dt);
        }
        [HttpGet]
        [ActionName("GetBox")]
        public async Task<IHttpActionResult> GetBox()
        {
            DataTable dt = await QueryDAL.GetBox();
            return Ok(dt);
        }
        [HttpGet]
        [ActionName("SearchInfo")]
        public async Task<IHttpActionResult> SearchInfo(string sdate, string edate, string model, string empname)
        {
            DataTable dt = await QueryDAL.SearchInfo(sdate, edate, model, empname);
            return Ok(dt);
        }
        [HttpGet]
        [ActionName("SearchCum")]
        public async Task<IHttpActionResult> SearchCum(string name, string ph)
        {
            ph = !string.IsNullOrEmpty(ph) ? ph : "";
            name = !string.IsNullOrEmpty(name) ? name : "";
            string sql = @"select s.Saleid,c.CustomerName,c.Address,c.PhNo,t.MobileName,m.Model,s.Color,m.Price,s.Date,m.Warranty,s.Qty,e.EmployeeName,s.TotalAmount from SaleInfo s
                            inner join MobileModel m on s.Typeid=m.Typeid
                            inner join Customer c on s.Customerid=c.Customerid
                            inner join Employee e on s.Employeeid = e.Employeeid
                            inner join MobileType t on t.Mobileid=m.Mobileid
							where  c.PhNo=@ph and c.CustomerName=@name";
            SqlParameter[] para = new SqlParameter[2];
            para[0] = new SqlParameter("@name", name);
            para[1] = new SqlParameter("@ph", ph);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            //DataTable dt = await QueryDAL.SearchCum(name, ph);
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteSaleInfo")]
        public async Task<IHttpActionResult> DeleteSaleInfo(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteSaleInfo(id);
            return Ok(msg);
        }
        #endregion

        #region Accessories March 31
        [HttpGet]
        [ActionName("GetAccessories")]
        public async Task<IHttpActionResult> GetAccessories()
        {
            DataTable dt = await QueryDAL.GetAccessories();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetBrandName")]
        public async Task<IHttpActionResult> GetBrandName()
        {
            DataTable dt = await QueryDAL.GetBrandName();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteExtra")]
        public async Task<IHttpActionResult> DeleteExtra(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteExtra(id);
            return Ok(msg);
        }

        [HttpGet]
        [ActionName("GetExtraType")]
        public async Task<IHttpActionResult> GetExtraType(string id)
        {

            string sql = "select distinct Type  from Extra where BrandName=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetExtraModel")]
        public async Task<IHttpActionResult> GetExtraModel(string id)
        {

           string sql = "select distinct Model from Extra where Type=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetExtraColor")]
        public async Task<IHttpActionResult> GetExtraColor(string id)
        {

            string sql = "select Aid,Color from Extra where Model=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            //DataTable dt = await QueryDAL.GetExtraColor(id);
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("GetExtraPrice")]
        public async Task<IHttpActionResult> GetExtraPrice(string id)
        {

            string sql = "select Aid,Price,Warranty  from Extra where Aid=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            return Ok(dt);
        }

        #endregion

        #region AccSale
        [HttpGet]
        [ActionName("GetAccSale")]
        public async Task<IHttpActionResult> GetAccSale()
        {
            DataTable dt = await QueryDAL.GetAccSale();
            return Ok(dt);
        }

        [HttpGet]
        [ActionName("DeleteAccSale")]
        public async Task<IHttpActionResult> DeleteAccSale(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteAccSale(id);
            return Ok(msg);
        }
        #endregion

        #region Order

        [HttpGet]
        [ActionName("ViewOrder")]
        public async Task<IHttpActionResult> ViewOrder()
        {
            DataTable dt = await QueryDAL.ViewOrder();
            return Ok(dt);
        }
        [HttpGet]
        [ActionName("DeleteOrder")]
        public async Task<IHttpActionResult> DeleteOrder(string id)
        {
            ReturnMessage msg = await QueryDAL.DeleteOrder(id);
            return Ok(msg);
        }

        #endregion
    }
}
