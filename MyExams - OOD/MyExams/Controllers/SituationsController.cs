using System.Web.Mvc;
using MyExams.Contracts;
using MyExams.ViewModels;
using AutoMapper;
using MyExams.Dto;
using MyExams.CustomExFilter;

namespace MyExams.Controllers
{
    [HandleGeneral]
    public class SituationsController : Controller
    {
        private readonly IStudentsRepository _studentsRepo;

        private readonly IExamsRepository _examsRepo;

        private readonly IBussiness _businessRepo;


        public SituationsController(IStudentsRepository _studentsRepo, IBussiness _businessRepo, IExamsRepository _examsRepo)
        {
            this._studentsRepo = _studentsRepo;
            this._businessRepo = _businessRepo;
            this._examsRepo = _examsRepo;
        }



        //GET: Situations/Search
        public ActionResult Search()
        {
            var situationFromVM = new SituationFormVM();

            situationFromVM.StudentIdSelectList = _studentsRepo.GetSelectList();

            return View(situationFromVM);
        }

        //Ajax Call - DropDownList
        public ActionResult GetCurrentSpecializations()
        {
            var specializations= _studentsRepo.GetCurrentSpecializations();

            return Json(specializations, JsonRequestBehavior.AllowGet);
        }

        //Ajax Post - DropDownList
        [HttpPost]
        public ActionResult GetYearForSpecialization(int specialization)
        {
            var years = _studentsRepo.GetYearForSpecialization(specialization);

            return Json(years, JsonRequestBehavior.AllowGet);
        }

       


        //Ajax Post - Form(StudentSituation) 
        [HttpPost]
        public ActionResult GetStudentId (int studentId)
        {
            return Json(new { url = Url.Action("StudentSituation", new { id = studentId }) }, JsonRequestBehavior.AllowGet);
        }

        //GET: Situations/StudentSituation/id
        public ActionResult StudentSituation(int id)
        {
            var examsOfStudentDto = _examsRepo.GetRelatedByPerson(id);

            var studentInfoDto = _studentsRepo.GetById(id);


            var studentSituationDto = _businessRepo.GetStudentSituation(examsOfStudentDto, studentInfoDto);

            var studentSituationVM = Mapper.Map<StudentSituationDto, StudentSituationVM>(studentSituationDto);

            return View(studentSituationVM);
        }

      

        //Ajax Post - Form(PromotionSituation)
        [HttpPost]
        public ActionResult GetPromotionInfo(int specialization, int year)
        {
            return Json(new { url = Url.Action("PromotionSituation", new { specialization = specialization, year = year }) }, JsonRequestBehavior.AllowGet);
        }

        //GET: Situations/PromotionSituation/promoInfo
        public ActionResult PromotionSituation(int specialization, int year)
        {
            var examsOfStudents = _examsRepo.GetRelatedByPromotion(specialization, year);

            var promotionSituationDto = _businessRepo.GetPromotionSituation(examsOfStudents, specialization, year);


            var promoSituationVM = Mapper.Map<PromotionSituationDto, PromotionSituationVM>(promotionSituationDto);

            return View(promoSituationVM);
        }
     
    }


}