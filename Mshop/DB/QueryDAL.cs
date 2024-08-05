using Mshop.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Mshop.DB
{
    public class QueryDAL
    {
        public static string conStr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        #region Customer 25 Feb 22
        public static Task<DataTable> GetCustomerInfo()
        {
            return Task.Run(() =>
            {
                string sql = @"select * from Customer";

                using (var adpt = new SqlDataAdapter(sql, conStr))
                {
                    adpt.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    return dt;
                }
            });
        }



        public async static Task<ReturnMessage> SaveCustomerInfo(CustomerInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities ms = new Mobile_ShopEntities())
            {
                string cid = "";



                string sql = "SELECT Customerid from Customer ";
                var ginlist = ms.Database.SqlQuery<string>(
                   sql).ToList();
                if (ginlist.Count == 0)
                {
                    cid = "C01";

                }
                else
                {
                    string dstr = ginlist.Max(a => a);
                    int id = int.Parse(dstr.Substring(1));
                    id++;
                    cid = "C" + id.ToString("D2");
                }
                Customer c = new Customer();
                c.Customerid = cid;
                c.CustomerName = info.CustomerName;
                c.Address = info.Address;
                c.PhNo = info.PhNo;
                ms.Customers.Add(c);
                try
                {
                    await ms.SaveChangesAsync();
                    msg.status = true;
                    msg.content = "Successfully Saved!";
                }
                catch (DbEntityValidationException ex)
                {
                    #region
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string str = string.Format("Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            msg.content += str;
                        }
                    }
                    msg.status = false;
                    #endregion
                }
                catch (DbUpdateException e)
                {
                    foreach (var error in e.Entries)
                    {
                        msg.content += error.Entity.GetType().Name;
                    }
                    msg.status = false;
                }
                catch (Exception e)
                {
                    msg.content += e.Message;
                    msg.status = false;
                }

            }
            return msg;
        }

        public static async Task<CustomerInfo> GetCustomerId(string id)
        {
            CustomerInfo info = new CustomerInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Customer mm = await db.Customers.SqlQuery("select * from Customer where Customerid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    info.Customerid = mm.Customerid;
                    info.CustomerName = mm.CustomerName;
                    info.Address = mm.Address;
                    info.PhNo = mm.PhNo;

                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateCustomer(CustomerInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Customer mm = await db.Customers.SqlQuery("select * from Customer where Customerid=@id", new SqlParameter("@id", info.Customerid)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    mm.CustomerName = info.CustomerName;
                    mm.Address = info.Address;
                    mm.PhNo = info.PhNo;

                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteCustomer(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();
                Customer mm = db.Customers.SqlQuery("select * from Customer where Customerid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.Customers.Remove(mm);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }

                }
                return msg;
            }
        }


        #endregion

        #region MobileType 25 Feb 22
        public static Task<DataTable> GetMobileType()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select * from MobileType";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }
        public static async Task<MobileTypeInfo> GetMobile(string id)
        {
            MobileTypeInfo info = new MobileTypeInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                MobileType mm = await db.MobileTypes.SqlQuery("select * from MobileType where Mobileid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (mm != null)
                {

                    info.Mobileid = mm.Mobileid;
                    info.MobileName = mm.MobileName;

                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateMobile(MobileTypeInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                MobileType mm = await db.MobileTypes.SqlQuery("select * from MobileType where Mobileid=@id", new SqlParameter("@id", info.Mobileid)).SingleOrDefaultAsync();
                if (mm != null)
                {

                    mm.MobileName = info.MobileName;

                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }
        public static async Task<ReturnMessage> SaveMobileType(MobileTypeInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {

                string Mobileid = "";



                string sql = "select Mobileid from MobileType";
                var ginlist = db.Database.SqlQuery<string>(
                   sql).ToList();
                if (ginlist.Count == 0)
                {
                    Mobileid = "M01";

                }
                else
                {
                    string dstr = ginlist.Max(a => a);
                    int id = int.Parse(dstr.Substring(1));
                    id++;
                    Mobileid = "M" + id.ToString("D2");
                }


                MobileType mt = db.MobileTypes.Where(x => x.MobileName == info.MobileName).FirstOrDefault();
                if (mt != null)
                {
                    msg.status = false;
                    msg.content = "Duplicate Mobile Name!";
                    return msg;
                }
                else
                {

                    MobileType mobile = new MobileType();
                    mobile.Mobileid = Mobileid;
                    mobile.MobileName = info.MobileName;
                    db.MobileTypes.Add(mobile);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Saved.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }
                }

                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteMobileType(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();
                MobileType mm = db.MobileTypes.SqlQuery("select * from MobileType where Mobileid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.MobileTypes.Remove(mm);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }

                }
                return msg;
            }
        }





        #endregion

        #region MobileModel 25 Feb 22
        public static Task<DataTable> GetMobileModel()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select * from MobileModel d
                            inner join MobileType s on s.Mobileid=d.Mobileid";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }
        //public static Task<DataTable> GetModel(string mid)
        //{
        //    return Task.Run(() =>
        //    {
        //        DataTable dt = new DataTable();
        //        string sql = @"select Typeid,Model,Price from MobileModel where Mobileid=@mid";
        //        SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
        //        adpt.SelectCommand.Parameters.AddWithValue("@mid", mid);
        //        adpt.Fill(dt);
        //        return dt;

        //    });
        //}
        public static Task<DataTable> GetDataTableAsync(string sSQL, params SqlParameter[] para)
        {
            return Task.Run(() =>
            {
                using (var adapt = new SqlDataAdapter(sSQL, conStr))
                {
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    if (para != null)
                        adapt.SelectCommand.Parameters.AddRange(para);
                    DataTable dt = new DataTable();

                    adapt.Fill(dt);
                    return dt;
                }
            });
        }

        public static Task<DataTable> GetAccDataTableAsync(string SQL, params SqlParameter[] par)
        {
            return Task.Run(() =>
            {
                using (var ad = new SqlDataAdapter(SQL, conStr))
                {
                    ad.SelectCommand.CommandType = CommandType.Text;
                    if (par!= null)
                        ad.SelectCommand.Parameters.AddRange(par);
                    DataTable adt = new DataTable();

                    ad.Fill(adt);
                    return adt;
                }
            });
        }
        public static async Task<ReturnMessage> SaveMobileModel(MobileModelInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                MobileModel mm = db.MobileModels.Where(x => x.Model == info.Model).FirstOrDefault();
                if (mm != null)
                {
                    msg.status = false;
                    msg.content = "Duplicate Mobile Model";
                    return msg;
                }
                else
                {
                    MobileModel mob = new MobileModel();
                    mob.Mobileid = info.Mobileid;
                    mob.Model = info.Model;
                    mob.Color = info.Color;
                    mob.Price = info.Price;
                    mob.Stock = info.Stock;
                    mob.Warranty = info.Warranty;
                    db.MobileModels.Add(mob);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Saved.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }
                }
                return msg;
            }
        }
        public static async Task<MobileModelInfo> GetModel(string id)
        {
            MobileModelInfo info = new MobileModelInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                MobileModel mm = await db.MobileModels.SqlQuery("select * from MobileModel where Typeid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    info.Typeid = mm.Typeid;
                    info.Mobileid = mm.Mobileid;
                    info.Model = mm.Model;
                    info.Color = mm.Color;
                    info.Warranty = mm.Warranty;
                    info.Price = mm.Price;
                    info.Stock = Convert.ToInt32(mm.Stock);
                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateModel(MobileModelInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                MobileModel mm = await db.MobileModels.SqlQuery("select * from MobileModel where Typeid=@id", new SqlParameter("@id", info.Typeid)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    mm.Mobileid = info.Mobileid;
                    mm.Model = info.Model;
                    mm.Price = info.Price;
                    mm.Stock = info.Stock;
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteModel(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();
                MobileModel mm = db.MobileModels.SqlQuery("select * from MobileModel where Typeid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.MobileModels.Remove(mm);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }

                }
                return msg;
            }
        }
        #endregion

        #region Employee 25 Feb 22
        public static Task<DataTable> GetEmployee()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select * from Employee";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }
        public static Task<DataTable> GetEmployeeName()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select Employeeid,EmployeeName from Employee";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }



        public static async Task<ReturnMessage> SaveEmployee(EmployeeInfo info)
        {
            ReturnMessage rm = new ReturnMessage();
            using (Mobile_ShopEntities ms = new Mobile_ShopEntities())
            {
                string eid = "";

                string sql = "SELECT Employeeid from Employee";
                var ginlist = ms.Database.SqlQuery<string>(
                   sql).ToList();
                if (ginlist.Count == 0)
                {
                    eid = "E01";

                }
                else
                {
                    string dstr = ginlist.Max(a => a);
                    int id = int.Parse(dstr.Substring(1));
                    id++;
                    eid = "E" + id.ToString("D2");
                }
                Employee em = new Employee();
                em.Employeeid = eid;
                em.EmployeeName = info.EmployeeName;
                em.Position = info.Position;
                em.Salary = info.Salary;
                em.Gender = info.Gender;
                em.Address = info.Address;
                em.PhNo = info.PhNo;
                em.ProfileImage = Encoding.ASCII.GetBytes("No Image");
                ms.Employees.Add(em);
                try
                {
                    await ms.SaveChangesAsync();
                    rm.status = true;
                    rm.content = "Successfuly Saved!";

                }
                catch (Exception ex)
                {
                    rm.status = false;
                    rm.content = ex.Message;
                }
                return rm;
            }
        }

        public static async Task<EmployeeInfo> GetEmployeeId(string id)
        {
            EmployeeInfo info = new EmployeeInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Employee em = await db.Employees.SqlQuery("select * from Employee where Employeeid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (em != null)
                {
                    info.Employeeid = em.Employeeid;
                    info.EmployeeName = em.EmployeeName;
                    info.Position = em.Position;
                    info.Address = em.Address;
                    info.Gender = em.Gender;
                    info.Salary = em.Salary;
                    info.PhNo = em.PhNo;
                    info.ProfileImage = em.ProfileImage;
                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateEmployee(EmployeeInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Employee em = await db.Employees.SqlQuery("select * from Employee where Employeeid=@id", new SqlParameter("@id", info.Employeeid)).SingleOrDefaultAsync();
                {
                    em.EmployeeName = info.EmployeeName;
                    em.Position = info.Position;
                    em.Salary = info.Salary;
                    em.Address = info.Address;
                    em.Gender = info.Gender;
                    em.PhNo = em.PhNo;

                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteEmployee(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();
                Employee mm = db.Employees.SqlQuery("select * from Employee where Employeeid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.Employees.Remove(mm);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }

                }
                return msg;
            }
        }


        #endregion

        #region SaleInfo 25 Feb 22
        public static Task<DataTable> GetSaleInfo()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select s.Saleid,c.CustomerName,c.Address,c.PhNo,t.MobileName,m.Model,s.Color,m.Price,s.Date,m.Warranty,s.Qty,e.EmployeeName,s.TotalAmount from SaleInfo s
                            inner join MobileModel m on s.Typeid=m.Typeid
                            inner join Customer c on s.Customerid=c.Customerid
                            inner join Employee e on s.Employeeid = e.Employeeid
                            inner join MobileType t on t.Mobileid=m.Mobileid";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }
        public static Task<DataTable> GetBox()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"SELECT SUM(s.Qty) as qty , SUM(m.Price) as price
                                FROM SaleInfo s,MobileModel m 
                                 WHERE s.Typeid=m.Typeid and s.Date >= DATEADD(day,-7, GETDATE())";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }

        public static Task<DataTable> SearchInfo(string sdate, string edate, string model, string empname)
        {
            return Task.Run(() =>
            {
                string cond = string.Empty;
                model = !string.IsNullOrEmpty(model) ? model : "";
                empname = !string.IsNullOrEmpty(empname) ? empname : "";

                if (empname != "" && empname != "")
                    cond += "and t.Mobileid=@model and e.Employeeid=@empname";

                if (model != "" && empname == "")
                    cond += " and t.Mobileid=@model";
                if (model == "" && empname != "")
                    cond += " and e.Employeeid=@empname";


                string sql = @"select s.Saleid,c.CustomerName,c.Address,c.PhNo,t.MobileName,m.Model,m.Price,s.Color,s.Date,m.Warranty,s.Qty,e.EmployeeName,s.TotalAmount from SaleInfo s
                            inner join MobileModel m on s.Typeid=m.Typeid
                            inner join Customer c on s.Customerid=c.Customerid
                            inner join Employee e on s.Employeeid = e.Employeeid
                            inner join MobileType t on t.Mobileid=m.Mobileid
							where s.Date between @sdate and @edate  " + cond + " Order By s.Date desc";
                using (var adapt = new SqlDataAdapter(sql, conStr))
                {
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    adapt.SelectCommand.Parameters.AddWithValue("@sdate", sdate);
                    adapt.SelectCommand.Parameters.AddWithValue("@edate", edate);

                    adapt.SelectCommand.Parameters.AddWithValue("@empname", empname);
                    adapt.SelectCommand.Parameters.AddWithValue("@model", model);

                    adapt.Fill(dt);
                    return dt;
                }
            });
        }
        public static Task<DataTable> SearchCum(string name, string ph)
        {
            return Task.Run(() =>
            {
                ph = !string.IsNullOrEmpty(ph) ? ph : "";
                name = !string.IsNullOrEmpty(name) ? name : "";
                string sql = @"select s.Saleid,c.CustomerName,c.Address,c.PhNo,t.MobileName,m.Model,m.Price,s.Date,s.Qty,e.EmployeeName from SaleInfo s
                            inner join MobileModel m on s.Typeid=m.Typeid
                            inner join Customer c on s.Customerid=c.Customerid
                            inner join Employee e on s.Employeeid = e.Employeeid
                            inner join MobileType t on t.Mobileid=m.Mobileid
							where  c.PhNo=@ph and c.CustomerName=@name";
                using (var adapt = new SqlDataAdapter(sql, conStr))
                {
                    adapt.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();

                    adapt.SelectCommand.Parameters.AddWithValue("@name", name);


                    adapt.SelectCommand.Parameters.AddWithValue("@ph", ph);



                    adapt.Fill(dt);
                    return dt;
                }
            });
        }

        public async static Task<ReturnMessage> SaveSaleInfo(MobileSaleInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities ms = new Mobile_ShopEntities())
            {
                string cid = "";

                try
                {

                    #region Add Customer tabl

                    string sql = "SELECT Customerid from Customer ";
                    var ginlist = ms.Database.SqlQuery<string>(
                       sql).ToList();
                    if (ginlist.Count == 0)
                    {
                        cid = "C01";

                    }
                    else
                    {
                        string dstr = ginlist.Max(a => a);
                        int id = int.Parse(dstr.Substring(1));
                        id++;
                        cid = "C" + id.ToString("D2");
                    }
                    Customer c = new Customer();
                    c.Customerid = cid;
                    c.CustomerName = info.CustomerName;
                    c.Address = info.Address;
                    c.PhNo = info.PhNo;
                    ms.Customers.Add(c);
                    await ms.SaveChangesAsync();
                    #endregion

                    string sid = "";
                    string sq = "SELECT Saleid from SaleInfo ";
                    var slist = ms.Database.SqlQuery<string>(
                       sq).ToList();
                    if (slist.Count == 0)
                    {
                        sid = "S01";

                    }
                    else
                    {
                        string dst = slist.Max(a => a);
                        int id = int.Parse(dst.Substring(1));
                        id++;
                        sid = "S" + id.ToString("D2");
                    }

                    string Custid = c.Customerid;
                    SaleInfo msi = new SaleInfo();

                    if (info.Ac != null && info.Typeid != 0)
                    {
                        msi.Saleid = sid;
                        msi.Customerid = Custid;
                        msi.Typeid = info.Typeid;
                        msi.Color = info.Colour;
                        msi.Qty = info.Qty;
                        
                        msi.TotalAmount = info.TotalAmount;
                        msi.Date = DateTime.Now;


                        msi.Employeeid = info.Employeeid;

                        ms.SaleInfoes.Add(msi);
                        await ms.SaveChangesAsync();
                        //msg.id = msi.Saleid;
                        string asid = msi.Saleid;
                        string cr = msi.Customerid;
                        List<AccSale> lst = new List<AccSale>();
                        foreach (var app in info.Ac)
                        {
                            AccSale _as = new AccSale();
                            _as.Saleid = asid;
                            _as.Customerid = cr;
                            _as.Aid = app.Color;
                            _as.Employeeid = info.Employeeid;
                            _as.Qty = app.Aqty;
                            _as.TotalAmount = app.Amount;
                            lst.Add(_as);
                        }
                        ms.AccSales.AddRange(lst);
                        await ms.SaveChangesAsync();
                        msg.id = asid;
                    }
                    if (info.Ac == null && info.Typeid != 0)
                    {

                        msi.Saleid = sid;

                        msi.Customerid = Custid;
                        msi.Typeid = info.Typeid;
                        msi.Color = info.Colour;
                        msi.Qty = info.Qty;
                        msi.TotalAmount = info.TotalAmount;
                        msi.Date = DateTime.Now;


                        msi.Employeeid = info.Employeeid;

                        ms.SaleInfoes.Add(msi);
                        await ms.SaveChangesAsync();
                        msg.id = msi.Saleid;
                    }
                    if (info.Typeid == 0 && info.Ac != null)
                    {

                        List<AccSale> lst = new List<AccSale>();
                        foreach (var app in info.Ac)
                        {
                            AccSale _as = new AccSale();
                            _as.Saleid = sid;
                            _as.Customerid = Custid;
                            _as.Aid = app.Color;
                            _as.Employeeid = info.Employeeid;
                            _as.Qty = app.Aqty;
                            _as.TotalAmount = app.Amount;
                            lst.Add(_as);
                        }
                        ms.AccSales.AddRange(lst);
                        await ms.SaveChangesAsync();
                        msg.id = sid;

                    }

                    msg.status = true;
                    msg.content = "Successfully Saved.";
                }
                catch (DbEntityValidationException ex)
                {
                    #region
                    foreach (DbEntityValidationResult item in ex.EntityValidationErrors)
                    {
                        // Get entry

                        DbEntityEntry entry = item.Entry;
                        string entityTypeName = entry.Entity.GetType().Name;

                        // Display or log error messages

                        foreach (DbValidationError subItem in item.ValidationErrors)
                        {
                            string str = string.Format("Error '{0}' occurred in {1} at {2}",
                                     subItem.ErrorMessage, entityTypeName, subItem.PropertyName);
                            msg.content += str;
                        }
                    }
                    msg.status = false;
                    #endregion
                }
                catch (DbUpdateException e)
                {
                    foreach (var error in e.Entries)
                    {
                        msg.content += error.Entity.GetType().Name;
                    }
                    msg.status = false;
                }
                catch (Exception e)
                {
                    msg.content += e.Message;
                    msg.status = false;
                }
            }
            return msg;
        }

        public static async Task<MobileSaleInfo> GetSaleId(string id)
        {
            MobileSaleInfo info = new MobileSaleInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                SaleInfo em = await db.SaleInfoes.SqlQuery("select * from SaleInfo s  inner join Customer c on s.Customerid=c.Customerid  where s.Saleid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (em != null)
                {

                    info.Saleid = em.Saleid;
                    info.Customerid = em.Customerid;
                    info.Typeid = Convert.ToInt32(em.Typeid);
                    info.Date = em.Date;
                    info.Qty = em.Qty;
                    info.Employeeid = em.Employeeid;
                    info.TotalAmount = em.TotalAmount;

                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateSaleInfo(MobileSaleInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Customer cm = await db.Customers.SqlQuery("select * from Customer c  inner join SaleInfo s on s.Customerid=c.Customerid  where s.Saleid=@id", new SqlParameter("@id", info.Saleid)).SingleOrDefaultAsync();
                {

                    cm.CustomerName = info.CustomerName;
                    cm.Address = info.Address;
                    cm.PhNo = info.PhNo;
                    await db.SaveChangesAsync();
                }
                SaleInfo em = await db.SaleInfoes.SqlQuery("select * from SaleInfo s  inner join Customer c on s.Customerid=c.Customerid  where s.Saleid=@id", new SqlParameter("@id", info.Saleid)).SingleOrDefaultAsync();
                {


                    em.Typeid = info.Typeid;
                    em.Qty = info.Qty;
                    em.Employeeid = info.Employeeid;
                    em.TotalAmount = info.TotalAmount;
                    await db.SaveChangesAsync();

                    try
                    {

                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteSaleInfo(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();

                Customer cu = db.Customers.SqlQuery("select c.* from Customer c inner join SaleInfo s on c.Customerid=s.Customerid where s.Saleid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (cu != null)
                {
                    db.Customers.Remove(cu);
                    await db.SaveChangesAsync();
                }

                SaleInfo mm = db.SaleInfoes.SqlQuery("select * from SaleInfo where Saleid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.SaleInfoes.Remove(mm);
                    await db.SaveChangesAsync();
                }

                try
                {

                    msg.status = true;
                    msg.content = "Successfully deleted.";
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.content = ex.Message;
                }


                return msg;
            }
        }

        #endregion

        #region Order
        public static  Task<DataTable> ViewOrder()
        {
            return Task.Run(() =>
            {
                string sql = "select * from PhoneOrder";
                using (var adpt = new SqlDataAdapter(sql, conStr))
                {
                    adpt.SelectCommand.CommandType = CommandType.Text;
                    DataTable dt = new DataTable();
                    adpt.Fill(dt);
                    return dt;
                }
            });



        }
        public static async Task<ReturnMessage> SaveOrder(OrderInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using(Mobile_ShopEntities db=new Mobile_ShopEntities())
            {
                string Oid = "";
                string sql = "select Orderid from PhoneOrder";
                var list = db.Database.SqlQuery<string>(sql).ToList();
                if (list.Count == 0)
                {
                    Oid = "OR01";
                }
                else
                {
                    string data = list.Max(a => a);
                    int id = int.Parse(data.Substring(2));
                    id++;
                    Oid = "OR" + id.ToString("D2");
                }
              

                PhoneOrder ord = new PhoneOrder();
                ord.Orderid = Oid;
                ord.Customername = info.Customername;
                ord.Oaddress = info.Oaddress;
                ord.Oph = info.Oph;
                ord.PhoneType = info.PhoneType;
                ord.PhoneModel = info.PhoneModel;
                ord.PhoneColor = info.PhoneColor;
                ord.OrderDate = info.OrderDate;
                ord.NowDate = DateTime.Now;
                ord.Delivery = info.Delivery;
                ord.Employeeid = info.Employeeid;
                 db.PhoneOrders.Add(ord);
                try
                {
                    await db.SaveChangesAsync();
                    msg.status = true;
                    msg.content = "Order saved sucessfully.";
                }
                catch(Exception ex)
                {
                    msg.status = false;
                    msg.content = ex.Message;
                }

            }
            return msg;
        }
      
        public static async Task<OrderInfo> GetOrderId(string id)
        {
            OrderInfo info = new OrderInfo();
            using(
                
                Mobile_ShopEntities db=new Mobile_ShopEntities())
            {
                 PhoneOrder ord = await db.PhoneOrders.SqlQuery("select * from PhoneOrder where Orderid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (ord != null)
                {
                    info.Orderid = ord.Orderid;
                    info.Customername = ord.Customername;
                    info.Oaddress = ord.Oaddress;
                    info.Oph = ord.Oph;
                    info.PhoneType = ord.PhoneType;
                    info.PhoneModel = ord.PhoneModel;
                    info.PhoneColor = ord.PhoneColor;
                    info.OrderDate = ord.OrderDate;
                    info.NowDate = ord.NowDate;
                    info.Delivery = ord.Delivery;
                    info.Employeeid = ord.Employeeid;
                }
            } 
            return info;
        }

        public static async Task<ReturnMessage> UpdateOrder(OrderInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                PhoneOrder ord = await db.PhoneOrders.SqlQuery("select * from PhoneOrder where Orderid=@id", new SqlParameter("@id", info.Orderid)).SingleOrDefaultAsync();
                if (ord != null)
                {
                    ord.Customername = info.Customername;
                    ord.Oaddress = info.Oaddress;
                    ord.Oph = info.Oph;
                    ord.PhoneType = info.PhoneType;
                    ord.PhoneModel = info.PhoneModel;
                    ord.PhoneColor = info.PhoneColor;
                    ord.OrderDate = info.OrderDate;
               
                    ord.Delivery = info.Delivery;
                    ord.Employeeid = info.Employeeid;
                    db.PhoneOrders.Add(ord);
                    try
                    {
                        msg.status = true;
                        msg.content = "Updated Successfully.";
                    }catch(Exception e)
                    {
                        msg.status = false;
                        msg.content = e.Message;

                    }

                }
            }
            return msg;
        }
        public static async Task<ReturnMessage> DeleteOrder(string id)
        {
            ReturnMessage msg = new ReturnMessage();
            using(Mobile_ShopEntities db=new Mobile_ShopEntities())
            {
                PhoneOrder ord = await db.PhoneOrders.SqlQuery("select * from PhoneOrder where Orderid=@id", new SqlParameter("@id",id)).SingleOrDefaultAsync();
          if(ord!= null)
                {
                    db.PhoneOrders.Remove(ord);
                    await db.SaveChangesAsync();
                }
                try
                {
                    msg.status = true;
                    msg.content = "Deleted Successfully.";
                }catch(Exception ex)
                {
                    msg.status = false;
                    msg.content = ex.Message;
                }

            }
            return msg;
        }
        #endregion

        #region Accessries 31 March 2022
        public static Task<DataTable> GetAccessories()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select * from Extra";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }

        public static Task<DataTable> GetBrandName()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select distinct BrandName from Extra";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }

        //public static Task<DataTable> GetExtraColor(string id)
        //{
        //    return Task.Run(() =>
        //    {
        //        DataTable dt = new DataTable();
        //        string sql = @"select Aid, Model, Color from Extra where Model = @id";

        //        SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
        //        adpt.SelectCommand.Parameters.AddWithValue("@id",id);
        //        adpt.Fill(dt);
        //        return dt;

        //    });
        //}

        public static string GetColor(string id)
        {
            string Color = string.Empty;
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Color = db.Database.SqlQuery<string>("select Color from Extra where Aid=@id", new SqlParameter("@id", id)).SingleOrDefault();
            }
            return Color;
        }

        //public static string GetBrandName(string id)
        //{
        //    string Brand = string.Empty;
        //    using (Mobile_ShopEntities db = new Mobile_ShopEntities())
        //    {
        //        Brand = db.Database.SqlQuery<string>("select BrandName from Extra where Aid=@id", new SqlParameter("@id", id)).SingleOrDefault();
        //    }
        //    return Brand;
        //}
        public static async Task<ReturnMessage> SaveAccessories(ExtraInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities ms = new Mobile_ShopEntities())
            {

                string aid = "";



                string sql = "SELECT Aid from Extra";
                var ginlist = ms.Database.SqlQuery<string>(
                   sql).ToList();
                if (ginlist.Count == 0)
                {
                    aid = "AC01";

                }
                else
                {
                    string dstr = ginlist.Max(a => a);
                    int id = int.Parse(dstr.Substring(2));
                    id++;
                    aid = "AC" + id.ToString("D2");
                }

                Extra mm = await ms.Extras.SqlQuery("select * from Extra where Model=@id and Color=@color", new SqlParameter("@id", info.Model), new SqlParameter("@color", info.Color)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    msg.status = false;
                    msg.content = "Duplicate  Model and Color";
                    return msg;
                }
                else
                {
                    Extra mob = new Extra();
                    mob.Aid = aid;
                    mob.BrandName = info.BrandName;
                    mob.Type = info.Type;
                    mob.Model = info.Model;
                    mob.Color = info.Color;
                    mob.Price = info.Price;
                    mob.Stock = info.Stock;
                    mob.Warranty = info.Warranty;
                    ms.Extras.Add(mob);
                    try
                    {
                        await ms.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Saved.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }
                }
                return msg;
            }
        }

        public static async Task<ExtraInfo> GetExtra(string id)
        {
            ExtraInfo info = new ExtraInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Extra mm = await db.Extras.SqlQuery("select * from Extra where Aid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (mm != null)
                {

                    info.Aid = mm.Aid;
                    info.BrandName = mm.BrandName;
                    info.Type = mm.Type;
                    info.Model = mm.Model;
                    info.Color = mm.Color;
                    info.Price = mm.Price;
                    info.Stock = mm.Stock;
                    info.Warranty = mm.Warranty;

                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateExtra(ExtraInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Extra mm = await db.Extras.SqlQuery("select * from Extra where Aid=@id", new SqlParameter("@id", info.Aid)).SingleOrDefaultAsync();
                if (mm != null)
                {

                    mm.Aid = info.Aid;
                    mm.BrandName = info.BrandName;
                    mm.Type = info.Type;
                    mm.Model = info.Model;
                    mm.Color = info.Color;
                    mm.Price = info.Price;
                    mm.Stock = info.Stock;
                    mm.Warranty = info.Warranty;

                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        public static async Task<ReturnMessage> DeleteExtra(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();
                Extra mm = await db.Extras.SqlQuery("select * from Extra where Aid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (mm != null)
                {
                    db.Extras.Remove(mm);
                    try
                    {
                        await db.SaveChangesAsync();
                        msg.status = true;
                        msg.content = "Successfully deleted.";
                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;
                    }

                }
                return msg;
            }
        }



        #endregion

        #region AccSale
        public static Task<DataTable> GetAccSale()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select a.Bid,a.Saleid,c.CustomerName,c.Address,c.PhNo,r.BrandName,r.Type,r.Model,r.Price,r.Color,r.Warranty,a.Qty,e.EmployeeName,a.TotalAmount from AccSale a
                            inner join Extra r on a.Aid=r.Aid
                            inner join Customer c on a.Customerid=c.Customerid
                            inner join Employee e on a.Employeeid = e.Employeeid
                            inner join SaleInfo s on a.Saleid=s.Saleid";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }

        public static async Task<ReturnMessage> DeleteAccSale(string id)
        {

            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                ReturnMessage msg = new ReturnMessage();

                Customer cu = db.Customers.SqlQuery("select c.* from Customer c inner join AccSale b on c.Customerid=b.Customerid where b.Bid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (cu != null)
                {
                    db.Customers.Remove(cu);
                    await db.SaveChangesAsync();
                }

                AccSale mm = db.AccSales.SqlQuery("select * from AccSale where Bid=@id", new SqlParameter("@id", id)).SingleOrDefault();
                if (mm != null)
                {
                    db.AccSales.Remove(mm);
                    await db.SaveChangesAsync();
                }

                try
                {

                    msg.status = true;
                    msg.content = "Successfully deleted.";
                }
                catch (Exception ex)
                {
                    msg.status = false;
                    msg.content = ex.Message;
                }


                return msg;
            }
        }

        public static async Task<AccSaleInfo> GetAccSaleId(string id)
        {
            AccSaleInfo info = new AccSaleInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                AccSale ass = await db.AccSales.SqlQuery("select * from AccSale s  inner join Customer c on s.Customerid=c.Customerid  where s.Bid=@id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (ass != null)



                {
                    info.Bid = ass.Bid;
                    info.Saleid = ass.Saleid;
                    info.Customerid = ass.Customerid;
                    info.Aid = ass.Aid;

                    info.Qty = ass.Qty;
                    info.Employeeid = ass.Employeeid;
                    info.TotalAmount = ass.TotalAmount;

                }
                return info;
            }


        }

        public static async Task<ReturnMessage> UpdateAccSale(AccSaleInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Customer cm = await db.Customers.SqlQuery("select * from Customer c  inner join AccSale s on s.Customerid=c.Customerid  where s.Bid=@id", new SqlParameter("@id", info.Bid)).SingleOrDefaultAsync();
                {

                    cm.CustomerName = info.CustomerName;
                    cm.Address = info.Address;
                    cm.PhNo = info.PhNo;
                    await db.SaveChangesAsync();
                }
                AccSale ass = await db.AccSales.SqlQuery("select * from AccSale s  inner join Customer c on s.Customerid=c.Customerid  where s.Bid=@id", new SqlParameter("@id", info.Bid)).SingleOrDefaultAsync();
                {

                    ass.Aid = info.Aid;
                    ass.Qty = info.Qty;
                    ass.Employeeid = info.Employeeid;
                    ass.TotalAmount = info.TotalAmount;
                    await db.SaveChangesAsync();

                    try
                    {

                        msg.status = true;
                        msg.content = "Successfully Updated.";

                    }
                    catch (Exception ex)
                    {
                        msg.status = false;
                        msg.content = ex.Message;

                    }

                }
                return msg;
            }
        }

        #endregion
        #region Repair 2022 April 18

        public static Task<DataTable> GetRepair()
        {
            return Task.Run(() =>
            {
                DataTable dt = new DataTable();
                string sql = @"select s.Rno,c.CustomerName,c.Address,c.PhNo,s.PhoneType,s.Model,s.Color,s.IsNo,s.Description,s.RepairPayment,s.Projection,s.Date,s.TotalAmount from Repair s
                                inner join Customer c on s.Customerid=c.Customerid";
                SqlDataAdapter adpt = new SqlDataAdapter(sql, conStr);
                adpt.Fill(dt);
                return dt;

            });
        }
        public static async Task<ReturnMessage> SaveRepairInfo(RepairInfo info)
        {
            ReturnMessage msg = new ReturnMessage();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                #region Add Customer tabl
                string cid = "";
                string sql = "SELECT Customerid from Customer ";
                var ginlist = db.Database.SqlQuery<string>(
                   sql).ToList();
                if (ginlist.Count == 0)
                {
                    cid = "C01";

                }
                else
                {
                    string dstr = ginlist.Max(a => a);
                    int id = int.Parse(dstr.Substring(1));
                    id++;
                    cid = "C" + id.ToString("D2");
                }
                Customer c = new Customer();
                c.Customerid = cid;
                c.CustomerName = info.CustomerName;
                c.Address = info.Address;
                c.PhNo = info.PhNo;
                db.Customers.Add(c);
                await db.SaveChangesAsync();
                #endregion
                string rcid = c.Customerid;
                string rid = "";
                string s = "select Rno from Repair";
                var slist = db.Database.SqlQuery<string>(s).ToList();
                if (slist.Count == 0)
                {
                    rid = "Re01";
                }
                else
                {
                    string mlist = slist.Max(a => a);
                    int dlist = int.Parse(mlist.Substring(2));
                    dlist++;
                    rid = "Re" + dlist.ToString("D2");
                }

                Repair re = new Repair();
                re.Rno = rid;
                re.Customerid = rcid;
                re.PhoneType = info.PhoneType;
                re.Model = info.Model;
                re.IsNo = info.IsNo;
                re.Color = info.Color;
                re.Date = DateTime.Now;
                re.Description = info.Description;
                re.RepairPayment = info.RepairPayment;
                re.Projection = info.Projection;
                re.TotalAmount = info.TotalAmount;
                db.Repairs.Add(re);
                try
                {
                    await db.SaveChangesAsync();
                    msg.id = re.Rno;
                    msg.content = "Saved Successfully.";
                    msg.status = true;
                }
                catch
                {
                    msg.status = false;
                    msg.content = "Saved Fail.";
                }

            }
            return msg;
        }
        public static async Task<RepairInfo> GetRepairId(string id)
        {
            RepairInfo info = new RepairInfo();
            using (Mobile_ShopEntities db = new Mobile_ShopEntities())
            {
                Repair re = await db.Repairs.SqlQuery("select * from Repair s  inner join Customer c on s.Customerid = c.Customerid where s.Rno = @id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                if (re != null)
                {
                    Customer cu = await db.Customers.SqlQuery("select * from Customer c  inner join Repair s on c.Customerid=s.Customerid  where s.Rno = @id", new SqlParameter("@id", id)).SingleOrDefaultAsync();
                    if (cu != null)
                    {
                        info.CustomerName = cu.CustomerName;
                        info.Address = cu.Address;
                        info.PhNo = cu.PhNo;
                    }
                    info.Rno = re.Rno;
                    info.Customerid = re.Customerid;
                    info.PhoneType = re.PhoneType;
                    info.Model = re.Model;
                    info.Color = re.Color;
                    info.IsNo = re.IsNo;
                    info.Description = re.Description;
                    info.RepairPayment = re.RepairPayment;
                    info.Projection = re.Projection;
                    info.TotalAmount = re.TotalAmount;
                }
            }
            return info;
        }
        #endregion

    }
}