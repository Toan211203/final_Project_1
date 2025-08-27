using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Models
{
    public class LoginResponseModel
    {
        public String? USername { get; set; }
        public String? AccessToken { get; set; }

        public int ExpiresIn { get; set; }
    }
}