using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyExams.Dto
{
    public class CourseDto
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int TeacherId { get; set; }
        public int Duration { get; set; }
        public int Credits { get; set; }
    }
}
