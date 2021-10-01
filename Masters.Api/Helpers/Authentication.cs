using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Masters.Api.Helpers
{
    public class Authentication
    {
        public JWT Jwt { get; set; }
    }

    public class JWT
    {
        public string SecurityKey { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }
    }
}
