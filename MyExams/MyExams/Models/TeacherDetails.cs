using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyExams.Enums;

namespace MyExams.Models
{
    public class TeacherDetails
    {
        [DisplayName("Teacher First Name")]
        public string TeacherFirstName { get; set; }

        [DisplayName("Teacher Last Name")]
        public string TeacherLastName { get; set; }

        [DisplayName("Teacher Name")]
        public string TeacherFullName
        {
            get { return TeacherFirstName + " " + TeacherLastName; }
        }

        [DisplayName("Teacher Title")]
        public Title TeacherTitle { get; set; }
    }
}