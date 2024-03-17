using System.ComponentModel.DataAnnotations;

namespace Wild_Deer.Models.Dto
{
    public class SignInDto
    {
        [Required]
        public string Username { get; set; } = string.Empty;
        [Required]
        public string Password { get; set; }=  string.Empty;
    }
}
