using TaskTracker.Domain.Interfaces;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class DeleteUserCommandHandler(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository;
        public async Task<OperationResult> HandleAsync(DeleteUserCommand command)
        {
            ArgumentNullException.ThrowIfNull(command, nameof(command));
            if (command.Id == Guid.Empty)
            {
                return OperationResult.Fail("Geçersiz kullanıcı ID.");
            }
            try
            {
                var result = await _userRepository.DeleteAsync(command.Id);
                if (!result.Success)
                {
                    return OperationResult.Fail($"Kullanıcı silinirken hata oluştu: {result.Message ?? "Bilinmeyen hata"}");
                }
                return OperationResult.Ok("Kullanıcı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Kullanıcı silinirken hata oluştu: {ex.Message ?? "Bilinmeyen hata"}");
            }
        }
    }
}
