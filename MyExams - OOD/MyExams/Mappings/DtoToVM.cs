using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyExams.Dto;
using MyExams.ViewModels;
using MyExams.Infrastructure;

namespace MyExams.Mappings1
{
    public class DtoToVM : Profile
    {
        public DtoToVM()
        {
            CreateMap<StudentsDto, StudentVM>()
                .ForMember(dest => dest.CNP, opt => opt.MapFrom(src => src.CNP.ToString()))
                .ForMember(dest => dest.Specialization, opt => opt.MapFrom(src => (Specialization)src.Specialization))
                .ForMember(dest => dest.YearOfStudy, opt => opt.MapFrom(src => (YearOfStudy)src.YearOfStudy));

            CreateMap<TeacherDto, TeacherVM>()
                .ForMember(dest => dest.TeacherTitle, opt => opt.MapFrom(src => (Title)src.Title));

            CreateMap<CourseDetailsDto, CourseDetailsVM>()
                .ForMember(dest => dest.TeacherDetails, opt => opt.MapFrom(src => Mapper.Map<TeacherDto, TeacherVM>(src.TeacherDetails)));

            CreateMap<CourseDto, CourseVM>();

            CreateMap<ExamDetailsDto, ExamDetailsVM>()
                .ForMember(dest => dest.StudentDetails, opt => opt.MapFrom(src => Mapper.Map<StudentsDto, StudentVM>(src.StudentDetails)))
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => ConvertGradeDetailsVM(src.Grade)));

            CreateMap<ExamDto, ExamVM>()
                .ForMember(dest => dest.Grade, opt => opt.MapFrom(src => ConvertGradeVM(src.Grade)));

            CreateMap<ExamsOfStudentDto, ExamsOfStudentVM>()
                .ForMember(dest=> dest.Teacher, opt=> opt.MapFrom(src=> Mapper.Map<TeacherDto, TeacherVM>(src.Teacher)))
                .ForMember(dest=> dest.Grade, opt=> opt.MapFrom(src=> ConvertGradeDetailsVM(src.Grade)));

            CreateMap<StudentSituationDto, StudentSituationVM>()
                .ForMember(dest => dest.Student, opt => opt.MapFrom(src => Mapper.Map<StudentsDto, StudentVM>(src.Student)))
                .ForMember(dest=> dest.ExamsToRetake, opt=> opt.MapFrom(src=> ConvertExamsToRetake(src.ExamsToRetake)))
                .ForMember(dest=> dest.Average, opt=>opt.MapFrom(src=> ConvertAverage(src.ExamsCount, src.Average)))
                .ForMember(dest=> dest.Credits, opt=> opt.MapFrom(src=> ConvertCredits(src.ExamsCount, src.Credits)))
                .ForMember(dest=> dest.ExamTable, opt=>opt.MapFrom(src=> Mapper.Map<List<ExamsOfStudentDto>, List<ExamsOfStudentVM>>(src.ExamTable)));

            CreateMap<PromoStudentSituationDto, PromoStudentSituationVM>()
                .ForMember(dest=> dest.Student, opt=> opt.MapFrom(src=> Mapper.Map<StudentsDto, StudentVM>(src.Student)))
                .ForMember(dest=> dest.ExamsToRetakeCount, opt=> opt.MapFrom(src => ConvertExamsToRetakeCount(src.ExamsCount, src.ExamsToRetakeCount)))
                .ForMember(dest=> dest.Average, opt=> opt.MapFrom(src=> ConvertAverage(src.ExamsCount, src.Average)))
                .ForMember(dest=> dest.Credits, opt=> opt.MapFrom(src=> ConvertCredits(src.ExamsCount, src.Credits)));

            CreateMap<PromotionSituationDto, PromotionSituationVM>()
                .ForMember(dest=> dest.Specialization, opt=> opt.MapFrom(src=> (Specialization)src.Specialization))
                .ForMember(dest=> dest.YearOfStudy, opt=> opt.MapFrom(src=> (YearOfStudy)src.YearOfStudy))
                .ForMember(dest=> dest.PromotionAverage, opt=> opt.MapFrom(src=> CheckAverage(src.PromotionAverage)))
                .ForMember(dest=> dest.HighestAverage, opt=> opt.MapFrom(src=> CheckAverage(src.HighestAverage)))
                .ForMember(dest=> dest.StudentSituationTable, opt=> opt.MapFrom(src=> Mapper.Map<List<PromoStudentSituationDto>, List<PromoStudentSituationVM>>(src.StudentSituationTable)));
        }


        private string ConvertGradeDetailsVM(double? grade)
        {
            return (grade == null) ? "absent" : RoundNumber((double)grade).ToString("F2");
        }


        private string ConvertGradeVM(double? grade)
        {
            return (grade == null) ? grade.ToString() : RoundNumber((double)grade).ToString("F2");
        }


        private string ConvertExamsToRetakeCount(int count, int examsToRetake)
        {
            return (count == 0) ? "-" : examsToRetake.ToString();
        }


        private List<string> ConvertExamsToRetake(List<string> examsToRetake)
        {
            return (examsToRetake.Count() == 0) ? new List<string>() { "-" } : examsToRetake;
        }


        private string ConvertAverage(int count, double? avg)
        {
            return (count == 0) ? "-" : RoundNumber((double)avg).ToString("F2");
        }

        private string CheckAverage(double? avg)
        {
            return (avg == null) ? "-" : RoundNumber((double)avg).ToString("F2");
        }


        private string ConvertCredits(int count, int credits)
        {
            return (count == 0) ? "-" : credits.ToString();
        }


        private double RoundNumber(double number)
        {
            return Math.Round(number,2);
        }

    }
}