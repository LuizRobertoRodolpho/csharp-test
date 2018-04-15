using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class ProductCreateViewModel
    {
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }

        [MaxLength(50)]
        [Required]
        public string Description { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, 999999999999999999, ErrorMessage = "The price must be between 0.01 and 999999999999999999")]
        public decimal Price { get; set; }
    }
}
