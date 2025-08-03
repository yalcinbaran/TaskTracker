using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class UpdateUserCommandHandler(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<(OperationResult Result, Guid UpdatedId)> HandleAsync(UpdateUserCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            var user = await _userRepository.GetByIdAsync(command.Id);
            if (user == null)
            {
                return (OperationResult.Fail("Kullanıcı bulunamadı."), Guid.Empty);
            }
            try
            {
                // Sadece dolu alanlar güncelleniyor
                bool profileUpdated = false;
                if (!string.IsNullOrWhiteSpace(command.Name) ||
                    !string.IsNullOrWhiteSpace(command.Surname) ||
                    !string.IsNullOrWhiteSpace(command.Email))
                {
                    user.UpdateProfile(
                        command.Name ?? user.Name,
                        command.Surname ?? user.Surname,
                        command.Email ?? user.Email!
                    );
                    profileUpdated = true;
                }

                bool passwordUpdated = false;
                if (!string.IsNullOrWhiteSpace(command.PasswordHash))
                {
                    user.ChangePassword(
                        command.PasswordHash
                    );
                    passwordUpdated = true;
                }

                if (!profileUpdated && !passwordUpdated)
                {
                    return (OperationResult.Fail("Güncellenecek herhangi bir bilgi bulunamadı."), user.Id);
                }

                var (result,Id ) = await _userRepository.UpdateAsync(user);
                if (!result.Success)
                {
                    return (OperationResult.Fail($"Kullanıcı güncelleme sırasında hata meydana geldi: {result.Message ?? "Bilinmeyen hata"}"), user.Id);
                }
                return (OperationResult.Ok("Kullanıcı başarıyla güncellendi."), Id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Kullanıcı güncelleme sırasında hata meydana geldi: {ex.Message ?? "Bilinmeyen hata"}"), user.Id);
            }
        }
    }
}
