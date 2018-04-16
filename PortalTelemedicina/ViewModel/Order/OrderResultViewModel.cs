using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.ViewModel
{
    public class OrderResultViewModel
    {
        public int OrderId { get; set; }
        public int UserId { get; set; }
        public DateTime CreationDate { get; set; }
        public List<OrderItemResultViewModel> OrderItems { get; set; }
    }
}
