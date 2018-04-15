using System.ComponentModel.DataAnnotations;

namespace PortalTelemedicina.ViewModel
{
    public class SignUpViewModel
    {
        [Required]
        [MaxLength(70)]
        public string DisplayName { get; set; }

        [Required]
        [MaxLength(30)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(70)]
        public string Email { get; set; }
    }
}
