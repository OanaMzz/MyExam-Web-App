using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class ExamsOfStudentDto
    {
        public DateTime Date { get; set; }

        public string Name { get; set; }

        public  TeacherDto Teacher { get; set; }

        public Nullable<double> Grade { get; set; }

        public int Credits { get; set; }
    }
}
