using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.ViewModel
{
    public class OrderItemResultViewModel
    {
        public int OrderItemId { get; set; }
        public int ProductId { get; set; }
        public int Amount { get; set; }
        public decimal CurrentPrice { get; set; }
    }
}
