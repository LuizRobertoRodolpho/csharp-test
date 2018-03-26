using System;
using System.Collections.Generic;

namespace PortalTelemedicina.Repository.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string DisplayName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; } /*SHA-256 generates a 256-bit hash value. You can use CHAR(64) or BINARY(32)*/
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }

        public List<Order> Orders { get; set; }
    }
}
