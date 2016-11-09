using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using JuCheap.Core.Extentions;
using JuCheap.Service.Abstracts;
using JuCheap.Service.Dto;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class UserController : AdmBaseController
    {
        public IUserRoleService userRoleService { get; set; }

        

        #region Page

        // GET: Adm/User
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <returns></returns>
        public ActionResult Add(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        public ActionResult Edit(int moudleId, int menuId, int btnId, int id)
        {
            var model = userService.GetOne(item => item.Id == id);
            return View(model);
        }

        /// <summary>
        /// 用户角色授权
        /// </summary>
        /// <param name="moudleId"></param>
        /// <param name="menuId"></param>
        /// <param name="btnId"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public ActionResult Authen(int moudleId, int menuId, int btnId, int id)
        {
            return View();
        }

        // GET: Adm/User/Login
        [HttpGet, AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        /// <summary>
        /// 忘记密码
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult ForgotPwd()
        {
            return View();
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult Reg()
        {
            return View();
        }

        /// <summary>
        /// 注销
        /// </summary>
        /// <returns></returns>
        public ActionResult Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                userService.Logout();
            }
            return RedirectToAction("Login");
        }

        #endregion

        #region Ajax

        [HttpGet]
        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"]
            };
            Expression<Func<UserDto, bool>> exp = item => !item.IsDeleted;
            if (!queryBase.SearchKey.IsBlank())
                exp = exp.And(item => item.LoginName.Contains(queryBase.SearchKey) || item.RealName.Contains(queryBase.SearchKey));

            var dto = userService.GetWithPages(queryBase, exp, Request["orderBy"], Request["orderDir"]);
            var res = new ResultDto<UserDto>
            {
                start = dto.start,
                length = dto.length,
                recordsTotal = dto.recordsTotal,
                data = dto.data.Select(d => new UserDto
                {
                    Id = d.Id,
                    LoginName = d.LoginName,
                    RealName = d.RealName,
                    Email = d.Email,
                    CreateDateTime = d.CreateDateTime,
                    Status = d.Status,
                    IsDeleted = d.IsDeleted
                }).ToList()
            };
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetMyRoles(int moudleId, int menuId, int btnId, int id)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"]
            };

            var dto = userService.GetMyRoles(queryBase, id);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetNotMyRoles(int moudleId, int menuId, int btnId, int id)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"]
            };

            var dto = userService.GetNotMyRoles(queryBase, id);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult AuthenRole(int moudleId, int menuId, int btnId, int id, List<RoleDto> roles)
        {
            var dto = new Result<string>();
            var userRoles = roles.Select(item => new UserRoleDto {UserId = id, RoleId = item.Id}).ToList();
            dto.flag = userRoleService.Add(userRoles);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult UnAuthenRole(string moudleId, string menuId, string btnId, string id, List<RoleDto> roles)
        {
            var dto = new Result<string>();
            var roleIds = roles.Select(item => item.Id);
            dto.flag = userRoleService.Delete(item => roleIds.Contains(item.RoleId));
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, AllowAnonymous]
        public ActionResult Login(UserDto model)
        {
            var result = userService.Login(model);
            if (result.flag)
            {
                return RedirectToAction("Index", "Control");
            }
            ModelState.AddModelError("Error", result.msg);
            return View();
        }



        [HttpPost]
        public ActionResult Add(int moudleId, int menuId, int btnId, UserDto dto)
        {
            dto.Password = dto.Password.ToMD5();
            userService.Add(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public ActionResult Edit(string moudleId, string menuId, string btnId, UserDto dto)
        {
            var old = userService.GetOne(item => item.Id == dto.Id);
            dto.Password = dto.Password.IsBlank() ? old.Password : dto.Password.ToMD5();
            userService.Update(dto);
            return RedirectToAction("Index", RouteData.Values);
        }


        [HttpPost]
        public JsonResult Delete(int moudleId, int menuId, int btnId, List<int> ids)
        {
            var res = new Result<string>();

            if (ids != null && ids.Any())
                res.flag = userService.Delete(item => ids.Contains(item.Id));

            return Json(res, JsonRequestBehavior.AllowGet);
        }
        [AllowAnonymous]
        public JsonResult GetTestData()
        {
            List<UserDto> users = new List<UserDto>();
            for (int i = 0; i < 5; i++)
            {
                users.Add(new UserDto()
                {
                    LoginName = "i" + i,
                    Id = i
                });
            }
            return Json(users, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}