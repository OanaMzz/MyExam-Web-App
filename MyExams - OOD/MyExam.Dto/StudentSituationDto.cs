using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class StudentSituationDto
    {
        public StudentsDto Student { get; set; }

        public int ExamsCount { get; set; }

        public int ExamsToRetakeCount { get; set; }

        public List<string> ExamsToRetake { get; set; }

        public double Average { get; set; }

        public int Credits { get; set; }

        public List<ExamsOfStudentDto> ExamTable { get; set; }
    }
}
