using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Enums;

namespace WebshopServer.Dtos
{
    public class VerifyDto
    {
        public long UserId { get; set; }
        public VerificationStatus VerificationStatus { get; set; }
    }
}
