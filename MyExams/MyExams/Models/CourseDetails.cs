using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MyExams.Models
{
    public class CourseDetails : CoursesOfTeacher
    {
        public TeacherDetails Teacher { get; set; }
    }

    public class CoursesOfTeacher
    {
        public string Course { get; set; }

        [DisplayName("Duration (Semesters)")]
        public int Duration { get; set; }

        public int Credits { get; set; }
    }
}