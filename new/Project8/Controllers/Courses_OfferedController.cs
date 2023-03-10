using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project8.Models;

namespace Project8.Controllers
{
    public class Courses_OfferedController : Controller
    {
        private Project8Entities2 db = new Project8Entities2();

        // GET: Courses_Offered
        public ActionResult Index()
        {
            var courses_Offered = db.Courses_Offered.Include(c => c.Cours).Include(c => c.Doctor).Include(c => c.semester);
            return View(courses_Offered.ToList());
        }

        // GET: Courses_Offered/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses_Offered courses_Offered = db.Courses_Offered.Find(id);
            if (courses_Offered == null)
            {
                return HttpNotFound();
            }
            return View(courses_Offered);
        }

        // GET: Courses_Offered/Create
        public ActionResult Create()
        {
            ViewBag.course_id = new SelectList(db.Courses, "Course_Id", "Course_Name");
            ViewBag.doctor_id = new SelectList(db.Doctors, "Doctor_Id", "Doctor_Name");
            ViewBag.semester_id = new SelectList(db.semesters, "id", "name");
            ViewBag.Days_id = new SelectList(db.Days, "Days_id", "Days");

            return View();
        }

        // POST: Courses_Offered/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "course_id,start_time,end_time,doctor_id,semester_id,Days_id")] Courses_Offered courses_Offered,string start_Hour,string start_min ,string end_Hour , string end_min  )
        {
            if (ModelState.IsValid)
            {
                string startTime = start_Hour + ":" + start_min;
                courses_Offered.start_time =TimeSpan.Parse(startTime);
                string endTime=end_Hour+ ":" + end_min; 
                courses_Offered.end_time=TimeSpan.Parse(endTime);
                db.Courses_Offered.Add(courses_Offered);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.course_id = new SelectList(db.Courses, "Course_Id", "Course_Name", courses_Offered.course_id);
            ViewBag.doctor_id = new SelectList(db.Doctors, "Doctor_Id", "Doctor_Name", courses_Offered.doctor_id);
            ViewBag.semester_id = new SelectList(db.semesters, "id", "name", courses_Offered.semester_id);
            return View(courses_Offered);
        }

        // GET: Courses_Offered/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses_Offered courses_Offered = db.Courses_Offered.Find(id);
            if (courses_Offered == null)
            {
                return HttpNotFound();
            }
            ViewBag.course_id = new SelectList(db.Courses, "Course_Id", "Course_Name", courses_Offered.course_id);
            ViewBag.doctor_id = new SelectList(db.Doctors, "Doctor_Id", "Doctor_Name", courses_Offered.doctor_id);
            ViewBag.semester_id = new SelectList(db.semesters, "id", "name", courses_Offered.semester_id);
            ViewBag.Days_id = new SelectList(db.Days, "Days_id", "Days");

            return View(courses_Offered);
        }

        // POST: Courses_Offered/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "offered_id,course_id,start_time,end_time,doctor_id,semester_id,Days_id")] Courses_Offered courses_Offered, string start_Hour, string start_min, string end_Hour, string end_min)
        {
            if (ModelState.IsValid)
            {
                if (start_Hour != null||start_min!=null) { 
                string startTime = start_Hour + ":" + start_min;

                courses_Offered.start_time = TimeSpan.Parse(startTime);
                }
                if (end_Hour != null || end_min != null)
                {
                    string endTime = end_Hour + ":" + end_min;
                    courses_Offered.end_time = TimeSpan.Parse(endTime);
                }
                db.Entry(courses_Offered).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.course_id = new SelectList(db.Courses, "Course_Id", "Course_Name", courses_Offered.course_id);
            ViewBag.doctor_id = new SelectList(db.Doctors, "Doctor_Id", "Doctor_Name", courses_Offered.doctor_id);
            ViewBag.semester_id = new SelectList(db.semesters, "id", "name", courses_Offered.semester_id);
            return View(courses_Offered);
        }

        // GET: Courses_Offered/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Courses_Offered courses_Offered = db.Courses_Offered.Find(id);
            if (courses_Offered == null)
            {
                return HttpNotFound();
            }
            return View(courses_Offered);
        }

        // POST: Courses_Offered/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Courses_Offered courses_Offered = db.Courses_Offered.Find(id);
            db.Courses_Offered.Remove(courses_Offered);
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
