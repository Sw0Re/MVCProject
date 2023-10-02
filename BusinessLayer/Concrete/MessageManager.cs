using BusinessLayer.Abstract;
using DataAccessLayer.Abstract;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Concrete
{
    public class MessageManager : IMessageService
    {

        IMessageDal _messageDal;

        public MessageManager(IMessageDal messageDal)
        {
            _messageDal = messageDal;
        }

        public Message GetById(int id)
        {
            return _messageDal.Get(x => x.MessageID == id);
        }

        public List<Message> GetListInbox(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p).OrderByDescending(x => x.MessageDate).ToList();
        }

        public List<Message> GetListSendbox(string p)
        {
            return _messageDal.List(x => x.SenderMail == p).OrderByDescending(x=>x.MessageDate).ToList();
        }

        public List<Message> GetList(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p).Where(x => x.IsRead == true).OrderByDescending(x => x.MessageDate).ToList();
        }

        public List<Message> GetListUnRead(string p)
        {
            return _messageDal.List(x => x.ReceiverMail == p).Where(x => x.IsRead == false).OrderByDescending(x => x.MessageDate).ToList();
        }

        public void MessageAdd(Message message)
        {
            _messageDal.Insert(message);
        }

        public void MessageDelete(Message message)
        {
            _messageDal.Delete(message);
        }

        public void MessageUpdate(Message message)
        {
            _messageDal.Update(message);
        }
    }
}
