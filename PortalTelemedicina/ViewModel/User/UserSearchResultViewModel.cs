using System;

namespace PortalTelemedicina.ViewModel
{
    public class UserSearchResultViewModel
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public DateTime CreationDate { get; set; }
    }
}
