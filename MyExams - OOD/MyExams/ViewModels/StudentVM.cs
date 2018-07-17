using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using MyExams.Models;
using MyExams.Infrastructure;

namespace MyExams.ViewModels
{
    public class StudentVM
    {
        public int StudentId { get; set; }

        [Required]
        [DisplayName("First Name")]
        [NameValidation(ErrorMessage = "First Name should begin and end only in letters")]
        [RegularExpression("^[a-zA-Z\\s'-]+$", ErrorMessage = "Please insert only letters and  - ' ")] 
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name should be 2-50 characters")]
        public string FirstName { get; set; }

        [Required]
        [DisplayName("Last Name")]
        [NameValidation(ErrorMessage = "Last Name should begin and end only in letters")]
        [RegularExpression("^[a-zA-Z\\s-']+$", ErrorMessage = "Please insert only letters and  - ' ")] 
        [StringLength(50, MinimumLength = 2, ErrorMessage = "First Name should be 2-50 characters")]
        public string LastName  { get; set; }

        [DisplayName("Student Name")]
        public string StudentFullName
        {
            get { return FirstName + " " + LastName; }
        }

        [Required]
        [RegularExpression("^\\d+$", ErrorMessage = "Please insert only numbers")]
        [StringLength(13, MinimumLength =13, ErrorMessage = "CNP should be 13 digits")]
        [ValidCNP]
        public string CNP { get; set; }

        [Required]
        public Specialization? Specialization { get; set; }
   
        [Required]
        [DisplayName("Year of Study")]
        public YearOfStudy? YearOfStudy { get; set; }
    }

}
