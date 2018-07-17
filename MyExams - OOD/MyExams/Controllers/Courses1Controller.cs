using System.Collections.Generic;
using System.Net;
using System.Web.Mvc;
using MyExams.Contracts;
using MyExams.Dto;
using MyExams.Models;
using MyExams.ViewModels;
using MyExams.CustomExFilter;
using AutoMapper;
using PagedList;

namespace MyExams.Controllers
{
    [HandleGeneral]
    public class Courses1Controller : Controller
    {

        private readonly ICoursesRepository _coursesRepo;

        private readonly ITeachersRepository _teachersRepo;

        private readonly IExamsRepository _examsRepo;

        private static string lastOrderField = "cnam_asc";


        public Courses1Controller(ICoursesRepository _coursesRepo, ITeachersRepository _teachersRepo, IExamsRepository _examsRepo)
        {
            this._coursesRepo = _coursesRepo;
            this._teachersRepo = _teachersRepo;
            this._examsRepo = _examsRepo;
        }
       

        // GET: Courses1
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {

            ViewBag.SearchString = _coursesRepo.GetSearchString(searchString);

            sortOrder = _coursesRepo.GetOrder(sortOrder, ref lastOrderField);


            var coursesDto = _coursesRepo.GetSearch(sortOrder, searchString);

            var coursesVM = Mapper.Map<List<CourseDetailsDto>, List<CourseDetailsVM>>(coursesDto);

            _coursesRepo.GetPagedListParameters(page, coursesDto, out int pageSize, out int pageNumber);

            
            return View(coursesVM.ToPagedList(pageNumber, pageSize));
        }


        // GET: Courses1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseDto = _coursesRepo.GetDetailsById(id.Value);
            
            if (courseDto == null)
            {
                return HttpNotFound();
            }

            var courseVM = Mapper.Map<CourseDetailsDto, CourseDetailsVM>(courseDto);

            return View(courseVM);
        }


        public ActionResult GetExamsForCourse(int id)
        {
            var examsForCourseDto = _examsRepo.GetRelatedByCourse(id);

            var examsForCourseVM = Mapper.Map<List<ExamDetailsDto>, List<ExamDetailsVM>>(examsForCourseDto);

            return PartialView("_ExamsForCourse", examsForCourseVM);
        }


        // GET: Courses1/Create
        public ActionResult Create()
        {

            var courseVM = new CourseVM();

            courseVM.TeacherIdSelectList = _teachersRepo.GetSelectList();

            return View(courseVM);
        }

        // POST: Courses1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CourseId,Name,TeacherId,Duration,Credits")] CourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseVM, CourseDto>(courseVM);

                _coursesRepo.Add(courseDto);

                return RedirectToAction("Index");
            }

            courseVM.TeacherIdSelectList = _teachersRepo.GetSelectList(courseVM.TeacherId);

            return View(courseVM);
        }

        // GET: Courses1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseDto = _coursesRepo.GetById(id.Value);

            if (courseDto == null)
            {
                return HttpNotFound();
            }

            var courseVM = Mapper.Map<CourseDto, CourseVM>(courseDto);

            courseVM.TeacherIdSelectList = _teachersRepo.GetSelectList(courseVM.TeacherId);

            return View(courseVM);
        }

        // POST: Courses1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CourseId,Name,TeacherId,Duration,Credits")] CourseVM courseVM)
        {
            if (ModelState.IsValid)
            {
                var courseDto = Mapper.Map<CourseVM, CourseDto>(courseVM);

                _coursesRepo.Edit(courseDto);

                return RedirectToAction("Index");
            }

            courseVM.TeacherIdSelectList = _teachersRepo.GetSelectList(courseVM.TeacherId);

            return View(courseVM);
        }

        // GET: Courses1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var courseDto = _coursesRepo.GetDetailsById(id.Value);

            if (courseDto == null)
            {
                return HttpNotFound();
            }

            var courseVM = Mapper.Map<CourseDetailsDto, CourseDetailsVM>(courseDto);

            return View(courseVM);
        }

        // POST: Courses1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var courseDto = _coursesRepo.GetById(id);

            try
            {
                _coursesRepo.Delete(courseDto);

                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return View("DeleteError", new ErrorObject("Course", "Exams", "Exams1"));
            }

        }


    }
}
