using Mshop.DB;
using Mshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mshop.Controllers
{
    public class MasterController : Controller
    {
        [Authorize]

        // GET: Master
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult MainPage()
        {
            return View();
        }
        #region Customer
        // GET: Master
        public ActionResult Customer()
        {
            return View();
        }

        public ActionResult NewCustomer()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewCustomer(CustomerInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveCustomerInfo(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateCustomer(string id)
        {
            CustomerInfo info = await QueryDAL.GetCustomerId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateCustomer(CustomerInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateCustomer(info);
            return Json(msg);
        }
        #endregion

        #region MobileType
        public ActionResult MobileType()
        {
            return View();
        }

        public ActionResult NewMobileType()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewMobileType(MobileTypeInfo info)
        {
            ReturnMessage msg = await QueryDAL.SaveMobileType(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateMobileType(string id)
        {
            MobileTypeInfo info = await QueryDAL.GetMobile(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateMobileType(MobileTypeInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateMobile(info);
            return Json(msg);
        }
        #endregion

        #region MobileModel
        public ActionResult MobileModel()
        {
            return View();
        }
        public ActionResult NewMobileModel()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewMobileModel(MobileModelInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveMobileModel(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateModel(string id)
        {
            MobileModelInfo info = await QueryDAL.GetModel(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateModel(MobileModelInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateModel(info);
            return Json(msg);
        }
        #endregion

        #region Employee
        public ActionResult Employee()
        {
            return View();
        }

        public ActionResult NewEmployee()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewEmployee(EmployeeInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveEmployee(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateEmployee(string id)
        {
            EmployeeInfo info = await QueryDAL.GetEmployeeId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateEmployee(EmployeeInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateEmployee(info);
            return Json(msg);
        }
        #endregion

        #region SaleInfo
        public ActionResult SaleInfo()
        {
            return View();
        }

        public ActionResult NewSaleInfo()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewSaleInfo(MobileSaleInfo info)
        {
            if (TempData["Accessories"] != null)
            {
                List<ModalTempInfo> invlst = TempData["Accessories"] as List<ModalTempInfo>;
                info.Ac = invlst;
            }

            ReturnMessage msg = await QueryDAL.SaveSaleInfo(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateSaleInfo(string id)
        {
            MobileSaleInfo info = await QueryDAL.GetSaleId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateSaleInfo(MobileSaleInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateSaleInfo(info);
            return Json(msg);
        }
        #endregion

        #region Accessories
        public ActionResult SaleExtra()
        {
            return View();
        }

        public ActionResult NewSaleExtra()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewSaleExtra(ExtraInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveAccessories(info);
            return Json(msg);
        }
        public async Task<ActionResult> UpdateExtra(string id)
        {
            ExtraInfo info = await QueryDAL.GetExtra(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateExtra(ExtraInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateExtra(info);
            return Json(msg);
        }


        [HttpPost]
        public JsonResult SaveModalAcc(ModalTempInfo att)
        {
            ReturnMessage msg = new ReturnMessage();
            try
            {

                using (Mobile_ShopEntities db = new Mobile_ShopEntities())
                {


                    List<ModalTempInfo> Investlst = new List<ModalTempInfo>();
                    List<ModalTempInfo> checkInvestList = TempData["Accessories"] as List<ModalTempInfo>;
                    if (checkInvestList != null && checkInvestList.Count > 0)
                    {
                        var existInvlst = checkInvestList.Where(x => x.Color == att.Color && x.Model == att.Model).ToList();
                        if (existInvlst.Count == 0)
                        {
                            int maxId = (from extInv in checkInvestList select Convert.ToInt32(extInv.ACid)).Max();
                            att.ACid = maxId++;
                            Investlst = TempData["Accessories"] as List<ModalTempInfo>;
                            //att.TypeName = QueryDAL.GetColor(att.Type);
                            //att.Brand = QueryDAL.GetBrandName(att.BrandName);
                            att.Brand = QueryDAL.GetColor(att.Color);
                            Investlst.Add(att);
                            TempData["Accessories"] = Investlst;
                            msg.content = "successfully added";
                            msg.status = true;
                        }
                        else
                        {
                            msg.content = "Dat is already exist";
                            msg.status = false;
                        }
                    }
                    else
                    {
                        att.ACid = 1;
                        //att.TypeName = QueryDAL.GetTypeName(att.Type);
                        //att.Brand = QueryDAL.GetBrandName(att.BrandName);
                        att.Brand = QueryDAL.GetColor(att.Color);
                        Investlst.Add(att);
                        TempData["Accessories"] = Investlst;
                        msg.content = "successfully added";
                        msg.status = true;
                    }
                }
            }
            catch (Exception ex)
            {
                msg.content = ex.ToString();
                msg.status = false;
            }
            TempData.Keep();
            return Json(msg, JsonRequestBehavior.AllowGet);

        }


        [HttpGet]
        public JsonResult GetModalAcc()
        {

            List<ModalTempInfo> lstinv = new List<ModalTempInfo>();
            if (TempData["Accessories"] != null)
            {
                lstinv = TempData["Accessories"] as List<ModalTempInfo>;
                TempData["Accessories"] = lstinv;

            }
            TempData.Keep();

            return Json(lstinv, JsonRequestBehavior.AllowGet);
        }

        public JsonResult RemoveModalInfo(int id)
        {
            try
            {
                List<ModalTempInfo> lstinv = new List<ModalTempInfo>();
                if (TempData["Accessories"] != null)
                {
                    lstinv = TempData["Accessories"] as List<ModalTempInfo>;
                    lstinv.Remove(lstinv.FirstOrDefault(t => t.ACid == (id)));
                    TempData["Accessories"] = lstinv;
                }
                TempData.Keep();
                return Json("success", JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }

        }

        public async Task<ActionResult> UpdateAccSale(string id)


        {
            AccSaleInfo info = await QueryDAL.GetAccSaleId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateAccSale(AccSaleInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateAccSale(info);
            return Json(msg);
        }
        #endregion

        #region Order
        public ActionResult NewOrder()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> NewOrder(OrderInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveOrder(info);
            return Json(msg);
        }

        public async Task<ActionResult> UpdateOrder(string id)
        {
            OrderInfo info = await QueryDAL.GetOrderId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> UpdateOrder(OrderInfo info)
        {
            ReturnMessage msg = await QueryDAL.UpdateOrder(info);
            return Json(msg);
        }
        #endregion
    }
}