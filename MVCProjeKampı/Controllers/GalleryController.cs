﻿using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampı.Controllers
{
    public class GalleryController : Controller
    {
        // GET: Gallery

        ImageFileManager ifm = new ImageFileManager(new EfImageFileDal());
        public ActionResult Index()
        {
            var files = ifm.GetList();

            return View(files);
        }

        [HttpGet]
        public ActionResult ImageAdd()
        {
            return View();
        }

        [HttpPost]
        public ActionResult ImageAdd(ImageFile p)
        {
            if (Request.Files.Count > 0)
            {
                string fileName = Path.GetFileName(Request.Files[0].FileName);
                string expansion = Path.GetExtension(Request.Files[0].FileName);
                string path = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                Request.Files[0].SaveAs(Server.MapPath(path));
                p.ImagePath = "/AdminLTE-3.0.4/images/" + fileName + expansion;
                ifm.Add(p);
                return RedirectToAction("Index");

            }
            return View();

        }


    }
}