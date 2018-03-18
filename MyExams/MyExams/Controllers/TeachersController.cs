using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyExams.Entities;
using MyExams.Repositories;
using MyExams.Models;
using MyExams.Mappings;
using MyExams.ViewModels;

namespace MyExams.Controllers
{
    public class TeachersController : Controller
    {
        private MyExamsEntities db = new MyExamsEntities();
        private TeachersRepository teachersRepo = new TeachersRepository();


        // GET: Teachers
        public ActionResult Index()
        {
            var teachers = teachersRepo.GetAll();

            var teachersVM = TeacherMap.TeachersToTeachersVM(teachers);

            return View(teachersVM);
        }



        // GET: Teachers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = teachersRepo.GetById(id.Value);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            var teacherVM = TeacherMap.TeacherToTeacherVM(teacher);

            return View(teacherVM);
        }


        //GET : Teachers/GetCoursesOfTeacher/5
        public ActionResult GetCoursesOfTeacher(int id)
        {
            List<CoursesOfTeacher> coursesOfTeacher = teachersRepo.GetCoursesOfTeacher(id);

            return PartialView("_CoursesOfTeacher", coursesOfTeacher);
        }




        // GET: Teachers/Create
        public ActionResult Create()
        {
            var teacherVM = new TeacherVM();

            return View(teacherVM);
        }

        // POST: Teachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,FirstName,LastName,TeacherTitle")] TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = TeacherMap.TeacherVMToTeacher(teacherVM);

                teachersRepo.Add(teacher);

                return RedirectToAction("Index");
            }

            return View(teacherVM);
        }



        // GET: Teachers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = teachersRepo.GetById(id.Value);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            var teacherVM = TeacherMap.TeacherToTeacherVM(teacher);

            return View(teacherVM);
        }

        // POST: Teachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,FirstName,LastName,TeacherTitle")] TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                Teacher teacher = TeacherMap.TeacherVMToTeacher(teacherVM);

                teachersRepo.Edit(teacher);

                return RedirectToAction("Index");
            }
            return View(teacherVM);
        }




        // GET: Teachers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Teacher teacher = teachersRepo.GetById(id.Value);

            if (teacher == null)
            {
                return HttpNotFound();
            }

            var teacherVM = TeacherMap.TeacherToTeacherVM(teacher);

            return View(teacherVM);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = teachersRepo.GetById(id);
            try
            {
                teachersRepo.Delete(teacher);
            }
            catch (Exception)
            {
                ViewBag.Message = "Teacher " + teacher.FirstName + " " + teacher.LastName + " cannot be deleted. He has related courses. Delete courses first.";
                ViewBag.Related = "Go to Courses";
                ViewBag.Controller = "Courses";
                return View("DeleteError");
            }
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
