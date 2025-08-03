using System.ComponentModel.DataAnnotations;

namespace TaskTrackerUI.Models
{
    public class RegisterModel
    {
        [Required(ErrorMessage = "İsim zorunludur")]
        public string? FirstName { get; set; }

        [Required(ErrorMessage = "Soyisim zorunludur")]
        public string? SurName { get; set; }

        [Required, EmailAddress(ErrorMessage = "Geçerli bir e-posta giriniz")]
        public string? Email { get; set; }

        [Required]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Şifreler uyuşmuyor")]
        public string? PasswordConfirm { get; set; }
    }
}