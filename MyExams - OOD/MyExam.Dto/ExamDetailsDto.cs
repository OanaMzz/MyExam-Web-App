using MyExams.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class ExamDetailsDto
    {
        public int ExamId { get; set; }
        
        public int CourseId { get; set; }

        public string Name { get; set; }

        public StudentsDto StudentDetails { get; set; }

        public DateTime Date { get; set; }

        public Nullable<double> Grade { get; set; }

        public int Credits { get; set; }

    }
}
