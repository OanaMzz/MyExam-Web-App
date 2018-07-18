using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MyExams.Data;
using MyExams.CustomExFilter;
using MyExams.Dto;
using MyExams.ViewModels;
using MyExams.Contracts;
using AutoMapper;
using PagedList;


namespace MyExams.Controllers
{
    [HandleGeneral]
    public class Exams1Controller : Controller
    {
        private readonly IExamsRepository _examsRepo;

        private readonly IStudentsRepository _studentsRepo;

        private readonly ICoursesRepository _coursesRepo;

        private static string lastOrderField = "date_asc";


        public Exams1Controller(IExamsRepository _examsRepo, IStudentsRepository _studentsRepo, ICoursesRepository _coursesRepo)
        {
            this._examsRepo = _examsRepo;
            this._studentsRepo = _studentsRepo;
            this._coursesRepo = _coursesRepo;
        }


        // GET: Exams1
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {

            ViewBag.SearchString = _examsRepo.GetSearchString(searchString);

            sortOrder = _examsRepo.GetOrder(sortOrder, ref lastOrderField);


            var examsDto = _examsRepo.GetSearch(sortOrder, searchString);

            var examsVM = Mapper.Map<List<ExamDetailsDto>, List<ExamDetailsVM>>(examsDto);

            _examsRepo.GetPagedListParameters(page, examsDto, out int pageSize, out int pageNumber);


            return View(examsVM.ToPagedList(pageNumber, pageSize)); 
        }

        // GET: Exams1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var examsDto = _examsRepo.GetDetailsById(id.Value);
            
            if (examsDto == null)
            {
                return HttpNotFound();
            }

            var examsVM = Mapper.Map<ExamDetailsDto, ExamDetailsVM>(examsDto);

            return View(examsVM);
        }

        // GET: Exams1/Create
        public ActionResult Create()
        {
            var examVM = new ExamVM();

            examVM.CourseIdSelectList = _coursesRepo.GetSelectList();

            examVM.StudentIdSelectList = _studentsRepo.GetSelectList();

            return View(examVM);
        }

        // POST: Exams1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ExamId,CourseId,StudentId,Date,Grade")] ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                var examDto = Mapper.Map<ExamVM, ExamDto>(examVM);

                _examsRepo.Add(examDto);

                return RedirectToAction("Index");
            }

            examVM.CourseIdSelectList = _coursesRepo.GetSelectList(examVM.CourseId);

            examVM.StudentIdSelectList = _studentsRepo.GetSelectList(examVM.StudentId);

            return View(examVM);
        }

        // GET: Exams1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var examDto = _examsRepo.GetById(id.Value);

            if (examDto == null)
            {
                return HttpNotFound();
            }

            var examVM = Mapper.Map<ExamDto, ExamVM>(examDto);

            examVM.CourseIdSelectList = _coursesRepo.GetSelectList(examVM.CourseId);

            examVM.StudentIdSelectList = _studentsRepo.GetSelectList(examVM.StudentId);
           
            return View(examVM);
        }

        // POST: Exams1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ExamId,CourseId,StudentId,Date,Grade")] ExamVM examVM)
        {
            if (ModelState.IsValid)
            {
                var examDto = Mapper.Map<ExamVM, ExamDto>(examVM);

                _examsRepo.Edit(examDto);

                return RedirectToAction("Index");
            }

            examVM.CourseIdSelectList = _coursesRepo.GetSelectList(examVM.CourseId);

            examVM.StudentIdSelectList = _studentsRepo.GetSelectList(examVM.StudentId);

            return View(examVM);
        }

        // GET: Exams1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var examDto = _examsRepo.GetDetailsById(id.Value);
           
            if (examDto == null)
            {
                return HttpNotFound();
            }

            var examVM = Mapper.Map<ExamDetailsDto, ExamDetailsVM>(examDto);

            return View(examVM);
        }

        // POST: Exams1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            var examDto = _examsRepo.GetById(id);

            _examsRepo.Delete(examDto);
            
            return RedirectToAction("Index");
        }
    }
}
