using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MyExams.Models;
using MyExams.ViewModels;
using MyExams.Repositories;
using MyExams.Enums;

namespace MyExams.Mappings
{
    public class SituationMap
    {

        // for Student
        public static StudentSituationVM GetStudentStatistics(int id)
        {
            StudentsRepository studentRepo = new StudentsRepository();

            var situationResult = new StudentSituationVM();

            situationResult.StudentName = studentRepo.GetStudentFullName(id);

            situationResult.StudentProfile = studentRepo.GetStudentProfile(id);

            situationResult.ExamsCount = studentRepo.GetTotalNumberOfExams(id);


            if (situationResult.ExamsCount > 0)
            {
                situationResult.Average = studentRepo.CalculateAvgerage(id).ToString("F2");

                situationResult.Credits = studentRepo.GetCreditsAcummulated(id).ToString();

                situationResult.ExamsToRetake = studentRepo.GetExamsToRetake(id);

                situationResult.ExamsToRetakeCount = situationResult.ExamsToRetake.Count();

                return situationResult;
            }


            situationResult.Average = "-";

            situationResult.Credits = "-";

            situationResult.ExamsToRetakeCount = 0;

            return situationResult;

        }


        // for Promotion
        public static PromotionSituation ConvertPromotionSituationVM(PromotionSituationFormVM promotionSituationVM)
        {
            var promotionSituation = new PromotionSituation();

            promotionSituation.Specialization = (int)promotionSituationVM.Specialization;

            promotionSituation.YearOfStudy = (int)promotionSituationVM.YearOfStudy;

            return promotionSituation;
        }



        public static List<PromotionSituationTableAux> GetPromotionTableAux(List<int> ids)
        {
            StudentsRepository studentRepo = new StudentsRepository();

            List<PromotionSituationTableAux> promotionResults = new List<PromotionSituationTableAux>();

            foreach (var id in ids)
            {
                PromotionSituationTableAux promotionStudentRecord = new PromotionSituationTableAux();

                promotionStudentRecord.StudentId = id;

                promotionStudentRecord.StudentName = studentRepo.GetStudentFullName(id);

                promotionStudentRecord.Average = studentRepo.CalculateAvgerage(id);

                promotionStudentRecord.Credits = studentRepo.GetCreditsAcummulated(id);

                promotionStudentRecord.ExamsCount = studentRepo.GetTotalNumberOfExams(id);

                promotionStudentRecord.ExamsToRetakeCount = studentRepo.GetExamsToRetake(id).Count();

                promotionResults.Add(promotionStudentRecord);
            }

            return promotionResults;
        }


        public static List<PromotionSituationTable> GetPromotionTable(List<PromotionSituationTableAux> auxTable)
        {
            List<PromotionSituationTable> promotionsTable = new List<PromotionSituationTable>();

            foreach (var item in auxTable)
            {
                var promotionTableRecord = new PromotionSituationTable();

                promotionTableRecord.StudentId = item.StudentId;

                promotionTableRecord.StudentName = item.StudentName.StudentFullName;

                promotionTableRecord.ExamsCount = item.ExamsCount;


                if (promotionTableRecord.ExamsCount > 0)
                {
                    promotionTableRecord.Average = item.Average.ToString("F2");

                    promotionTableRecord.Credits = item.Credits.ToString();

                    promotionTableRecord.ExamsToRetakeCount = item.ExamsToRetakeCount.ToString();

                    promotionsTable.Add(promotionTableRecord);
                }
                else
                {
                    promotionTableRecord.Average = "-";

                    promotionTableRecord.Credits = "-";

                    promotionTableRecord.ExamsToRetakeCount = "-";

                    promotionsTable.Add(promotionTableRecord);
                }

            }
            return promotionsTable;
        }



        public static PromotionSituationVM GetPromotionStatistics(List<PromotionSituationTableAux> auxTable, PromotionSituation promotionSituation)
        {
            StudentsRepository studentsRepo = new StudentsRepository();

            var finalsituation = new PromotionSituationVM();

            var promotionsTable = GetPromotionTable(auxTable);

            finalsituation.PromotionSituationTable = promotionsTable.OrderByDescending(p => p.StudentName).ToList();

            finalsituation.PromotionAverage = studentsRepo.GetPromotionAverage(auxTable).ToString("F2");

            finalsituation.HighestAverage = studentsRepo.GetHighestAverage(auxTable).ToString("F2");

            finalsituation.Scholarships = studentsRepo.GetScholarships(auxTable);

            finalsituation.Specialization = (Specialization)promotionSituation.Specialization;

            finalsituation.YearOfStudy = (YearOfStudy)promotionSituation.YearOfStudy;

            return finalsituation;
        }


    }

}