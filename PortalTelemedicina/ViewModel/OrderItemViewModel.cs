using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class OrderItemViewModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, 999999999999999999, ErrorMessage = "The price must be between 0.01 and 999999999999999999")]
        public decimal CurrentPrice { get; set; }
    }
}
