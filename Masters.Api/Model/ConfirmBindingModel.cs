using System.ComponentModel.DataAnnotations;

namespace Masters.Api.Model
{
    public class ConfirmBindingModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Code { get; set; }
    }
}
