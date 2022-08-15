using System.ComponentModel.DataAnnotations;

namespace Tugas2.Models
{
    public class StudentCreateViewModel
    {
       

        [Required]
        public string LastName { get; set; }
        [Required]
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
    }
}
