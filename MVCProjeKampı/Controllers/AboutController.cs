using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampı.Controllers
{
    public class AboutController : Controller
    {
        // GET: About

        AboutManager abm = new AboutManager(new EfAboutDal());
        AboutValidator aboutvalidator = new AboutValidator();
        public ActionResult Index()
        {
            var aboutvalues = abm.GetList();
            return View(aboutvalues);
        }
        [HttpGet]
        public ActionResult AddAbout()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddAbout(About p)
        {
            ValidationResult results = new ValidationResult();
            results = aboutvalidator.Validate(p);

            if (results.IsValid)
            {
                if (Request.Files.Count > 0)
                {

                    string fileName = Path.GetFileName(Request.Files[0].FileName);
                    string expansion = Path.GetExtension(Request.Files[0].FileName);

                    string fileName1 = Path.GetFileName(Request.Files[1].FileName);
                    string expansion2 = Path.GetExtension(Request.Files[1].FileName);
                    string path = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                    Request.Files[0].SaveAs(Server.MapPath(path));

                    p.AboutImage1 = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                    p.AboutImage2 = "/AdminLTE-3.0.4/images/" + fileName1 + expansion2;
                    abm.AboutAdd(p);
                    return RedirectToAction("Index");
                }
            }
            else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                       
                    }
                }
               
            return RedirectToAction("Index");
        }

        public PartialViewResult AboutPartial()
        {

            return PartialView();
        }

        public ActionResult DeleteAbout(int id)
        {
            var AboutValue = abm.GetById(id);
            if (AboutValue.AboutStatus == true)
            {
                AboutValue.AboutStatus = false;

            }
            else
            {
                AboutValue.AboutStatus = true;

            }
            abm.AboutDelete(AboutValue);
            return RedirectToAction("Index");

        }


    }
}