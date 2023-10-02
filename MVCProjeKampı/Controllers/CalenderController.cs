using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using MVCProjeKampı.Calender;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampı.Controllers
{
    [AllowAnonymous]
    public class CalenderController : Controller
    {
        // GET: Calender
        HeadingManager hm = new HeadingManager(new EfHeadingDal());
        [HttpGet]
        public ActionResult Index()
        {
            DateTime date = DateTime.Parse(DateTime.Now.ToShortDateString());
            ViewBag.y=date;
            ViewBag.x = date.AddDays(7 - (int)date.DayOfWeek);
            ViewBag.z = DateTime.Now.AddDays(5);
           
            ViewBag.ErrorMessage = "Tarih karşılaştırması doğrudur";
            return View(new Calenderr());
        }

        public JsonResult GetEvents(DateTime start, DateTime end)
        {
            var viewModel = new Calenderr();
            var events = new List<Calenderr>();
            start = DateTime.Today.AddDays(-14);
            end = DateTime.Today.AddDays(-14);
         

            foreach (var item in hm.GetList())
            {
                events.Add(new Calenderr()
                {   
                    
                    title = item.HeadingName,
                    start = item.HeadingDate.AddDays(1),
                    end = item.HeadingDate.AddDays(-14),
                    allDay = false
                });

                start = start.AddDays(7);
                end = end.AddDays(7);
            }

            return Json(events.ToArray(), JsonRequestBehavior.AllowGet);
        }
    }
}