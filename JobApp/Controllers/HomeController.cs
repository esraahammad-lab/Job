using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;
using System.Web.Mvc;
using JobApp.Models;
using System.Net.Mail;
using System.Net;

namespace JobApp.Controllers
{
    public class HomeController : Controller
    {
        EmploymentEntities db = new EmploymentEntities();
        

        public ActionResult Index()              
        {
            MvcApplication ob = new MvcApplication();
            var mac = ob.GetMacAddress().ToString();
            var obj = db.User.Where(a => a.MacAddress == mac).FirstOrDefault();
           
            if (obj != null)
            {
                if (obj.OnLine == true)
                {
                 int? id  = obj.IDUser;
                 Session["UserID"]=id;
                    Session["na"] = obj.Name.ToString();
                    if (obj.IDTypeUser == 1)
                    {
                        Session["TypePub"] = "pub";
                    }

                    //ViewBag.mas = "notfirst";
                    Session["MeaasgeView"] = "notfirst";
                }
            }
           
            return View(db.Job.ToList());
        }

     
        public ActionResult ListJob()
        {
            ViewBag.header = "Browse Job";
            return View(db.Job.ToList());
        }
       
        public ActionResult About()
        {
            ViewBag.header = "About";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.header = "Contact";
            
            MvcApplication ob = new MvcApplication();
            var mac = ob.GetMacAddress().ToString();
            var obj = db.User.Where(a => a.MacAddress == mac).FirstOrDefault();
            if (obj == null)
            {

                return RedirectToAction("login", "Account");
            }
            else{
               
            return View();
                }
        }
        [HttpPost]
        public ActionResult Contact(ContactModel con)
        {
            
            var mail = new MailMessage();
            var loginInfo = new NetworkCredential("esraahammad654123@gmail.com", "654123HammadEsraa");
            mail.From = new MailAddress(con.email);
            mail.To.Add(new MailAddress("esraahammad654123@gmail.com"));
            mail.Subject = con.subject;
            mail.IsBodyHtml = true;
            string body = "Name:" + con.name + "<br>" +
                "Email:" + con.email + "<br>" +
                "Subject:" + con.subject + "<br>" +
                "Message:<b>" + con.message + "<b>";
            mail.Body = body;
            var smtpClient = new SmtpClient("smtp.gmail.com", 587);
            smtpClient.EnableSsl = true;
           
           
            smtpClient.Credentials = loginInfo;
            smtpClient.Send(mail);

            return RedirectToAction("Index");
            
        }

        public static bool CheckForInternetConnection()
        {
            try { 
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.google.com"))
                {
                  
                    return true;
                }
            }
            catch
            {
                return false;
            }
            
            
        }
       
    }
}