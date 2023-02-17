using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Project8.Models;

namespace Project8.Controllers
{
    public class Cours1Controller : Controller
    {
        private Project8Entities2 db = new Project8Entities2();

        // GET: Cours1
        public ActionResult Index()
        {
            var courses = db.Courses.Include(c => c.Major).Include(c => c.Cours1);
            return View(courses.ToList());
        }

        // GET: Cours1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // GET: Cours1/Create
        public ActionResult Create()
        {
            ViewBag.Major_Id = new SelectList(db.Majors, "Major_Id", "Major_Name");
            //ViewBag.dependent_Course = new SelectList(db.Courses, "Course_Id", "Course_Name");
            List<SelectListItem> courseList = new List<SelectListItem>();
            courseList.Add(new SelectListItem { Text = "Select a course", Value = null });
            courseList.AddRange(db.Courses.Select(c => new SelectListItem { Text = c.Course_Name, Value = c.Course_Id.ToString() }));
            ViewBag.dependent_Course = new SelectList(courseList, "Value", "Text");
            return View();
        }

        // POST: Cours1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Course_Id,Course_Name,Number_Of_Hours,Course_Description,Major_Id,syllabus,dependent_Course")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Courses.Add(cours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Major_Id = new SelectList(db.Majors, "Major_Id", "Major_Name", cours.Major_Id);
            ViewBag.dependent_Course = new SelectList(db.Courses, "Course_Id", "Course_Name", cours.dependent_Course);
            return View(cours);
        }

