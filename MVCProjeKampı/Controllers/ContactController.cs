using BusinessLayer.Concrete;
using BusinessLayer.ValidationRules;
using DataAccessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProjeKampı.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        Context _Context = new Context();
        ContactManager cm = new ContactManager(new EfContactDal());
        MessageManager mm = new MessageManager(new EfMessageDal());
        ContactValidator cv = new ContactValidator();
        public ActionResult Index()
        {
            var contactvalues = cm.GetList();
            return View(contactvalues);
        }

        public ActionResult GetContactDetails(int id)
        {
            var contactvalues = cm.GetById(id);
            return View(contactvalues);

        }

        public ActionResult MessageListMenu()
        {

            return PartialView();

        }
        public PartialViewResult ContactMenuPartial()
        {
           string p = (string)Session["AdminUsername"];

            var receiverMail = _Context.Messages.Count(x => x.ReceiverMail == p).ToString();
            ViewBag.receiverMail = receiverMail;

            var senderMail = _Context.Messages.Count(x => x.SenderMail == p).ToString();
            ViewBag.senderMail = senderMail;

            var contact = _Context.Contacts.Count().ToString();
            ViewBag.contact = contact;

            var draft = _Context.Messages.Count(x => x.isDraft == true && x.SenderMail==p ).ToString();
            ViewBag.draft = draft;

            var readMessage = mm.GetList(p).Count();
            ViewBag.readMessage = readMessage;

            var unReadMessage = mm.GetListUnRead(p).Count();
            ViewBag.unReadMessage = unReadMessage;


            return PartialView();
        }

    }
}