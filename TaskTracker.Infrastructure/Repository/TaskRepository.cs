using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Infrastructure.Persistence;

namespace TaskTracker.Infrastructure.Repository
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task AddAsync(TaskItem task)
        {
            await _context.TaskItems.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.AsNoTracking().ToListAsync();
        }

        public async Task UpdateAsync(TaskItem task)
        {
            _context.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var entity = await _context.TaskItems.FindAsync(id);
            if (entity != null)
            {
                _context.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteRange(IEnumerable<Guid> ids)
        {
            var entities = await _context.TaskItems
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();
            if (entities.Count > 0)
            {
                _context.RemoveRange(entities);
                await _context.SaveChangesAsync();
            }
        }
    }
}
