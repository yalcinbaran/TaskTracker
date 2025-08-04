using TaskTracker.SharedKernel.Common;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.ValueObjects;

namespace TaskTracker.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<Users?> GetByIdAsync(Guid id);
        Task<(OperationResult Result, Guid CreatedId)> CreateAsync(Users user);
        Task<(OperationResult Result, Guid UpdatedId)> UpdateAsync(Users user);
        Task<OperationResult> DeleteAsync(Guid id);
        Task<bool> ExistsByEmailAsync(string email);
        Task<Users?> GetByEmailAsync(string email);
        Task<Users?> GetByUsernameAsync(string email);
        Task<bool> CheckPasswordAsync(Users user, string password); // hash karşılaştırması
    }
}
