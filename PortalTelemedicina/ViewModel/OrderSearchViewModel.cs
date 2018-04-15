using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PortalTelemedicina.ViewModel
{
    public class OrderSearchViewModel
    {
        public int? OrderId { get; set; }

        public int? UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, 999999999999999999, ErrorMessage = "The inital price must be between 0.01 and 999999999999999999")]
        public decimal? MinTotal { get; set; }

        [DataType(DataType.Currency)]
        [Range(0.01, 999999999999999999, ErrorMessage = "The end price must be between 0.01 and 999999999999999999")]
        public decimal? MaxTotal { get; set; }
    }
}
