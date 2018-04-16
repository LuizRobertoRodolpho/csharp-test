using System;

namespace PortalTelemedicina.ViewModel
{
    public class ProductSearchResultViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
