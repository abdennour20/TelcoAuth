using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Domain.Entites
{
    public class User
    {
        public required string id { get; set; }
        public required string firstName { get; set; }
        public required string lastName { get; set; }
        public required string email { get; set; }
        public required string phoneNumber { get; set; }
        public required string userType { get; set; }
        public required string password { get; set; }
        public required DateTime createdAt { get; set; }
        public required DateTime lastLoginDate { get; set; }
    }
}
