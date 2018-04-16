using System;
using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class UserSearchViewModel
    {
        [MaxLength(30)]
        public string UserName { get; set; }

        [MaxLength(70)]
        public string DisplayName { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        
        [MaxLength(70)]
        [EmailAddress]
        public string Email { get; set; }

        // Column name
        public string OrderBy { get; set; }

        // ASC or DESC
        public string OrderType { get; set; }
    }
}
