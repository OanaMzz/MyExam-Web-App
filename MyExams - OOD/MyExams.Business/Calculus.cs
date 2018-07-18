using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyExams.Contracts;
using MyExams.Infrastructure;
using System.Web.Mvc;
using MyExams.Dto;

namespace MyExams.Business
{
    public class Calculus : IBussiness
    {

        public StudentSituationDto GetStudentSituation(List<ExamsOfStudentDto> relatedExams, StudentsDto studentInfo)
        {
            return new StudentSituationDto
            {
                Student = studentInfo,
                ExamsCount = GetExamsCount(relatedExams),
                ExamsToRetakeCount = GetExamsToRetakeCount(relatedExams),
                ExamsToRetake = GetExamsToRetake(relatedExams),
                Average = GetAverage(relatedExams),
                Credits = GetCredits(relatedExams),
                ExamTable = relatedExams
            };
        }

        public PromotionSituationDto GetPromotionSituation(Dictionary<StudentsDto, List<ExamsOfStudentDto>> situationPerStudent, int specialization, int year)
        {
            var promoStudentSituations = GetPromoStudentSituations(situationPerStudent);

            return new PromotionSituationDto
            {
                Specialization = specialization,
                YearOfStudy = year,
                PromotionAverage = GetAverage(promoStudentSituations),
                HighestAverage = GetHighestAverage(promoStudentSituations),
                Scholarships = GetScholarships(promoStudentSituations),
                StudentSituationTable = promoStudentSituations
            };
        }


        //StudentSituation Calculations
        private int GetExamsCount(List<ExamsOfStudentDto> relatedExams)
        {
            return relatedExams.Count();
        }


        private int GetExamsToRetakeCount(List<ExamsOfStudentDto> relatedExams)
        {
            return relatedExams.Where(e => ((e.Grade == null) || (e.Grade < 4.5))).Count();
        }


        private List<string> GetExamsToRetake(List<ExamsOfStudentDto> relatedExams)
        {
            return relatedExams.Where(e => ((e.Grade == null) || (e.Grade < 4.5))).OrderBy(e=> e.Name).Select(e => e.Name).ToList();
        }


        private double GetAverage(List<ExamsOfStudentDto> relatedExams)
        {
            var avg = relatedExams.Average(e => e.Grade);

            return (avg == null) ? 0 : RoundNumber((double)avg);

        }


        private int GetCredits(List<ExamsOfStudentDto> relatedExams)
        {
            return relatedExams.Where(e => ((e.Grade != null) && (e.Grade >= 4.5))).Sum(e => e.Credits);
        }


        private double RoundNumber(double number)
        {
            return Math.Round(number, 2);
        }



        //Promotion Situation calculations
        private List<PromoStudentSituationDto> GetPromoStudentSituations(Dictionary<StudentsDto, List<ExamsOfStudentDto>> situationPerStudent)
        {
            var result = new List<PromoStudentSituationDto>();

            foreach (var pair in situationPerStudent)
            {
                result.Add(new PromoStudentSituationDto
                {
                    Student = pair.Key,
                    ExamsCount = GetExamsCount(pair.Value),
                    ExamsToRetakeCount = GetExamsToRetakeCount(pair.Value),
                    Average = GetAverage(pair.Value),
                    Credits = GetCredits(pair.Value)
                }
                );
            }
            return result;
        }

        private double? GetAverage(List<PromoStudentSituationDto> promoStudentSituations)
        {
            var avg = promoStudentSituations.Where(s => (s.ExamsCount != 0)).Select(s=> s.Average).ToList();

            return (avg.Count==0)? (double?)null : avg.Average();
        }


        private double? GetHighestAverage(List<PromoStudentSituationDto> promoStudentSituations)
        {
            var highestAvg = promoStudentSituations.Where(s => (s.ExamsCount != 0)).Select(e=> e.Average).ToList();

            return (highestAvg.Count==0) ? (double?)null : highestAvg.Max();
        }


        private List<string> GetScholarships(List<PromoStudentSituationDto> promoStudentSituations)
        {
            var scholarships =  promoStudentSituations.Where(s => ((s.Average != 0) && (s.ExamsCount != 0) && (s.ExamsToRetakeCount == 0)))
                               .OrderByDescending(s => s.Average).Select(s => s.Student.FirstName + " " + s.Student.LastName).ToList();

            return CheckScholarships(scholarships);
        }


        private List<string> CheckScholarships(List<string> scholarships)
        {
            if (scholarships.Count() <= 3)
            {
                return scholarships;
            }

            scholarships.RemoveRange(3, scholarships.Count() - 3);
            return scholarships;
        }
    }
}
