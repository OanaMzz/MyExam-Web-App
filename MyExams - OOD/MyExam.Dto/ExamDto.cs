using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class ExamDto
    {
        public int ExamId { get; set; }
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public DateTime Date { get; set; }
        public Nullable<double> Grade { get; set; }
    }
}
