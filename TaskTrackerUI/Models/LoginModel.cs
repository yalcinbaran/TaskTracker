using System.ComponentModel.DataAnnotations;

namespace TaskTrackerUI.Models
{
    public class LoginModel
    {
        [Required(ErrorMessage = "Kullanıcı adı zorunludur")]
        public string? Username { get; set; }

        [Required(ErrorMessage = "Şifre zorunludur")]
        public string? Password { get; set; }
    }
}