using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyExams.Data;
using MyExams.Dto;
using MyExams.Models;
using MyExams.ViewModels;
using MyExams.Contracts;
using MyExams.CustomExFilter;
using AutoMapper;
using PagedList;


namespace MyExams.Controllers
{
    [HandleGeneral]
    public class Teachers1Controller : Controller
    {

        private readonly ITeachersRepository _teachersRepo;

        private readonly ICoursesRepository _coursesRepo;

        private static string lastOrderField = "tnam_asc";



        public Teachers1Controller(ITeachersRepository _teachersRepo, ICoursesRepository _coursesRepo)
        {
            this._teachersRepo = _teachersRepo;
            this._coursesRepo = _coursesRepo;
        }


        // GET: Teachers1
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {

            ViewBag.SearchString = _teachersRepo.GetSearchString(searchString);

            sortOrder = _teachersRepo.GetOrder(sortOrder, ref lastOrderField);


            var teachersDto = _teachersRepo.GetSearch(sortOrder, searchString);

            var teachersVM = Mapper.Map<List<TeacherDto>, List<TeacherVM>>(teachersDto);

            _teachersRepo.GetPagedListParameters(page, teachersDto, out int pageSize, out int pageNumber);


            return View(teachersVM.ToPagedList(pageNumber, pageSize));
        }


        // GET: Teachers1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherDto = _teachersRepo.GetById(id.Value);

            if (teacherDto == null)
            {
                return HttpNotFound();
            }

            var teacherVM = Mapper.Map<TeacherDto, TeacherVM>(teacherDto);

            return View(teacherVM);
        }


        public ActionResult GetCoursesOfTeacher(int id)
        {
            var coursesOfTeacherDto = _coursesRepo.GetRelatedByPerson(id);

            var coursesOfTeacherVM = Mapper.Map<List<CourseDto>, List<CourseVM>>(coursesOfTeacherDto);

            return PartialView("_CoursesOfTeacher", coursesOfTeacherVM);
        }


        // GET: Teachers1/Create
        public ActionResult Create()
        {
            var teacherVM = new TeacherVM();

            return View(teacherVM);
        }

        // POST: Teachers1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "TeacherId,FirstName,LastName,TeacherTitle")] TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                var teacherDto = Mapper.Map<TeacherVM, TeacherDto>(teacherVM);

                _teachersRepo.Add(teacherDto);

                return RedirectToAction("Index");
            }

            return View(teacherVM);
        }

        // GET: Teachers1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherDto = _teachersRepo.GetById(id.Value);

            if (teacherDto == null)
            {
                return HttpNotFound();
            }

            var teacherVM = Mapper.Map<TeacherDto, TeacherVM>(teacherDto);

            return View(teacherVM);
        }

        // POST: Teachers1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "TeacherId,FirstName,LastName,TeacherTitle")] TeacherVM teacherVM)
        {
            if (ModelState.IsValid)
            {
                var teacherDto = Mapper.Map<TeacherVM, TeacherDto>(teacherVM);

                _teachersRepo.Edit(teacherDto);

                return RedirectToAction("Index");
            }
            return View(teacherVM);
        }

        // GET: Teachers1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var teacherDto = _teachersRepo.GetById(id.Value); 

            if (teacherDto == null)
            {
                return HttpNotFound();
            }

            var teacherVM = Mapper.Map<TeacherDto, TeacherVM>(teacherDto);

            return View(teacherVM);
        }

        // POST: Teachers1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var teacherDto = _teachersRepo.GetById(id);

            try
            {
                _teachersRepo.Delete(teacherDto);

                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return View("DeleteError", new ErrorObject("Teacher", "Courses", "Courses1"));
            }
        }

    }
}
