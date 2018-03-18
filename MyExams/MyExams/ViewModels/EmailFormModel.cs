using System.ComponentModel.DataAnnotations;

namespace MyExams.ViewModels
{
    public class EmailFormModel
    {
        [Required, Display(Name = "Name")]
        [StringLength(60, MinimumLength = 3)]
        public string FromName { get; set; }

        [Required, Display(Name = "Email"), EmailAddress]
        public string FromEmail { get; set; }

        [Required]
        public string Message { get; set; }
    }
}