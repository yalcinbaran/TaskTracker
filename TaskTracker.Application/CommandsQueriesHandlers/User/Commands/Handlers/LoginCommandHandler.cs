using TaskTracker.Application.Auth;
using TaskTracker.Domain.Interfaces;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ApiResponse<T>> HandleAsync<T>(LoginCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return ApiResponse<T>.FailResponse("Kullanıcı adı ve şifre zorunludur.");
            }

            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return ApiResponse<T>.FailResponse("Geçersiz kullanıcı adı.");
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                return ApiResponse<T>.FailResponse("Geçersiz şifre.");
            }

            // Başarılı giriş: DTO'ya mapleme
            var userDto = new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email
            };

            // T tipi kontrolü (opsiyonel güvenlik önlemi)
            if (userDto is T castedDto)
            {
                return ApiResponse<T>.SuccessResponse(castedDto,"Giriş başarılı");
            }

            return ApiResponse<T>.FailResponse("Beklenmeyen dönüş tipi.");
        }

    }
}
