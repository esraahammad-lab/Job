using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.NetworkInformation;
using System.Web.Mvc;
using JobApp.Models;

namespace JobApp.Controllers
{
    public class AccountController : Controller
    {
        EmploymentEntities db = new EmploymentEntities();

        public PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet && nic.OperationalStatus == OperationalStatus.Up)
                {
                }
                return nic.GetPhysicalAddress();
            }
            return null;

        }

        public ActionResult Register()
        {
            ViewBag.header = "Register";
            ViewBag.userType = new SelectList(db.UserType, "IDUserType", "UserType1");
            return View();
        }
        [HttpPost]
        public ActionResult Register(string name = "", string email = "", int userType = 0, string password = "",
            string confirm = "", string phone = "", string gender = "")
        {
            var test = db.User.Where(a => a.Email == email).FirstOrDefault();
         string mac = GetMacAddress().ToString();
            if (test == null)
            {
               User us = new User();             
               us.MacAddress = mac;
                us.Name = name;
                us.Email = email;
                us.Password = password;
                us.Phone = phone;
                us.Gender = gender;
                us.IDTypeUser = userType;
                us.OnLine = true;
                db.User.Add(us);
                db.SaveChanges();
                Session["Name"] = name;
                Session["UserID"] = us.IDUser;               
                return RedirectToAction("Index","Home");
            }
            ViewBag.message = "faild";
            return View();
        }

        public ActionResult login()
        {
            ViewBag.header = "log In";
            return View();
        }
        [HttpPost]
        public ActionResult login( string email,string password)
        {
            string mac = GetMacAddress().ToString();
            var obj = db.User.Where(a => a.Email == email && a.Password == password).FirstOrDefault();
            if (obj != null)
            {
                Session["Name"] = obj.Name.ToString();
                Session["UserID"] = obj.IDUser.ToString();
                obj.MacAddress=mac;
                 obj.OnLine=true;              
                db.SaveChanges();
                if (Session["UserPostJob"] == "Create" || Session["UserPostJob"] != null)
                {
                    Session["na"] = obj.Name;
                    Session["MeaasgeView"] = "notfirst";
                    return RedirectToAction("index", "Companies");
                }
                else if (Session["ApplyJob"] == "Create")
                {
                    Session["na"] = obj.Name;
                    Session["MeaasgeView"] = "notfirst";
                    var id = Session["IDJOB"];
                    return RedirectToAction("ApplyDetails", "Employee", new { id });
                }
                return RedirectToAction("Index", "Home");

            }
            
            ViewBag.message = "faild";
            return View();
        }

        public ActionResult logout()
        {
            int ui = Int32.Parse(Session["UserID"].ToString());
            var obj = db.User.Where(a => a.IDUser == ui).FirstOrDefault();
            obj.OnLine = false;
            obj.MacAddress = null;
            db.SaveChanges();  
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }


        public ActionResult EditData()
        {
            ViewBag.header = "Edit Account";
            int ui = Int32.Parse(Session["UserID"].ToString());
            var obj = db.User.Where(a => a.IDUser == ui).FirstOrDefault();
            return View(obj);
        }
        [HttpPost]
        public ActionResult EditData(string name, string email, string phone, string oldpassword, string password)
        {

            int ui = Int32.Parse(Session["UserID"].ToString());
            var obj = db.User.Where(a => a.IDUser == ui).FirstOrDefault();
            if (obj.Password == oldpassword)
            {
                obj.Name = name;
                obj.Email = email;
                obj.Password = password;
                obj.Phone = phone;
                db.SaveChanges();
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewBag.message = "faild";
                return View(obj);
            }

        }

       
     
}

   
}