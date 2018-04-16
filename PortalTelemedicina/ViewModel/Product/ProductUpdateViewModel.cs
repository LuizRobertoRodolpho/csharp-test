using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class ProductUpdateViewModel
    {
        [MaxLength(30)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, 999999999999999999, ErrorMessage = "The price must be between 0.01 and 999999999999999999")]
        public decimal Price { get; set; }
    }
}
