using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Answer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello WellCode.io !!");
            Notification notif = new Notification("iwasug");
            notif.status();
            notif = new Notification();
            notif.status();
            EmailNotification Eml = new EmailNotification("iwasug@email");
            Eml.status();
            Eml = new EmailNotification();
            Eml.status();

            using(var Db = new DBContext())
            {
                var a = Db.Users
                    .Include(x => x.address_id)
                    .Where(x => x.address_id.city == "Bandung")
                    .ToList();
                //SELECT * FROM Users WHERE address_id IN(SELECT id FROM address WHERE city='Bandung');

                var b = Db.Users
                    .Where(x => x.created_at <= DateTime.Now.AddMonths(-3))
                    .OrderByDescending(x => x.created_at)
                    .ToList();
                //SELECT * FROM Users WHERE created_at <= DATE(NOW() - INTERVAL 3 MONTH) ORDER BY created_at DESC;

            }
            Console.ReadLine();
        }
    }

    public class Notification
    {
        protected string receiver;
        protected bool isRead = false;
        public Notification(string _receiver = null)
        {
            this.receiver = _receiver;
            markAsRead();
        }

        public bool validReceiver()
        {
            if (string.IsNullOrEmpty(receiver))
                return false;
            return true;
        }

        public void markAsRead()
        {
            if (validReceiver())
                isRead = true;
        }

        public virtual void status()
        {
            if (isRead)
                Console.WriteLine("read");
            else
                Console.WriteLine("unread");
        }
    }

    public class EmailNotification : Notification 
    {
        public EmailNotification(string _receiver = null)
        {
            receiver = _receiver;
            markAsRead();
        }
        public override void status()
        {
            if (isRead)
                Console.WriteLine("read-email");
            else
                Console.WriteLine("unread-email");
        }

    }

    public class Users
    {
        [Required]
        public int id { set; get; }
        public string email { set; get; }
        public string encrypted_password { set; get; }
        public DateTime created_at { set; get; }
        public Address address_id { set; get; }
    }

    public class Address
    {
        [Required]
        public int id { set; get; }
        public string street { set; get; }
        public string city { set; get; }
        public string province { set; get; }
        public string zip { set; get; }
        public string country { set; get; }
    }

}
