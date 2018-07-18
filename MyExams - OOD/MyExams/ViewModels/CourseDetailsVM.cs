using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace MyExams.ViewModels
{
    public class CourseDetailsVM
    {
        public int CourseId { get; set; }
      
        [DisplayName("Course")]
        public string Name { get; set; }

        public TeacherVM TeacherDetails { get; set; }

        [DisplayName("Duration (Semesters)")]
        public int Duration { get; set; }

        public int Credits { get; set; }      

    }
}