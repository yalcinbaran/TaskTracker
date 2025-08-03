using TaskTracker.Application.Auth;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class CreateUserCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<(OperationResult Result, Guid CreatedId)> HandleAsync(CreateUserCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));

            var exists = await _userRepository.ExistsByEmailAsync(command.Email);
            if (exists)
                return (OperationResult.Fail("Bu e-posta adresi zaten kayıtlı."), Guid.Empty);

            var hashedPassword = _passwordHasher.HashPassword(command.Password);

            var user = new Users(
                command.Name,
                command.Surname,
                command.Email,
                hashedPassword
            );

            try
            {
                var (result, createdId) = await _userRepository.CreateAsync(user);

                if (!result.Success)
                    return (OperationResult.Fail("Kullanıcı oluşturulamadı: " + result.Message), Guid.Empty);

                return (OperationResult.Ok("Kullanıcı başarıyla oluşturuldu."), createdId);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail("Kayıt sırasında beklenmeyen hata: " + ex.Message), Guid.Empty);
            }
        }
    }
}
