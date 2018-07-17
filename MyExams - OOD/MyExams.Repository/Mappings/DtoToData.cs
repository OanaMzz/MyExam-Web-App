using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MyExams.Data;
using MyExams.Dto;

namespace MyExams.Repository.Mappings
{
    public class DtoToData : Profile
    {
        public DtoToData()
        {
            CreateMap<StudentsDto, Students>();
            CreateMap<TeacherDto, Teachers>();
            CreateMap<CourseDto, Courses>();
            CreateMap<ExamDto, Exams>();
        }
    }
}
