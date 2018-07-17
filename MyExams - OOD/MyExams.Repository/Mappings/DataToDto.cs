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
    public class DataToDto : Profile
    {
        public DataToDto()
        {
            CreateMap<Students, StudentsDto>();
            CreateMap<Teachers, TeacherDto>();
            CreateMap<Courses, CourseDto>();
            CreateMap<Exams, ExamDto>();
        }
    }
}
