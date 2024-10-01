using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.Models
{
    public class Donation
    {
        [Key]
        public Guid DonationID { get; set; } 

        [Required]
        public Guid UserID { get; set; } 

        [Required]
        [StringLength(100)]
        public string DonationType { get; set; } 

        [Required]
        public int Quantity { get; set; } 

        [StringLength(255)]
        public string Description { get; set; } 

        [Required]
        public DateTime DonationDate { get; set; } 

        [Required]
        public bool IsDistributed { get; set; } 

        // Navigation property
        public Users User { get; set; }
        public string DonorName { get; internal set; }
        public string DonorEmail { get; internal set; }
        public DateTime DeliveryDate { get; internal set; }
    }

}
