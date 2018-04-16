using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class OrderCreateViewModel
    {
        [Required]
        public int UserId { get; set; }

        public List<OrderItemViewModel> OrderItems { get; set; }
    }
}
