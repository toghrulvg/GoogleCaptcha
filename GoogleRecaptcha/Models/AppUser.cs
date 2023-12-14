using System.ComponentModel.DataAnnotations;

namespace GoogleRecaptcha.Models
{
    public class AppUser
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string Token { get; set; }
    }
}
