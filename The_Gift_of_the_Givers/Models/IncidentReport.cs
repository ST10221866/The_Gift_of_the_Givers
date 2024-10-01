using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.Models
{
    public class IncidentReport
    {
        [Key]
        public Guid IncidentID { get; set; }

        [Required]
        [StringLength(100)]
        public string IncidentType { get; set; }

        [Required]
        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public DateTime DateOfIncident { get; set; }

        [Required]
        [StringLength(100)]
        public string ReporterName { get; set; }

        [Required]
        [StringLength(100)]
        public string ReporterEmail { get; set; }

        [StringLength(100)]
        public string Location { get; set; }
    }
}
