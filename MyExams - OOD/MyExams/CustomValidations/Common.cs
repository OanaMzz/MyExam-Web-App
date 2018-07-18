using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MyExams.ViewModels;
namespace MyExams.Models
{

    public class NameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var firstName = value.ToString().TrimStart().TrimEnd();

            if ((!Char.IsLetter(firstName[0])) || (!Char.IsLetter(firstName[firstName.Length - 1])))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return null;
            }
        }

    }

    public class CourseNameValidationAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var courseName = value.ToString().TrimStart().TrimEnd();

            if ((!Char.IsLetter(courseName[0])) || ((!Char.IsLetter(courseName[courseName.Length - 1]))
                && (!Char.IsNumber(courseName[courseName.Length - 1])) && (courseName[courseName.Length - 1] != ')')))
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }
            else
            {
                return null;
            }
        }
    }


    public class ValidCNP : ValidationAttribute
    {
        private const string StringCHECKDIGIT = "279146358279";


        private string errors;
        private bool ValidateMonth(string cnp)
        {
            var month = int.Parse(cnp.Substring(3, 2));
            if ((month >= 1) && (month <= 12))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        private bool ValidateDay(string cnp)
        {
            var day = int.Parse(cnp.Substring(5, 2));
            return ((day >= 1) && (day <= 31));
        }


        private bool ValidateCounty(string cnp)
        {
            var county = int.Parse(cnp.Substring(7, 2));
            return ((county >= 1) && (county <= 52));
        }


        private bool ValidateGender(string cnp)
        {
            var gender = int.Parse(cnp.Substring(0, 1));
            return ((gender >= 1) && (gender <= 8));
        }


        private bool ValidateLength(string cnp)
        {
            return (cnp.Length == 13);
        }


        private int CalculateCheckDigit(string cnp)
        {

            var checkSum = 0;
            int checkDigit;
            for (var i = 0; i < 12; i++)
            {
                checkSum += int.Parse(StringCHECKDIGIT[i].ToString()) * int.Parse(cnp[i].ToString());
            }
            checkDigit = checkSum % 11;
            if (checkDigit == 10)
            {
                checkDigit = 1;
            }
            return checkDigit;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            if (value != null)
            {
                errors = "";
                var cnp = value.ToString();
                bool isValid = true;
                if (!ValidateLength(cnp))
                {
                    isValid = false;
                    errors += "length should be 13,\n";
                }


                if (!ValidateGender(cnp))
                {
                    isValid = false;
                    errors += "gender should be between 1 and 8, \n";
                }


                if (!ValidateMonth(cnp))

                {
                    isValid = false;
                    errors += "month shoud be between 1 and 12, \n";
                }


                if (!ValidateDay(cnp))

                {
                    isValid = false;
                    errors += "day should be between 1 and 31, \n";
                }


                if (!ValidateCounty(cnp))
                {
                    isValid = false;
                    errors += "county should be between 1 and 52, \n";
                }

                if (CalculateCheckDigit(cnp) != int.Parse(cnp[12].ToString()))

                {
                    isValid = false;
                    errors += "check digit is not valid, ";
                }


                if (isValid)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    errors = "CNP invalid:" + errors;
                    return new ValidationResult(errors);
                }

            }
            return base.IsValid(value, validationContext);

        }
    }
}