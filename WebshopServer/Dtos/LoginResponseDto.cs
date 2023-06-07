using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopServer.Dtos
{
    public class LoginResponseDto
    {
        public long Id { get; set; }
        public string Token { get; set; }
    }
}
