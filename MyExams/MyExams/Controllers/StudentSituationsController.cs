using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyExams.ViewModels;
using MyExams.Models;
using MyExams.Repositories;
using MyExams.Mappings;

namespace MyExams.Controllers
{
    public class StudentSituationsController : Controller
    {
        private StudentsRepository studentsRepo = new StudentsRepository();

        // GET: StudentSituations/Search
        public ActionResult Search()
        {
            var situationVM = new SituationVM
            { StudentSituation = new StudentSituationFormVM(), PromotionStituation = new PromotionSituationFormVM() };

            situationVM.StudentSituation.StudentIdSelectList = SelectListHelper
                .CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList());

            return View(situationVM);
        }


        // POST: StudentSituations/Search
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search([Bind(Include = "StudentSituation")] SituationVM situationVM)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("Situation", new { id = situationVM.StudentSituation.StudentId });
            }

            situationVM.StudentSituation.StudentIdSelectList = SelectListHelper
                .CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList(), situationVM.StudentSituation.StudentId);

            return View(situationVM);
        }


        //POST : StudentSituations/Search2
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Search2([Bind(Include = "PromotionStituation", Exclude = "StudentSituation")] SituationVM situationVM)
        {

            situationVM.StudentSituation = new StudentSituationFormVM();
            situationVM.StudentSituation.StudentIdSelectList = SelectListHelper
                .CreateSelectListProp(SelectListHelper.PopulateStudentsSelectList());


            if (ModelState.IsValid)
            {
                var promotionSituation = SituationMap.ConvertPromotionSituationVM(situationVM.PromotionStituation);

                if (studentsRepo.PromotionExists(promotionSituation))
                {
                    List<int> studentIDs = studentsRepo.GetPromotionIDs(promotionSituation);

                    var finalSituation = SituationMap
                        .GetPromotionStatistics(SituationMap.GetPromotionTableAux(studentIDs), promotionSituation);

                    return View("Situation2", finalSituation);
                }


                ModelState.AddModelError(string.Empty, "The promotion you selected does not exist.Please try again.");

                return View("Search", situationVM);
            }


            return View("Search", situationVM);
        }

        // GET: StudentSituations/Situation/5
        public ActionResult Situation(int id)
        {
            return View(id);
        }


        // GET : Students/GetTable/5
        public ActionResult GetTable(int id)
        {
            List<ExamsOfStudent> situationTable = studentsRepo.GetExamsOfStudent(id);

            return PartialView("_ExamsOfStudent", situationTable);

        }

        // GET : Students/GetSituationResult/5
        public ActionResult GetSituationResult(int id)
        {
            var situationResult = SituationMap.GetStudentStatistics(id);

            return PartialView("_SituationResults", situationResult);
        }
    }
}