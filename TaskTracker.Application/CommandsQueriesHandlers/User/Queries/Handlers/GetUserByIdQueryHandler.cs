using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Application.Mappings;
using TaskTracker.Domain.Interfaces;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Queries.Handlers
{
    public class GetUserByIdQueryHandler(IUserRepository userRepository)
    {
        private readonly IUserRepository _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        public async Task<UserDTO?> HandleAsync(GetUserByIdQuery query)
        {
            ArgumentNullException.ThrowIfNull(query, nameof(query));
            if (query.Id == Guid.Empty)
            {
                throw new ArgumentException("Geçersiz kullanıcı ID.", nameof(query.Id));
            }
            try
            {
                var user = await _userRepository.GetByIdAsync(query.Id);

                return user!.ToDto();
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Kullanıcı bilgileri alınırken hata oluştu: {ex.Message ?? "Bilinmeyen hata"}", ex);
            }
        }
    }
}
