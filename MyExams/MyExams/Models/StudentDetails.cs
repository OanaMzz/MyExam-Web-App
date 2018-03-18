using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using MyExams.Enums;

namespace MyExams.Models
{
    public class StudentDetails
    {
        [DisplayName("Student First Name")]
        public string StudentFirstName { get; set; }

        [DisplayName("Student Last Name")]
        public string StudentLastName { get; set; }

        [DisplayName("Student Name")]
        public string StudentFullName
        {
            get { return StudentFirstName + " " + StudentLastName; }
        }
    }


    public class StudentProfile
    {
        public Specialization Specialization { get; set; }

        [DisplayName("Year of Study")]
        public YearOfStudy YearOfStudy { get; set; }
    }
}