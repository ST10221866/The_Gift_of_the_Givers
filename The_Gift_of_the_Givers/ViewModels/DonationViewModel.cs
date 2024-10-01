using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.ViewModels
{
    public class DonationViewModel
    {
        [Required]
        [StringLength(100)]
        public string DonationType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Your Name")]
        public string DonorName { get; set; }

        [Required]
        [EmailAddress]
        [Display(Name = "Your Email")]
        public string DonorEmail { get; set; }

        [Display(Name = "Delivery Date")]
        [DataType(DataType.Date)]
        public DateTime DeliveryDate { get; set; }


    }
}
