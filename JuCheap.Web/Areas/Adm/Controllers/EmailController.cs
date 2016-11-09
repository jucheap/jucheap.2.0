using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;
using JuCheap.Core.Extentions;
using JuCheap.Service.Abstracts;
using JuCheap.Service.Dto;
using JuCheap.Service.Enum;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class EmailController : AdmBaseController
    {
        public IEmailPoolService emailPoolService { get; set; }

        public IEmailReceiverService emailReceService { get; set; }

        #region Page

        // GET: Adm/Email
        public ActionResult Index(int moudleId, int menuId, int btnId)
        {
            GetButtons(menuId);
            return View();
        }

        // GET: Adm/Email/Add
        public ActionResult Add(int moudleId, int menuId, int btnId)
        {
            return View();
        }

        #endregion

        #region Ajax


        [HttpPost]
        public ActionResult Add(int moudleId, int menuId, int btnId, EmailPoolDto dto)
        {
            if (dto != null && !dto.ReceiverEmails.IsBlank())
            {
                dto.Status = EmailStatus.等待发送;
                var res = emailPoolService.Add(dto);
                if (res)
                {
                    var list = new List<EmailReceiverDto>();
                    dto.ReceiverEmails.Split(new []{';',','}, StringSplitOptions.RemoveEmptyEntries).ToList()
                        .ForEach(email =>
                    {
                        if (email.IsValidEmail())
                        {
                            list.Add(new EmailReceiverDto
                            {
                                EmailId = dto.Id,
                                Email = email,
                                Type = EmailReceiverType.收件人
                            });
                        }
                    });
                    emailReceService.Add(list);
                }
            }
            return RedirectToAction("Index", RouteData.Values);
        }

        public JsonResult GetList(int moudleId, int menuId, int btnId)
        {
            var queryBase = new QueryBase
            {
                Start = Request["start"].ToInt(),
                Length = Request["length"].ToInt(),
                Draw = Request["draw"].ToInt(),
                SearchKey = Request["keywords"]
            };
            Expression<Func<EmailPoolDto, bool>> exp = item => !item.IsDeleted;
            if (!queryBase.SearchKey.IsBlank())
                exp = exp.And(item => item.Title.Contains(queryBase.SearchKey));

            var dto = emailPoolService.GetWithPages(queryBase, exp, Request["orderBy"], Request["orderDir"]);
            return Json(dto, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}