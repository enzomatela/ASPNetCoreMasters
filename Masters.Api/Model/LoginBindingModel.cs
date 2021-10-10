using System.ComponentModel.DataAnnotations;

namespace Masters.Api.Model
{
    public class LoginBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
