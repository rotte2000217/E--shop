using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebshopServer.Dtos
{
    public class VerifyDto
    {
        public long UserId { get; set; }
        public long StatusId { get; set; }
    }
}
