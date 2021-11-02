using Masters.Api.Helpers;
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
        [StringLengthValidation(6, ErrorMessage = "{0} field validation failed.")]
        public string Text { get; set; }
        public string CreatedBy { get; set; }
        public DateTime DateCreated { get; set; }
        [EmailDomainValidation("test.com")]
        public string EmailAddress { get; set; }
    }
}
