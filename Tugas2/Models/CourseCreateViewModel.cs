using System.ComponentModel.DataAnnotations;

namespace Tugas2.Models
{
    public class CourseCreateViewModel
    {
        [Required]
        public string Title { get; set; }
        [Required]
        public int Credits { get; set; }
    }
}
