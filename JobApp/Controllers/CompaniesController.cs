using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobApp.Models;
using System.IO;
using System.Data.Objects;
using System.Data.Entity.Validation;
namespace JobApp.Controllers
{
    public class CompaniesController : Controller
    {
        private EmploymentEntities db = new EmploymentEntities();

    

        public ActionResult Index()
        {
            ViewBag.header = "Posted A Job";
            Job ap = new Job();
            ApplyForJob p = new ApplyForJob();
            MvcApplication ob = new MvcApplication();
            var mac = ob.GetMacAddress().ToString();
            var obj = db.User.Where(a => a.MacAddress == mac).FirstOrDefault();
            if (obj == null)
            {
                Session["UserPostJob"] = "Create";
                return RedirectToAction("login", "Account");
            }
            else
            {
                Session["UserPostJob"] = obj.IDUser;
                if (obj.IDTypeUser == 1)
                {
                    var list = db.Job.Where(a => a.IDUser == obj.IDUser).ToList().OrderByDescending(x => x.IDJob);

                    return View(list);
                }
                else
                {
                    ViewBag.Pub = "NotPublisher";
                    return View();
                }
            }
        }


        public ActionResult Create()
        {
            ViewBag.header = "Posted New Job";
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Job job, HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
                upload.SaveAs(path);
                job.JobImage = upload.FileName;
                job.publishon = DateTime.Now;
                job.CompanyName = Session["na"].ToString();
                job.IDUser = int.Parse(Session["UserID"].ToString());
                db.Job.Add(job);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(job);
        }

        public ActionResult JobEdit(int id)
        {
            var obj = db.Job.Where(a => a.IDJob == id).FirstOrDefault();
            ViewBag.header = "Editing " + obj.JobTitle;
            return View(obj);
        }
        [HttpPost, ValidateInput(false)]
        [ValidateAntiForgeryToken]
       
        public ActionResult JobEdit(string JobTitle, string JobContent, string Location, float Salary,
            int id,string JobNature, DateTime Dateline, HttpPostedFileBase upload)
        {
            var obj = db.Job.Where(a => a.IDJob == id).FirstOrDefault();
            obj.JobTitle = JobTitle;
            string path = Path.Combine(Server.MapPath("~/Uploads"), upload.FileName);
            upload.SaveAs(path);
            obj.JobImage = upload.FileName;
            obj.JobContent = JobContent;
            obj.Location = Location;
            obj.Salary = Salary;
            obj.JobNature = JobNature;
            obj.Dateline = Dateline;

            obj.CompanyName = Session["na"].ToString();
            obj.publishon = DateTime.Now;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public JsonResult Delete(int id)
        {
            var Alljobapply = db.ApplyForJob.Where(x => x.IDJob == id).ToList();
            var result = "Error";
            if (Alljobapply != null)
            {
                foreach (var item in Alljobapply)
                {
                    db.ApplyForJob.Remove(item);
                    db.SaveChanges();
                }
            }
            Job obj = db.Job.Where(x => x.IDJob == id).FirstOrDefault();

            db.Job.Remove(obj);
            db.SaveChanges();
            result = "success";
            return Json(result, JsonRequestBehavior.AllowGet);

        }

        public ActionResult JobAppliers(int id)
        {
            ViewBag.header = " Applicants for this job";
            var mod = db.ApplyForJob.Where(a => a.IDJob == id).ToList();
            if (mod.Count == 0)
            {
                ViewBag.Mess = "no One";
            }
            return View(mod);
        }

        public FileResult Download(HttpPostedFileBase file, int id)
        {
            var obj = db.ApplyForJob.Where(a => a.IDApply == id).FirstOrDefault();
            var Cv = obj.CV;
            string name = obj.User.Name;
            string filePath = Path.Combine(Server.MapPath("~/CV"), Cv);

            byte[] fileBytes = System.IO.File.ReadAllBytes(filePath);
            string fileName = name + Path.GetExtension(filePath);
            return File(fileBytes, System.Net.Mime.MediaTypeNames.Application.Octet, fileName);

        }

        public ActionResult JobDetails(int id)
        {

            var obj = db.Job.Where(a => a.IDJob == id).FirstOrDefault();

            ViewBag.header = obj.JobTitle;

            return View(obj);
        }

   
       

    }
}
