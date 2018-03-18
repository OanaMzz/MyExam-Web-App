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
using MyExams.Mappings;
using MyExams.ViewModels;
using MyExams.Models;
using PagedList;


namespace MyExams.Controllers
{
    public class ExamsController : Controller
    {
        private MyExamsEntities db = new MyExamsEntities();
        private ExamsRepository examsRepo = new ExamsRepository();

        private static string lastOrderField = "name_asc";



        // GET: Exams
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {
            ViewBag.SearchString = (string.IsNullOrEmpty(searchString)) ? "" : searchString;
            sortOrder = examsRepo.GetOrder(sortOrder, ref lastOrderField);
            var exams = examsRepo.GetAll(sortOrder, searchString);

            var examsVM = ExamMap.ExamsToExamsVM(exams);



            int pageSize = 10;
            int pageNumber = (page ?? 1);
            int noOfPage = (examsVM.Count() / pageSize) + ((examsVM.Count() % pageSize) == 0 ? 0 : 1);
            if (pageNumber > noOfPage)
            {
                pageNumber = 1;
            }
            return View(examsVM.ToPagedList(pageNumber, pageSize));
        }




        // GET: Exams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Exam exam = examsRepo.GetById(id.Value);

            if (exam == null)
            {
                return HttpNotFound();
            }

            var examVM = ExamMap.ExamToExamVM(exam);

            examVM.StudentProfile = examsRepo.GetStudentProfile(examVM.ExamId);

            return View(examVM);
        }


        //GET : Exams/GetCourseDetails/5
        public ActionResult GetCourseDetails(int id)
        {
            var courseDetails = examsRepo.GetCourseDetails(id);

            return PartialView("_CourseDetails", courseDetails);
        }




        // GET: Exams/Create
        public ActionResult Create()
        {
            var examVM = new ExamVM();

            examVM.CourseIdSelectList = SelectListHelper.CreateCourseSelectList();

            examVM.StudentIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList());

            return View(examVM);
        }

        // POST: Exams/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamId,CourseId,StudentId,Date,Grade")] ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                Exam exam = ExamMap.ExamVMToExam(examVM);

                examsRepo.Add(exam);

                return RedirectToAction("Index");
            }

            examVM.CourseIdSelectList = (SelectListHelper.CreateCourseSelectList(examVM.CourseId));

            examVM.StudentIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList(), examVM.StudentId);

            return View(examVM);
        }




        // GET: Exams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Exam exam = examsRepo.GetById(id.Value);

            if (exam == null)
            {
                return HttpNotFound();
            }

            var examVM = ExamMap.ExamToExamVM(exam);

            examVM.CourseIdSelectList = (SelectListHelper.CreateCourseSelectList(examVM.CourseId));

            examVM.StudentIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList(), examVM.StudentId);

            return View(examVM);
        }

        // POST: Exams/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamId,CourseId,StudentId,Date,Grade")] ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                Exam exam = ExamMap.ExamVMToExam(examVM);

                examsRepo.Edit(exam);

                return RedirectToAction("Index");
            }

            examVM.CourseIdSelectList = (SelectListHelper.CreateCourseSelectList(examVM.CourseId));

            examVM.StudentIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList(), examVM.StudentId);

            return View(examVM);
        }




        // GET: Exams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Exam exam = examsRepo.GetById(id.Value);

            if (exam == null)
            {
                return HttpNotFound();
            }

            var examVM = ExamMap.ExamToExamVM(exam);

            examVM.Course = examsRepo.GetBasicCourseDetails(examVM.ExamId);

            examVM.Student = examsRepo.GetStudentDetails(examVM.ExamId);

            return View(examVM);
        }

        // POST: Exams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Exam exam = examsRepo.GetById(id);

            examsRepo.Delete(exam);

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
