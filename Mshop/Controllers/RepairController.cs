using Mshop.DB;
using Mshop.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Mshop.Controllers
{[Authorize]
    public class RepairController : Controller
    {
        // GET: Repair
        public ActionResult SaveRepair()
        {
            return View();
        }
        public async Task<ActionResult> GetRepairId(string id)
        {
            RepairInfo info = await QueryDAL.GetRepairId(id);
            return View(info);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<JsonResult> SaveRepair(RepairInfo info)
        {

            ReturnMessage msg = await QueryDAL.SaveRepairInfo(info);
            return Json(msg);
        }
    }
}