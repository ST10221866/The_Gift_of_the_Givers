using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.Models
{
    public class Volunteer
    {
        [Key]
        public Guid VolunteerId { get; set; }

        [Required]
        public Guid UserId { get; set; }

        [Required]
        [StringLength(100)]
        public string TaskType { get; set; } 

        [Required]
        [StringLength(500)]
        public string Availability { get; set; } 
    }
}
