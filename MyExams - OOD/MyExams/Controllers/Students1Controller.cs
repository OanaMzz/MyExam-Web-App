using System.Collections.Generic;
using System.Web.Mvc;
using MyExams.Dto;
using MyExams.Models;
using MyExams.Contracts;
using AutoMapper;
using MyExams.ViewModels;
using MyExams.CustomExFilter;
using PagedList;
using System.Net;

namespace MyExams.Controllers
{
    [HandleGeneral]
    public class Students1Controller : Controller
    {
        private readonly IStudentsRepository _studentsRepo;

        private readonly IExamsRepository _examsRepo;

        private static string lastOrderField = "name_asc";



        public Students1Controller(IStudentsRepository _studentsRepo, IExamsRepository _examsRepo)
        {
            this._studentsRepo = _studentsRepo;
            this._examsRepo = _examsRepo;
        }


        // GET: Students1
        public ActionResult Index(int? page, string sortOrder, string searchString)
        {

            ViewBag.SearchString = _studentsRepo.GetSearchString(searchString);

            sortOrder = _studentsRepo.GetOrder(sortOrder, ref lastOrderField);


            var studentsDto = _studentsRepo.GetSearch(sortOrder, searchString);

            var studentsVM = Mapper.Map<List<StudentsDto>, List<StudentVM>>(studentsDto);

            _studentsRepo.GetPagedListParameters(page, studentsDto, out int pageSize, out int pageNumber);


            return View(studentsVM.ToPagedList(pageNumber, pageSize));
        }


        // GET: Students1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentDto = _studentsRepo.GetById(id.Value);

            if (studentDto == null)
            {
                return HttpNotFound();
            }

            var studentVM = Mapper.Map<StudentsDto, StudentVM>(studentDto);

            return View(studentVM);
        }


        public ActionResult GetExamsOfStudent(int id)
        {
            var examsOfStudentDto = _examsRepo.GetRelatedByPerson(id);

            var examsOfStudentVM = Mapper.Map<List<ExamsOfStudentDto>, List<ExamsOfStudentVM>>(examsOfStudentDto);

            return PartialView("_ExamsOfStudent", examsOfStudentVM);
        }



        // GET: Students1/Create
        public ActionResult Create()
        {
            var studentVM = new StudentVM();

            return View(studentVM);
        }

        // POST: Students1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "StudentId,FirstName,LastName,CNP,Specialization,YearOfStudy")] StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                var studentDto = Mapper.Map<StudentVM, StudentsDto>(studentVM);

                _studentsRepo.Add(studentDto);

                return RedirectToAction("Index");
            }

            return View(studentVM);
        }

        // GET: Students1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentDto = _studentsRepo.GetById(id.Value);

            if (studentDto == null)
            {
                return HttpNotFound();
            }

            var studentVM = Mapper.Map<StudentsDto, StudentVM>(studentDto);

            return View(studentVM);
        }

        // POST: Students1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StudentId,FirstName,LastName,CNP,Specialization,YearOfStudy")] StudentVM studentVM)
        {
            if (ModelState.IsValid)
            {
                var studentDto = Mapper.Map<StudentVM, StudentsDto>(studentVM);

                _studentsRepo.Edit(studentDto);

                return RedirectToAction("Index");
            }
            return View(studentVM);
        }

        // GET: Students1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var studentDto = _studentsRepo.GetById(id.Value);

            if (studentDto == null)
            {
                return HttpNotFound();
            }

            var studentVM = Mapper.Map<StudentsDto, StudentVM>(studentDto);

            return View(studentVM);
        }

        // POST: Students1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {

            var studentDto = _studentsRepo.GetById(id);

            try
            {
                _studentsRepo.Delete(studentDto);

                return RedirectToAction("Index");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateException)
            {
                return View("DeleteError", new ErrorObject("Student", "Exams", "Exams1"));
            }

        }

    }
}
