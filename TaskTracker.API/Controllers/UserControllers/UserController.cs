using Microsoft.AspNetCore.Mvc;
using TaskTracker.Application.CommandsQueriesHandlers.User.Commands;
using TaskTracker.Application.CommandsQueriesHandlers.User.Commands.Handlers;
using TaskTracker.Application.CommandsQueriesHandlers.User.Queries;
using TaskTracker.Application.CommandsQueriesHandlers.User.Queries.Handlers;
using TaskTracker.Shared.Common;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.API.Controllers.UserControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(CreateUserCommandHandler createUser,
                                DeleteUserCommandHandler deleteUser,
                                GetUserByIdQueryHandler getUserBy,
                                UpdateUserCommandHandler updateUser,
                                LoginCommandHandler login) : ControllerBase
    {
        private readonly CreateUserCommandHandler _createUser = createUser;
        private readonly DeleteUserCommandHandler _deleteUser = deleteUser;
        private readonly GetUserByIdQueryHandler _getUserBy = getUserBy;
        private readonly UpdateUserCommandHandler _updateUser = updateUser;
        private readonly LoginCommandHandler _login = login;

        [HttpPost("CreateUser")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
        {
            if (command == null)
                return BadRequest(ApiResponse<string>.FailResponse("Geçersiz istek."));

            var (result, createdId) = await _createUser.HandleAsync(command);

            if (result.Success && createdId != Guid.Empty)
            {
                var dto = new UserDTO
            {
                    Id = createdId,
                    Name = command.Name,
                    Surname = command.Surname,
                    Username = command.Username,
                    Email = command.Email
                };
                return CreatedAtAction(nameof(GetUserById), new { id = createdId }, ApiResponse<UserDTO>.SuccessResponse(dto, "Kullanıcı oluşturuldu."));
            }

            return BadRequest(ApiResponse<string>.FailResponse(result.Message!));
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Geçersiz Id.");
            var result = await _deleteUser.HandleAsync(new DeleteUserCommand(id));
            if (result.IsSuccess)
                return Ok(new { message = result.Message });
            return BadRequest(new { message = result.Message });
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest("Geçersiz Id.");

            var user = await _getUserBy.HandleAsync(new GetUserByIdQuery(id));
            if (user != null)
                return Ok(user);

            return NotFound("Kullanıcı bulunamadı.");
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (command == null || id != command.Id)
                return BadRequest("Geçersiz istek.");

            var (result, updatedId) = await _updateUser.HandleAsync(command);
            if (result.IsSuccess)
                return Ok(new { message = result.Message, id = updatedId });

            return BadRequest(new { message = result.Message });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginCommand model)
        {
            var response = await _login.HandleAsync<UserDTO>(model);

            if (!response.Success || response.Data == null)
            {
                return Unauthorized(ApiResponse<UserDTO>.FailResponse(response.Message!));
            }

            return Ok(ApiResponse<UserDTO>.SuccessResponse(response.Data, "Giriş başarılı."));
        }
    }
}
