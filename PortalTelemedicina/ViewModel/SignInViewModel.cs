using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class SignInViewModel
    {
        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }
    }
}
