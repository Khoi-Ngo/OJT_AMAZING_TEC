using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiService.DTO.response
{
    public class UserRes
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

    }
}