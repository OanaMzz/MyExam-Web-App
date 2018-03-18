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
using MyExams.Models;
using MyExams.ViewModels;
using PagedList;

namespace MyExams.Controllers
{
    public class CoursesController : Controller
    {
        private MyExamsEntities db = new MyExamsEntities();
        private CoursesRepository coursesRepo = new CoursesRepository();

        private static string lastOrderField = "name_asc";


        // GET: Courses
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {

            ViewBag.SearchString = (string.IsNullOrEmpty(searchString)) ? "" : searchString;

            sortOrder = coursesRepo.GetOrder(sortOrder, ref lastOrderField);

            var courses = coursesRepo.GetAll(sortOrder, searchString);

            var coursesVM = CourseMap.CoursesToCoursesVM(courses);

            int pageSize = 9;
            int pageNumber = (page ?? 1);
            int noOfPage = (coursesVM.Count() / pageSize) + ((coursesVM.Count() % pageSize) == 0 ? 0 : 1);
            if (pageNumber > noOfPage)
            {
                pageNumber = 1;
            }
            return View(coursesVM.ToPagedList(pageNumber, pageSize));
        }



        // GET: Courses/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cours cours = coursesRepo.GetById(id.Value);

            if (cours == null)
            {
                return HttpNotFound();
            }

            var courseVM = CourseMap.CourseToCourseVM(cours);

            return View(courseVM);
        }


        //GET : Courses/GetExamsForCourse/5
        public ActionResult GetExamsForCourse(int id)
        {
            List<ExamsForCourse> examsForCourse = coursesRepo.GetExamsForCourse(id);

            return PartialView("_ExamsForCourse", examsForCourse);
        }




        // GET: Courses/Create
        public ActionResult Create()
        {
            var courseVM = new CourseVM();

            courseVM.TeacherIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateTeacherSelectList());

            return View(courseVM);
        }


        // POST: Courses/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Name,TeacherId,Duration,Credits")] CourseVM coursVM)
        {
            if (ModelState.IsValid)
            {
                Cours course = CourseMap.CourseVMToCourse(coursVM);

                coursesRepo.Add(course);

                return RedirectToAction("Index");
            }

            coursVM.TeacherIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateTeacherSelectList(), coursVM.TeacherId);

            return View(coursVM);
        }



        // GET: Courses/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cours cours = coursesRepo.GetById(id.Value);

            if (cours == null)
            {
                return HttpNotFound();
            }

            var courseVM = CourseMap.CourseToCourseVM(cours);

            courseVM.TeacherIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateTeacherSelectList(), courseVM.TeacherId);

            return View(courseVM);
        }

        // POST: Courses/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Name,TeacherId,Duration,Credits")] CourseVM coursVM)
        {
            if (ModelState.IsValid)
            {
                Cours course = CourseMap.CourseVMToCourse(coursVM);

                coursesRepo.Edit(course);

                return RedirectToAction("Index");
            }

            coursVM.TeacherIdSelectList = SelectListHelper.CreateSelectListProp(SelectListHelper.PopulateTeacherSelectList(), coursVM.TeacherId);

            return View(coursVM);
        }



        // GET: Courses/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cours cours = coursesRepo.GetById(id.Value);

            if (cours == null)
            {
                return HttpNotFound();
            }

            var courseVM = CourseMap.CourseToCourseVM(cours);

            return View(courseVM);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cours cours = coursesRepo.GetById(id);
            try
            {
                coursesRepo.Delete(cours);
            }
            catch (Exception)
            {
                ViewBag.Message = "The course " + cours.Name + " cannot be deleted. It has related exams. Delete exams first.";
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
