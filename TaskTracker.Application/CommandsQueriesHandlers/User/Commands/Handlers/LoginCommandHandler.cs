using TaskTracker.Application.Auth;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Shared.Common;

namespace TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers
{
    public class LoginCommandHandler(IUserRepository userRepository, IPasswordHasher passwordHasher)
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        public async Task<ApiResponse<UserDTO>> HandleAsync(LoginCommand request)
        {
            if (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password))
            {
                return ApiResponse<UserDTO>.FailResponse("Kullanıcı adı ve şifre zorunludur.");
            }

            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
            {
                return ApiResponse<UserDTO>.FailResponse("Geçersiz kullanıcı adı.");
            }

            bool isPasswordValid = _passwordHasher.VerifyPassword(request.Password, user.Data!.PasswordHash);
            if (!isPasswordValid)
            {
                return ApiResponse<UserDTO>.FailResponse("Geçersiz şifre.");
            }

            var userDto = new UserDTO
            {
                Id = user.Data!.Id,
                Name = user.Data!.Name,
                Surname = user.Data!.Surname,
                Email = user.Data!.Email
            };

            return ApiResponse<UserDTO>.SuccessResponse(userDto, "Giriş başarılı.");
        }
    }
}
