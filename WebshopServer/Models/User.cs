using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public long RoleId { get; set; }
        public Role Role { get; set; }
        public long? StatusId { get; set; }
        public Status Status { get; set; }
        public List<Article> Articles { get; set; }
    }
}
