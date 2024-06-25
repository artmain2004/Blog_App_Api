using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTO.Response
{
    public class LoginResponse
    {
        public string Message { get; set; }

        public UserDto User { get; set; }

        public string Token { get; set; }
    }
}
