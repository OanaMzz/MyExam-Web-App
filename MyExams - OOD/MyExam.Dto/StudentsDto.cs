using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class StudentsDto
    {
        public int StudentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long CNP { get; set; }
        public int Specialization { get; set; }
        public int YearOfStudy { get; set; }
    }
}
