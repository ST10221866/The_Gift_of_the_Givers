using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.ViewModels
{
    public class IncidentReportViewModel
    {
        [Required]
        [Display(Name = "Incident Type")]
        public string IncidentType { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Date of Incident")]
        public DateTime DateOfIncident { get; set; }

        [Required]
        [Display(Name = "Reporter Name")]
        public string ReporterName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Reporter Email")]
        public string ReporterEmail { get; set; }

        [Display(Name = "Location")]
        public string Location { get; set; }
    }
}
