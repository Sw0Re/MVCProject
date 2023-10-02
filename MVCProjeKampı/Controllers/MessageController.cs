using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampı.Controllers
{
    public class MessageController : Controller
    {
        // GET: Message
        MessageManager mm = new MessageManager(new EfMessageDal());
        MessageValidator messagevalidator = new MessageValidator();
        Context c = new Context();
        public ActionResult Inbox(string p)
        {
            p = (string)Session["AdminUsername"];
          
            var messagelist = mm.GetListInbox(p);
        
            return View(messagelist);
        }

        public ActionResult Sendbox(string p)
        {
            p = (string)Session["AdminUsername"];
            var messagelist = mm.GetListSendbox(p);
            return View(messagelist);
        }

        public ActionResult GetInboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);

        }
        public ActionResult GetSendboxMessageDetails(int id)
        {
            var values = mm.GetById(id);
            return View(values);

        }

        [HttpGet]
        public ActionResult NewMessage()
        {
            return View();
        }

        [HttpPost]

        public ActionResult NewMessage(Message p, string button)
        {
          
            string c = (string)Session["AdminUsername"];
            ValidationResult results = new ValidationResult();
            if (button == "draft")
            {

                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Now;
                    p.SenderMail = c;
                    p.isDraft = true;
                    mm.MessageAdd(p);
                    return RedirectToAction("Draft");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            else if (button == "save")
            {
                results = messagevalidator.Validate(p);
                if (results.IsValid)
                {
                    p.MessageDate = DateTime.Now;
                    p.SenderMail = c;
                    p.isDraft = false;
                    mm.MessageAdd(p);
                    return RedirectToAction("Sendbox");
                }
                else
                {
                    foreach (var item in results.Errors)
                    {
                        ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                    }
                }
            }
            return View();
        }
        public ActionResult Draft(string p)
        {
            p = (string)Session["AdminUsername"];
            var sendList = mm.GetListSendbox(p);
            var draftList = sendList.FindAll(x => x.isDraft == true);
            return View(draftList);
        }

        public ActionResult GetDraftMessageDetails(int id)
        {
            var Values = mm.GetById(id);
            return View(Values);
        }

        public ActionResult IsRead(int id)
        {
            var result = mm.GetById(id);
            if (result.IsRead == false)
            {
                result.IsRead = true;
            }
            mm.MessageUpdate(result);
            return RedirectToAction("ReadMessage");
        }

        public ActionResult ReadMessage(string p)
        {
            p = (string)Session["AdminUsername"];
            var readMessage = mm.GetList(p).Where(x => x.IsRead == true).OrderByDescending(x => x.MessageDate).ToList();
            return View(readMessage);
        }

        public ActionResult UnReadMessage(string p)
        {
            p = (string)Session["AdminUsername"];
            var unReadMessage = mm.GetListUnRead(p);
            return View(unReadMessage);
        }
    }
}