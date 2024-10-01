using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.Models
{
    public class Admin
    {
        [Key]
        public Guid AdminID { get; set; }

        [Required]
        public string Username { get; set; } 

        [Required]
        public string PasswordHash { get; set; } 

    }
}
