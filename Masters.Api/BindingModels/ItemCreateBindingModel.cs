using System.ComponentModel.DataAnnotations;

namespace Masters.Api.BindingModels
{
    public class ItemCreateBindingModel
    {
        [Required]
        [StringLength(128, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Text { get; set; }
    }
}
