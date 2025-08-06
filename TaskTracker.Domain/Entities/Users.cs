using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared;

namespace TaskTracker.Domain.Entities
{
    public class Users : BaseEntity, IAggregateRoot
    {
        public string Name { get; private set; }
        public string Surname { get; private set; }
        public string Email { get; private set; }
        public string Username { get; private set; }
        public string PasswordHash { get; private set; }

        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private Users() { } // EF için

        public Users(string name, string surname, string email, string username, string passwordHash)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("İsim bilgisi boş olamaz.", nameof(name));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Soyisim bilgisi boş olamaz.", nameof(surname));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email bilgisi boş olamaz.", nameof(email));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Kullanıcı adı bilgisi boş olamaz.", nameof(username));
            if (string.IsNullOrWhiteSpace(passwordHash))
                throw new ArgumentException("Şifre bilgisi boş olamaz.", nameof(passwordHash));

            // Basic email format validation
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    throw new ArgumentException("Geçersiz email formatı.", nameof(email));
            }
            catch
            {
                throw new ArgumentException("Geçersiz email formatı.", nameof(email));
            }

            Name = name;
            Surname = surname;
            Email = email;
            Username = username;
            PasswordHash = passwordHash;
        }

        public void ChangePassword(string newPasswordHash)
        {
            if (string.IsNullOrWhiteSpace(newPasswordHash))
            {
                throw new ArgumentException("Şifre alanı null veya boş olamaz.", nameof(newPasswordHash));
            }
            if (newPasswordHash == PasswordHash)
            {
                throw new InvalidOperationException("Yeni şifre, eski şifre ile aynı olamaz.");
            }
            PasswordHash = newPasswordHash;
        }

        public void UpdateProfile(string name, string surname, string email, string username)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("İsim bilgisi boş olamaz.", nameof(name));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentException("Soyisim bilgisi boş olamaz.", nameof(surname));
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email bilgisi boş olamaz.", nameof(email));
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException("Kullanıcı adı bilgisi boş olamaz.", nameof(username));

            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                if (addr.Address != email)
                    throw new ArgumentException("Geçersiz email formatı.", nameof(email));
            }
            catch
            {
                throw new ArgumentException("Geçersiz email formatı.", nameof(email));
            }

            Name = name;
            Surname = surname;
            Email = email;
            Username = username;
        }
    }
}
