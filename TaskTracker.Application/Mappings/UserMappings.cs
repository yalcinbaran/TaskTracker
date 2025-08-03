using TaskTracker.Application.CommandsQueriesHandlers.DTOs;
using TaskTracker.Domain.Entities;

namespace TaskTracker.Application.Mappings
{
    public static class UserMappings
    {
        // Domain -> DTO dönüşümü
        public static UserDTO? ToDto(this Users user)
        {
            if (user == null) return null;
            return new UserDTO
            {
                Id = user.Id,
                Name = user.Name,
                Surname = user.Surname,
                Email = user.Email,
            };
        }
        // DTO -> Domain dönüşümü
        public static Users? ToDomain(this UserDTO dto)
        {
            if (dto == null) return null;
            return new Users(
                dto.Name ?? string.Empty,
                dto.Surname ?? string.Empty,
                dto.Email ?? string.Empty,
                dto.PasswordHash ?? string.Empty
            );
        }
    }
}
