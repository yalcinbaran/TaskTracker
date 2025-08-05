using TaskTracker.Domain.Entities;
using TaskTracker.SharedKernel.Common;

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
    }
}
