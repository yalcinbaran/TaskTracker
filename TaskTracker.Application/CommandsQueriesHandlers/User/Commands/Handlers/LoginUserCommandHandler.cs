using TaskTracker.Application.Auth;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class LoginUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<(OperationResult Result, Guid Id)> HandleAsync(LoginUserCommand command)
        {
            ArgumentNullException.ThrowIfNull(command);

            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null)
            {
                return (OperationResult.Fail("Kullanıcı bulunamadı."), Guid.Empty);
            }

            bool passwordValid = _passwordHasher.VerifyPassword(command.Password, user.PasswordHash);
            if (!passwordValid)
            {
                return (OperationResult.Fail("Şifre hatalı."), Guid.Empty);
            }

            // Giriş başarılı, istersen token vb. oluşturabilirsin
            return (OperationResult.Ok("Giriş başarılı"), user.Id);
        }
    }
}
