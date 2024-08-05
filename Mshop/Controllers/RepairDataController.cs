using Mshop.DB;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Mshop.Controllers
{
    public class RepairDataController : ApiController
    {
        #region Repair

        [HttpGet]
        [ActionName("GetRepair")]
        public async Task<IHttpActionResult> GetRepair()
        {
            DataTable dt = await QueryDAL.GetRepair();
            return Ok(dt);
        }
        #endregion
    }
}
