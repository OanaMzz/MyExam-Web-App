using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using MyExams.Dto;
using MyExams.ViewModels;
using System.Globalization;

namespace MyExams.Mappings1
{
    public class VMToDto : Profile
    {
        public VMToDto()
        {
            CreateMap<StudentVM, StudentsDto>()
                .ForMember(dest=> dest.FirstName, opt=> opt.MapFrom(src=> CultureInfo.CurrentCulture.TextInfo.ToTitleCase(src.FirstName.TrimStart().TrimEnd())))
                .ForMember(dest=> dest.LastName, opt=> opt.MapFrom(src=> CultureInfo.InvariantCulture.TextInfo.ToTitleCase(src.LastName.TrimStart().TrimEnd())))
                .ForMember(dest=> dest.CNP, opt=>opt.MapFrom(src=> long.Parse(src.CNP)))
                .ForMember(dest=> dest.Specialization, opt=> opt.MapFrom(src=> (int)src.Specialization))
                .ForMember(dest=> dest.YearOfStudy, opt=>opt.MapFrom(src=> (int)src.YearOfStudy));

            CreateMap<TeacherVM, TeacherDto>()
                .ForMember(dest=> dest.FirstName, opt=> opt.MapFrom(src=> CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(src.FirstName.TrimStart().TrimEnd())))
                .ForMember(dest=> dest.LastName, opt=> opt.MapFrom(src=> CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(src.LastName.TrimStart().TrimEnd())))
                .ForMember(dest=> dest.Title, opt=> opt.MapFrom(src=> (int)src.TeacherTitle));

            CreateMap<CourseVM, CourseDto>()
                .ForMember(dest=> dest.Name, opt=> opt.MapFrom(src=> CultureInfo.InstalledUICulture.TextInfo.ToTitleCase(src.Name).TrimStart().TrimEnd()));

            CreateMap<ExamVM, ExamDto>()
                .ForMember(dest=> dest.Grade, opt=> opt.MapFrom(src=> ConvertGradeDto(src.Grade)));            
        }


        public double? ConvertGradeDto(string grade)
        {
            return (double.TryParse(grade, out double result)) ? result : (double?)null;
            
        } 
    }
}