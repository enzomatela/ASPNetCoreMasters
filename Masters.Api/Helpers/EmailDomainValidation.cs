using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Masters.Api.Helpers
{
    public class EmailDomainValidation : ValidationAttribute
    {
        private string _allowedDoamin;

        public EmailDomainValidation(string allowedDomain)
        {
            _allowedDoamin = allowedDomain;
        }

        public override bool IsValid(object value)
        {
            if (value == null) return true; 

            string[] strings = value.ToString().Split("@");

            if (strings.Length > 1)
            {
                if(strings[1].ToUpper() != _allowedDoamin.ToUpper())
                {
                    ErrorMessage = string.Format("{0} is not a valid email", value);
                    return false;
                } else { ErrorMessage = string.Empty; return true; }

            } else
            {
                ErrorMessage = string.Format("{0} is not a valid email",value);
                return false;
            }
           
        }
    }
}
