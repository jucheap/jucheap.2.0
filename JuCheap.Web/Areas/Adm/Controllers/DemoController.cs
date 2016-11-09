using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JuCheap.Service.Dto;

namespace JuCheap.Web.Areas.Adm.Controllers
{
    public class DemoController : AdmBaseController
    {
        // GET: Adm/Demo/Base
        public ActionResult Base()
        {
            return View();
        }

        public ActionResult Fontawosome()
        {
            return View();
        }

        public ActionResult Form()
        {
            return View();
        }

        public ActionResult Advance()
        {
            return View();
        }

        public ActionResult Gallery()
        {
            return View();
        }

        public new ActionResult Profile()
        {
            return View();
        }

        public ActionResult InBox()
        {
            return View();
        }

        public ActionResult InBoxDetail()
        {
            return View();
        }

        public ActionResult InBoxCompose()
        {
            return View();
        }

        public ActionResult Editor()
        {
            return View();
        }

        public ActionResult FormValidate()
        {
            return View();
        }

        public ActionResult Chart()
        {
            return View();
        }

        public ActionResult ChartMorris()
        {
            return View();
        }

        public ActionResult ChartJs()
        {
            return View();
        }

        public ActionResult DataTable()
        {
            return View();
        }

        public ActionResult DataTableAdv()
        {
            return View();
        }

        public JsonResult GetData()
        {
            ResultDto<dynamic> res = new ResultDto<dynamic>();
            
            List<dynamic> list = new List<dynamic>(10);
            for (int i = 0; i < 10; i++)
            {
                list.Add(new
                {
                    name = "AA" + i,
                    position = "局长" + i,
                    office = "Office" + i,
                    salary = i*987
                });
            }
            res.data = list;
            res.length = 10;
            res.recordsTotal = 10;
            return Json(res, JsonRequestBehavior.AllowGet);
        }
    }
}