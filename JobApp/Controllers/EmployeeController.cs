using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobApp.Models;
using System.IO;

namespace JobApp.Controllers
{
    public class EmployeeController : Controller
    {
        EmploymentEntities db = new EmploymentEntities();
       
        public ActionResult ApplyDetails(int id)
        {
            MvcApplication ob = new MvcApplication();
            var mac = ob.GetMacAddress().ToString();
            var obj = db.User.Where(a => a.MacAddress == mac).FirstOrDefault();         
            var jobobj = db.Job.Where(a => a.IDJob == id).FirstOrDefault();
            Session["IDJOB"]=jobobj.IDJob;
            ViewBag.header = jobobj.JobTitle;
                if (obj == null)
                {
                    Session["IDJOB"] = id;
                    Session["ApplyJob"] = "Create";
                    return RedirectToAction("login", "Account");
                }
                else
                {
                    var check = db.ApplyForJob.Where(a => a.User.IDUser == obj.IDUser && a.IDJob == id).ToList();
                    Session["IDJOB"] = id;
                    Session["UserPostJob"] = obj.IDUser;
                    if (obj.IDTypeUser == 2) { 
                    ViewBag.header = jobobj.JobTitle;
                    if (check.Count < 1)
                    {
                        ViewBag.result = "Succ";
                    }
                    else
                    {
                        ViewBag.result = "Error";
                        
                    }

                    return View(jobobj);
                    }
                    else
                    {
                        ViewBag.Res = "not Researcher";
                        return View();
                    }
                }
               
           
        }
      [HttpPost]
        public ActionResult ApplyDetails(int id, string Mess, HttpPostedFileBase upload)
        {

            int ui = Int32.Parse(Session["UserID"].ToString());
            ApplyForJob ap = new ApplyForJob();
            ap.IDUser=ui;
           
            ap.IDJob = id;
            ap.Message = Mess;
            ap.ApplyDate = DateTime.Now;
            string path = Path.Combine(Server.MapPath("~/CV"), upload.FileName);
            upload.SaveAs(path);
            ap.CV = upload.FileName;
            db.ApplyForJob.Add(ap);
            db.SaveChanges();

            return RedirectToAction("Index","Home");
           
        }

      public ActionResult ApplyingJob()
      {
          ViewBag.header = "Applied Jobs";
          int ui = Int32.Parse(Session["UserID"].ToString());

          var jobaply = db.ApplyForJob.Where(x => x.IDUser == ui).ToList();
          return View(jobaply);
      }
       
      
	}
}