using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyExams.Entities;
using MyExams.ViewModels;
using MyExams.Repositories;
using MyExams.Mappings;
using MyExams.Models;
using System.Threading.Tasks;
using PagedList;

namespace MyExams.Controllers
{
    public class StudentsController : Controller
    {
        private MyExamsEntities db = new MyExamsEntities();
        private StudentsRepository studentRepo = new StudentsRepository();

        private static string lastOrderField = "name_asc";


        // GET: Students
        public ActionResult Index(int? page,string sortOrder, string searchString)
        {

            ViewBag.SearchString = (string.IsNullOrEmpty(searchString)) ? "" : searchString;

            sortOrder = studentRepo.GetOrder(sortOrder, ref lastOrderField);

            var students = studentRepo.GetAll(sortOrder, searchString);

            var studentsVM = StudentMap.StudentsToStudentsVM(students);


            int pageSize = 10;
            int pageNumber = (page ?? 1);

            int noOfPage = (studentsVM.Count() / pageSize) + ((studentsVM.Count() % pageSize) == 0 ? 0 : 1);
            if (pageNumber > noOfPage)
            {
                pageNumber = 1;
            }

            return View(studentsVM.ToPagedList(pageNumber, pageSize));
        }



        // GET: Students/Details/5

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = studentRepo.GetById(id.Value);

            if (student == null)
            {
                return HttpNotFound();
            }

            var studentVM = StudentMap.StudentToStudentVM(student);

            return View(studentVM);
        }


        //GET : Students/ExamsOfStudent/5
        public ActionResult GetExamsOfStudent(int id)
        {
            List <ExamsOfStudent> examsOfStudent = studentRepo.GetExamsOfStudent(id);

            return PartialView("_ExamsOfStudent", examsOfStudent);
        
        }
       



        // GET: Students/Create
        public ActionResult Create()
        {
            var studentVM = new StudentVM();

            return View(studentVM);
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,CNP,Specialization,YearOfStudy")] StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                Student student = StudentMap.StudentVMToStudent(studentVM);

                studentRepo.Add(student);

                return RedirectToAction("Index");
            }

            return View(studentVM);
        }




        // GET: Students/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = studentRepo.GetById(id.Value);

            if (student == null)
            {
                return HttpNotFound();
            }

            var studentVM = StudentMap.StudentToStudentVM(student);

            return View(studentVM);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,CNP,Specialization,YearOfStudy")] StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                Student student = StudentMap.StudentVMToStudent(studentVM);

                studentRepo.Edit(student);

                return RedirectToAction("Index");
            }
            return View(studentVM);
        }




        // GET: Students/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Student student = studentRepo.GetById(id.Value);

            if (student == null)
            {
                return HttpNotFound();
            }

            var studentVM = StudentMap.StudentToStudentVM(student);

            return View(studentVM);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = studentRepo.GetById(id);
            try
            {
                studentRepo.Delete(student);
            }
            catch (Exception)
            {
                ViewBag.Message = "Student " + student.FirstName + " " + student.LastName + " cannot be deleted. He has related exams. Delete exams first.";
                ViewBag.Related = "Go to Exams";
                ViewBag.Controller = "Exams";
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
