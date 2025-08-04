using System.ComponentModel.DataAnnotations;

namespace TaskTrackerUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "İsim zorunludur")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur")]
        public string? Surname { get; set; }

        [Required, EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz")]
        public string? Email { get; set; }

        [Required]
        public string Username { get; set; } = default!;

        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string? PasswordConfirm { get; set; }
    }
}