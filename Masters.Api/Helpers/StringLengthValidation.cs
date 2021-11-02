using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Masters.Api.Helpers
{
    public class StringLengthValidation  :ValidationAttribute
    {
        private int _minLength;

        public StringLengthValidation(int minLength)
        {
            _minLength = minLength;
        }

        public override bool IsValid(object value)
        {
            return value.ToString().Length >= _minLength;
        }
    }
}
