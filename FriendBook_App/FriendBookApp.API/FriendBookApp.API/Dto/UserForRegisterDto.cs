using System.ComponentModel.DataAnnotations;

namespace FriendBookApp.API.Dto
{
    public class UserForRegisterDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [StringLength(14,MinimumLength = 4, ErrorMessage = "Error Message: Your must specify the password between 4 and 14")]
        public string Password { get; set; }
    }
}
