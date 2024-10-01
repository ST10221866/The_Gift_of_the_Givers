using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.ViewModels
{
    public class AdminLoginViewModel
    {

        [Required(ErrorMessage = "Username is required.")]
        [StringLength(100, ErrorMessage = "Username must be between 3 and 100 characters.", MinimumLength = 3)]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
