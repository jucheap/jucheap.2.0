using System.Web.Mvc;
using JuCheap.Service.Abstracts;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class ControlController : AdmBaseController
    {

        public IRoleMenuService roleMenuService { get; set; }

        public IUserRoleService userRoleService { get; set; }

        // GET: Adm/Control
        public ActionResult Index()
        {
            if (IsLogined)
            {
                //获取拥有的角色
                var userid = CurrentUser.Id;
                ViewBag.MyMenus = userService.GetMyMenus(userid);
            }
            return View();
        }

        /// <summary>
        /// Welcome
        /// </summary>
        /// <returns></returns>
        public ActionResult Welcome()
        {
            return View();
        }
    }
}