        // GET: Cours1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            ViewBag.Major_Id = new SelectList(db.Majors, "Major_Id", "Major_Name", cours.Major_Id);
            List<SelectListItem> courseList = new List<SelectListItem>();
            courseList.Add(new SelectListItem { Text = "Select a course", Value = null });
            courseList.AddRange(db.Courses.Select(c => new SelectListItem { Text = c.Course_Name, Value = c.Course_Id.ToString() }));
            ViewBag.dependent_Course = new SelectList(courseList, "Value", "Text"); return View(cours);
        }

        // POST: Cours1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Course_Id,Course_Name,Number_Of_Hours,Course_Description,Major_Id,syllabus,dependent_Course")] Cours cours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Major_Id = new SelectList(db.Majors, "Major_Id", "Major_Name", cours.Major_Id);
            ViewBag.dependent_Course = new SelectList(db.Courses, "Course_Id", "Course_Name", cours.dependent_Course);
            return View(cours);
        }

        // GET: Cours1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cours cours = db.Courses.Find(id);
            if (cours == null)
            {
                return HttpNotFound();
            }
            return View(cours);
        }

        // POST: Cours1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = db.Courses.Find(id);
            db.Courses.Remove(cours);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Student")]
        public ActionResult Registration()
        {
            //string userid = User.Identity.GetUserId();
            //var logstudent = db.AspNetUsers.Find(userid);
            //int combalance = 9 * Convert.ToInt32(logstudent.Major.Price);
            //Session["semesterid"] = 1;
            //int iid = Convert.ToInt32(Session["semesterid"]);
            //var dates1 = db.RegistrationPeriods.First(x => x.semester_id == iid);
            //if (DateTime.Now.Date <= dates1.start_date && DateTime.Now.Date >= dates1.end_date)
            //{
            //    string Error1 = $"You should be in the registration period";
            //    return RedirectToAction("Errors", new { Error = Error1 });
                
            //}
            //if (logstudent.Balance < combalance)
            //{
            //    string Error = $"You dont have the min balance which is {combalance}";
            //    return RedirectToAction("Errors", new { Error = Error });
            //}

            return View();
        }
     
        [HttpPost]
        public ActionResult Registration(int Course_id)
        {
            string userid = User.Identity.GetUserId();
            Session["semesterid"] = 1;
            int semesterid = Convert.ToInt32(Session["semesterid"]);
            var logstudent = db.AspNetUsers.Find(userid);
            int balancee = Convert.ToInt32( logstudent.Balance);
            var registerednow = db.Enrollments.Where(x => x.Student_id == userid && x.Is_Paid == false);
            var selectedcourse = db.Courses_Offered.Find(Course_id);
            if (selectedcourse == null)
            {
                TempData["swal_message"] = $"There is no Courses in the schedual with this id ";
                ViewBag.title = "Error";
                ViewBag.icon = "warning";
                return View();
            }
            int hourenumber = Convert.ToInt32(selectedcourse.Cours.Number_Of_Hours);
            int hourprice = Convert.ToInt32(logstudent.Major.Price);
            int combalance = 9 * Convert.ToInt32(logstudent.Major.Price);
            var coursesoffered = db.Courses_Offered.Where(x => x.semester_id == semesterid && x.Cours.Major_Id == logstudent.Major_Id);
            bool ex1 = true;
            foreach (var courses in coursesoffered)
            {
                if (courses.course_id == selectedcourse.Cours.Course_Id)
                {
                    ex1 = true;
                }
                if (!ex1)
                {
                    TempData["swal_message"] = $"This course belong to other major";
                    ViewBag.title = "Error";
                    ViewBag.icon = "warning";
                    return View();
                }
            }

            foreach (var item in registerednow)
            {
                if(item.Course_id==Course_id)
                {
                    TempData["swal_message"] = $"You already have this course ";
                    ViewBag.title = "Error";
                    ViewBag.icon = "warning";
                    return View();
                }
                if (selectedcourse.start_time < item.Courses_Offered.end_time && selectedcourse.end_time > item.Courses_Offered.start_time &&selectedcourse.Days_id==item.Courses_Offered.Days_id)
                {
                    TempData["swal_message"] = $"Partial overlapping occured between {selectedcourse.end_time} and {item.Courses_Offered.start_time } ";
                    ViewBag.title = "Error";
                    ViewBag.icon = "warning";
                    return View();
                }

                else if (selectedcourse.start_time == item.Courses_Offered.start_time || selectedcourse.end_time == item.Courses_Offered.end_time && selectedcourse.Days_id == item.Courses_Offered.Days_id)
                {
                    TempData["swal_message"] = $"Partial overlapping occured  ";
                    ViewBag.title = "Error";
                    ViewBag.icon = "warning";
                    return View();
                }


            }
           
            if (logstudent.Balance < hourenumber * hourprice)
            {
                TempData["swal_message"] = $"You dont have the enofh balance that is requiered for registration this course you should add   {hourenumber * hourprice - logstudent.Balance} ";
                ViewBag.title = "Error";
                ViewBag.icon = "warning";
                return View();
            }
          
          if(selectedcourse.Registered >= selectedcourse.Capacity)
            {
                TempData["swal_message"] = $"This course is full ";
                ViewBag.title = "Error";
                ViewBag.icon = "warning";
                return View();
            }

          bool ex = false;
            var allregisteredcourses = db.Enrollments.Where(x => x.Student_id == userid && x.Is_Paid==true);
            //Find();

            if (selectedcourse.Cours.dependent_Course != null)
            {
                var dependentcourse = db.Courses.Find(Convert.ToInt32( selectedcourse.Cours.dependent_Course));

                foreach (var item in allregisteredcourses)
            {
                if(item.Course_id==dependentcourse.Course_Id)
                    {
                        ex= true;
                    }
            }
                if (!ex)
                {
                    TempData["swal_message"] = $"you dont have the prerequirement which is {dependentcourse.Course_Name} ";
                    ViewBag.title = "Error";
                    ViewBag.icon = "warning";
                    return View();

                }
            }
            Enrollment addcourse = new Enrollment();
            addcourse.semester_id= Convert.ToInt32(Session["semesterid"]);
            addcourse.Course_id = Course_id;
            addcourse.Student_id = userid;
            addcourse.Is_Paid = false;
           
            db.Enrollments.Add(addcourse);
            db.SaveChanges();
            logstudent.Balance = balancee - hourenumber * hourprice;
            db.SaveChanges();
            TempData["swal_message"] = $"you dont have registered this course successfully ";
            ViewBag.title = "success";
            ViewBag.icon = "success";
            return View();
           
        }


        public ActionResult Errors(string Error)
        {
            if(Error == null)
            {
                ViewBag.message = "No Errors";
                return View();
            }
            ViewBag.message= Error;
            return View();
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
