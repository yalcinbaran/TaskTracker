using Microsoft.EntityFrameworkCore;
using TaskTracker.Application.Auth;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Infrastructure.Persistence;
using TaskTracker.Shared.Common;

namespace TaskTracker.Infrastructure.Repository
{
    public class UserRepository(AppDbContext context, IPasswordHasher passwordHasher) : IUserRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));
        private readonly IPasswordHasher _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));

        public Task<bool> CheckPasswordAsync(Users user, string password)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            ArgumentNullException.ThrowIfNull(password, nameof(password));

            // user.PasswordHash gibi bir alan olduğunu varsayalım
            bool isValid = _passwordHasher.VerifyPassword(password, user.PasswordHash);
            return Task.FromResult(isValid);
        }

        public async Task<(OperationResult Result, Guid CreatedId)> CreateAsync(Users user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            try
            {
                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();
                return (OperationResult.Ok("Kullanıcı başarıyla eklendi."), user.Id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Kullanıcı eklenirken hata oluştu: {ex.Message}"), Guid.Empty);
            }
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            try
            {
                await _context.Users.Where(u => u.Id == id).ExecuteDeleteAsync();
                return OperationResult.Ok("Kullanıcı başarıyla silindi.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Kullanıcı silinirken hata oluştu: {ex.Message}" ?? "Bilinmeyen hata.");
            }
        }

        public Task<bool> ExistsByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email, nameof(email));
            return _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task<Users?> GetByEmailAsync(string email)
        {
            ArgumentNullException.ThrowIfNull(email, nameof(email));

            return await _context.Users.AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Users?> GetByIdAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id));
            return await _context.Users.AsNoTracking()
                                       .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApiResponse<Users?>> GetByUsernameAsync(string username)
        {
            ArgumentNullException.ThrowIfNull(username, nameof(username));
            ApiResponse<Users?> response = new()
            {
                Success = true,
                Data = await _context.Users.AsNoTracking()
                                           .FirstOrDefaultAsync(u => u.Username == username)
            };
            return response;
        }

        public async Task<(OperationResult Result, Guid UpdatedId)> UpdateAsync(Users user)
        {
            ArgumentNullException.ThrowIfNull(user, nameof(user));
            try
            {
                await _context.Users
                    .Where(u => u.Id == user.Id)
                    .ExecuteUpdateAsync(u => u.SetProperty(x => x.Email, user.Email)
                                              .SetProperty(x => x.Name, user.Name)
                                              .SetProperty(x => x.Surname, user.Surname)
                                              .SetProperty(x => x.PasswordHash, user.PasswordHash));
                return (OperationResult.Ok("Kullanıcı başarıyla güncellendi."), user.Id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Kullanıcı güncellenirken hata oluştu: {ex.Message}" ?? "Bilinmeyen hata."), Guid.Empty);
            }
        }
    }
}
