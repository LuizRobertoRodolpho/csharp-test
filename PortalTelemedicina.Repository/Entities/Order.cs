using System;
using System.Collections.Generic;

namespace PortalTelemedicina.Repository.Entities
{
    public class Order
    {
        public int OrderId { get; set; }
        public DateTime CreationDate { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public List<OrderItem> OrderItems { get; set; }
    }
}
