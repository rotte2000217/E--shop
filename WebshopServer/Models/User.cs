using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebshopServer.Enums;

namespace WebshopServer.Models
{
    public class User
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Birthdate { get; set; }
        public string Address { get; set; }
        public UserRole Role { get; set; }
        public VerificationStatus? VerificationStatus { get; set; }
        public List<Article> Articles { get; set; }
        public List<Order> Orders { get; set; }
    }
}
