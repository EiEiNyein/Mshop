using Mshop.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace Mshop.Controllers
{
    public class ManageDataController : ApiController
    {
        [HttpGet]
        [ActionName("GetRole")]
        [Route("api/ManageData/GetRole")] //used in register
        public async Task<IHttpActionResult> GetRoleByDep()
        {
            DataTable dt = new DataTable();
            if (User.IsInRole("Admin"))
            {
                dt = await ManageService.GetRoleByDep("Admin");
            }
            else if (User.IsInRole("Phone_Sale"))
            {
                dt = await ManageService.GetRoleByDep("Phone_Sale");
            }
            else if (User.IsInRole("Phone_Service"))
            {
                dt = await ManageService.GetRoleByDep("Phone_Service");
            }
            else
            {
                DataTable dtuserRole = ManageService.GetLoginUserRole(HttpContext.Current.User.Identity.Name).GetAwaiter().GetResult();
                dt = await ManageService.GetRoleByDep(dtuserRole.Rows[0]["Role"].ToString());

            }
            return Ok(dt);
        }
    }
}
