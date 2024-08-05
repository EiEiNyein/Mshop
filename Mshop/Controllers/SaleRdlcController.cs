using Microsoft.Reporting.WebForms;
using Mshop.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mshop.Controllers
{
    public class SaleRdlcController : Controller
    {
        // GET: SaleRdlc
        public byte[] ExportPDF(Microsoft.Reporting.WebForms.LocalReport rpt)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rpt.Render("PDF", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            return bytes;
        }
        public byte[] ExportExcel(Microsoft.Reporting.WebForms.LocalReport rpt)
        {
            Microsoft.Reporting.WebForms.Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            byte[] bytes = rpt.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            return bytes;
        }

        public ActionResult SaleView(string id)
        {

            ViewBag.id = id;
            return View();
        }
        public async Task<ActionResult> SaleInfoPos(string id)
        {

            string Empname = string.Empty;
            string Customername = string.Empty;
            string Address = string.Empty;
            string Phno = string.Empty;
            string Aemp = string.Empty;
            string Acustomer = string.Empty;
            string Aaddress = string.Empty;
            string Aphno = string.Empty;
            string Sale = string.Empty;




            byte[] byteData;
            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();
            ReportViewer reportViewer = new ReportViewer();
            string path = Server.MapPath("~/Report/");

            string sql = @"select s.Saleid,c.CustomerName,c.Address,c.PhNo,t.MobileName,m.Model,m.Price,s.Color,s.Qty,e.EmployeeName,s.TotalAmount from SaleInfo s
                            inner join MobileModel m on s.Typeid=m.Typeid
                            inner join Customer c on s.Customerid=c.Customerid
                            inner join Employee e on s.Employeeid = e.Employeeid
                           inner join MobileType t on t.Mobileid=m.Mobileid 
						   where s.Saleid=@id";
            string sq= @"select  distinct ac.Saleid as Sale,c.CustomerName as Acustomer,c.Address as Aaddress,c.PhNo as Aphno,e.EmployeeName as Aemp, a.BrandName,a.Type,a.Model as Amodel,a.Color as Acolor,a.Price as Aprice,ac.Qty as Aqty,ac.TotalAmount as Atotal From AccSale ac
                       inner join Extra a on ac.Aid = a.Aid
					   inner join Customer c on ac.Customerid=c.Customerid
					   inner join Employee e on ac.Employeeid=e.Employeeid
					    where ac.Saleid=@aid";
            SqlParameter[] para = new SqlParameter[1];
            SqlParameter[] par = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            par[0] = new SqlParameter("@aid", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);
            DataTable adt = await QueryDAL.GetAccDataTableAsync(sq, par);

            Report.ReportDataSet.MobileShop.SaleInfoDataTable tblsi = new Report.ReportDataSet.MobileShop.SaleInfoDataTable();
            Report.ReportDataSet.MobileShop.AccInfoDataTable atbl = new Report.ReportDataSet.MobileShop.AccInfoDataTable();
           
                for (
                int i = 0; i < dt.Rows.Count; i++)
                {

                    Report.ReportDataSet.MobileShop.SaleInfoRow row = tblsi.NewSaleInfoRow();
                    row.Saleid = dt.Rows[i]["Saleid"].ToString();

                    row.MobileName = dt.Rows[i]["MobileName"].ToString();
                    row.Model = dt.Rows[i]["Model"].ToString();
                    row.Color = dt.Rows[i]["Color"].ToString();
                    row.Price = Convert.ToInt32(dt.Rows[i]["Price"]);

                    row.Qty = Convert.ToInt32(dt.Rows[i]["Qty"]);

                    row.Total = Convert.ToInt32(dt.Rows[i]["TotalAmount"]);


                    tblsi.AddSaleInfoRow(row);
                    Empname = dt.Rows[i]["EmployeeName"].ToString();
                    Customername = dt.Rows[i]["CustomerName"].ToString();
                    Address = dt.Rows[i]["Address"].ToString();
                    Phno = dt.Rows[i]["PhNo"].ToString();


                }

            if (adt.Rows.Count > 0)
            {
                for (int i = 0; i < adt.Rows.Count; i++)
                {
                    Report.ReportDataSet.MobileShop.AccInfoRow ro = atbl.NewAccInfoRow();

                    ro.BrandName = adt.Rows[i]["BrandName"].ToString();
                    ro.Type = adt.Rows[i]["Type"].ToString();
                    ro.Amodel = adt.Rows[i]["Amodel"].ToString();
                    ro.Acolor = adt.Rows[i]["Acolor"].ToString();
                    ro.Aprice = Convert.ToInt32(adt.Rows[i]["Aprice"]);
                    ro.Aqty = Convert.ToInt32(adt.Rows[i]["Aqty"]);
                    ro.Atotal = Convert.ToInt32(adt.Rows[i]["Atotal"]);
                    atbl.AddAccInfoRow(ro);
                    Aemp = adt.Rows[i]["Aemp"].ToString();
                    Acustomer = adt.Rows[i]["Acustomer"].ToString();
                    Aaddress = adt.Rows[i]["Aaddress"].ToString();
                    Aphno = adt.Rows[i]["Aphno"].ToString();
                }
                Sale = adt.Rows[0]["Sale"].ToString();
            }
           
            if ((dt.Rows.Count > 0 && adt.Rows.Count > 0)|| (dt.Rows.Count > 0 && adt.Rows.Count == 0))
            {
                ReportParameter[] param = new ReportParameter[4];
                param[0] = new ReportParameter("Empname", Empname);
                param[1] = new ReportParameter("Customername", Customername);
                param[2] = new ReportParameter("Address", Address);
                param[3] = new ReportParameter("Phno", Phno);
              
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SaleInfo", (DataTable)tblsi));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AccInfo", (DataTable)atbl));

                reportViewer.LocalReport.ReportPath = path + "SaleInfo.rdlc";
                reportViewer.LocalReport.ReportEmbeddedResource = path + "SaleInfo.rdlc";
                reportViewer.LocalReport.SetParameters(param);
            }
            if(adt.Rows.Count>0 && dt.Rows.Count == 0)
            {
                ReportParameter[] param = new ReportParameter[5];
                
                param[0] = new ReportParameter("Aemp", Aemp);
                param[1] = new ReportParameter("Acustomer", Acustomer);
                param[2] = new ReportParameter("Aaddress", Aaddress);
                param[3] = new ReportParameter("Aphno", Aphno);
                param[4] = new ReportParameter("Sale", Sale);
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("SaleInfo", (DataTable)tblsi));
                reportViewer.LocalReport.DataSources.Add(new ReportDataSource("AccInfo", (DataTable)atbl));

                reportViewer.LocalReport.ReportPath = path + "AccInfo.rdlc";
                reportViewer.LocalReport.ReportEmbeddedResource = path + "AccInfo.rdlc";
                reportViewer.LocalReport.SetParameters(param);
            }
            
          
            rpt = reportViewer.LocalReport;
            byteData = ExportPDF(rpt);
            rpt.Dispose();
            return File(byteData, "application/pdf");
        }

        public ActionResult RepairView(string id)
        {

            ViewBag.id = id;
            return View();
        }
        public async Task<ActionResult> RepairInfoPos(string id)
        {

            string Color = string.Empty;
            string CustomerName = string.Empty;
            string Address = string.Empty;
            string PhNo = string.Empty;
            string PhoneType = string.Empty;
            string Model = string.Empty;
            byte[] byteData;
            Microsoft.Reporting.WebForms.LocalReport rpt = new Microsoft.Reporting.WebForms.LocalReport();
            ReportViewer reportViewer = new Microsoft.Reporting.WebForms.ReportViewer();
            string path = Server.MapPath("~/Report/");

            string sql = @"select s.Rno,c.CustomerName,c.Address,c.PhNo,s.PhoneType,s.Model,s.Color,s.Description,s.RepairPayment,s.Projection,s.Date,s.TotalAmount from Repair s
                                inner join Customer c on s.Customerid=c.Customerid where s.Rno=@id";
            SqlParameter[] para = new SqlParameter[1];
            para[0] = new SqlParameter("@id", id);
            DataTable dt = await QueryDAL.GetDataTableAsync(sql, para);

            Report.ReportDataSet.RepairDs.RepairmgDataTable tblsi = new Report.ReportDataSet.RepairDs.RepairmgDataTable();

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                Report.ReportDataSet.RepairDs.RepairmgRow row = tblsi.NewRepairmgRow();
                row.Rno = dt.Rows[i]["Rno"].ToString();
                row.Description = dt.Rows[i]["Description"].ToString();
                row.RepairPayment = Convert.ToInt32(dt.Rows[i]["RepairPayment"]);
                row.Projection = Convert.ToInt32(dt.Rows[i]["Projection"]);
                row.TotalAmount = Convert.ToInt32(dt.Rows[i]["TotalAmount"]);
                tblsi.AddRepairmgRow(row);
                CustomerName = dt.Rows[i]["CustomerName"].ToString();
                Address = dt.Rows[i]["Address"].ToString();
                PhNo = dt.Rows[i]["PhNo"].ToString();
                PhoneType = dt.Rows[i]["PhoneType"].ToString();
                Model = dt.Rows[i]["Model"].ToString();
                Color = dt.Rows[i]["Color"].ToString();
            }
            ReportParameter[] param = new ReportParameter[6];
            param[0] = new ReportParameter("CustomerName", CustomerName);
            param[1] = new ReportParameter("Address", Address);
            param[2] = new ReportParameter("PhNo", PhNo);
            param[3] = new ReportParameter("PhoneType", PhoneType);
            param[4] = new ReportParameter("Model", Model);
            param[5] = new ReportParameter("Color", Color);
            reportViewer.LocalReport.DataSources.Add(new ReportDataSource("RepairInfo", (DataTable)tblsi));

            reportViewer.LocalReport.ReportPath = path + "RepairInfo.rdlc";
            reportViewer.LocalReport.ReportEmbeddedResource = path + "RepairInfo.rdlc";
            reportViewer.LocalReport.SetParameters(param);

            rpt = reportViewer.LocalReport;
            byteData = ExportPDF(rpt);
            rpt.Dispose();
            return File(byteData, "application/pdf");
        }
    }
}