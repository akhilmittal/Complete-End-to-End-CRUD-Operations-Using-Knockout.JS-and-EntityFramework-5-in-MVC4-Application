using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace KnockoutWithMVC4.Controllers
{
    public class StudentController : Controller
    {
        private LearningKOEntities db = new LearningKOEntities();

        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        /// <summary>
        /// Fetches all Students and return the jason format.
        /// </summary>
        /// <returns></returns>
        public JsonResult FetchStudents()
        {
            return Json(db.Students.ToList(), JsonRequestBehavior.AllowGet);
        }
       
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public string Create(Student student)
        {
            if (ModelState.IsValid)
            {
                if (!StudentExists(student))
                    db.Students.Add(student);
                else
                    return Edit(student);
                db.SaveChanges();
                return "Student Created";
            }
            return "Creation Failed";
        }

        public ActionResult Edit(string id=null)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(student); 
            return View();
        }

        /// <summary>
        /// Edits particular student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public string Edit(Student student)
        {
            if (ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return "Student Edited";
            }
            return "Edit Failed";
        }
       
        public ActionResult Delete(string id = null)
        {
            Student student = db.Students.Find(id);
            if (student == null)
            {
                return null;
            }
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            ViewBag.InitialData = serializer.Serialize(student);
            return View();
        }

        /// <summary>
        /// Delete particular student details
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        [HttpPost]
        public string Delete(Student student)
        {
            Student studentDetail = db.Students.Find(student.StudentId);
            db.Students.Remove(studentDetail);
            db.SaveChanges();
            return "Student Deleted";
        }

        /// <summary>
        /// Checks if student exists
        /// </summary>
        /// <param name="student"></param>
        /// <returns></returns>
        private bool StudentExists(Student student)
        {
            Student foundStudent = db.Students.Find(student.StudentId);
            if (foundStudent != null && !String.IsNullOrEmpty(foundStudent.StudentId))
                return true;
            return false;
        }

        /// <summary>
        /// Dispose dbContext
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}