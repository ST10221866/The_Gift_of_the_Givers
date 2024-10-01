using System.ComponentModel.DataAnnotations;

namespace The_Gift_of_the_Givers.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
