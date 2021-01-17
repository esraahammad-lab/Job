using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using JobApp.Models;

namespace JobApp.Controllers
{
    public class Default1Controller : Controller
    {
        private EmploymentEntities db = new EmploymentEntities();

        // GET: /Default1/
        public ActionResult Index()
        {
            var applyforjob = db.ApplyForJob.Include(a => a.Job).Include(a => a.User);
            return View(applyforjob.ToList());
        }

        // GET: /Default1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyForJob applyforjob = db.ApplyForJob.Find(id);
            if (applyforjob == null)
            {
                return HttpNotFound();
            }
            return View(applyforjob);
        }

        // GET: /Default1/Create
        public ActionResult Create()
        {
            ViewBag.IDJob = new SelectList(db.Job, "IDJob", "JobTitle");
            ViewBag.IDUser = new SelectList(db.User, "IDUser", "Name");
            return View();
        }

        // POST: /Default1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="IDApply,Message,ApplyDate,IDJob,IDUser,CV")] ApplyForJob applyforjob)
        {
            if (ModelState.IsValid)
            {
                db.ApplyForJob.Add(applyforjob);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IDJob = new SelectList(db.Job, "IDJob", "JobTitle", applyforjob.IDJob);
            ViewBag.IDUser = new SelectList(db.User, "IDUser", "Name", applyforjob.IDUser);
            return View(applyforjob);
        }

        // GET: /Default1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyForJob applyforjob = db.ApplyForJob.Find(id);
            if (applyforjob == null)
            {
                return HttpNotFound();
            }
            ViewBag.IDJob = new SelectList(db.Job, "IDJob", "JobTitle", applyforjob.IDJob);
            ViewBag.IDUser = new SelectList(db.User, "IDUser", "Name", applyforjob.IDUser);
            return View(applyforjob);
        }

        // POST: /Default1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="IDApply,Message,ApplyDate,IDJob,IDUser,CV")] ApplyForJob applyforjob)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applyforjob).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IDJob = new SelectList(db.Job, "IDJob", "JobTitle", applyforjob.IDJob);
            ViewBag.IDUser = new SelectList(db.User, "IDUser", "Name", applyforjob.IDUser);
            return View(applyforjob);
        }

        // GET: /Default1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplyForJob applyforjob = db.ApplyForJob.Find(id);
            if (applyforjob == null)
            {
                return HttpNotFound();
            }
            return View(applyforjob);
        }

        // POST: /Default1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ApplyForJob applyforjob = db.ApplyForJob.Find(id);
            db.ApplyForJob.Remove(applyforjob);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
