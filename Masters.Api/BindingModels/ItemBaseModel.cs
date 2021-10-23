using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Masters.Api.BindingModels
{
    public class ItemBaseModel
    {
        public int? ItemId { get; set; }

        [Required]
        [StringLength(128, ErrorMessage = "{0} length must be between {2} and {1}.", MinimumLength = 6)]
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
