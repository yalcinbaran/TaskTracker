using Microsoft.EntityFrameworkCore;
using TaskTracker.Domain.Entities;
using TaskTracker.Domain.Interfaces;
using TaskTracker.Infrastructure.Persistence;
using TaskTracker.Shared.Common;
using TaskTracker.SharedKernel.Common;

namespace TaskTracker.Infrastructure.Repository
{
    public class TaskRepository(AppDbContext context) : ITaskRepository
    {
        private readonly AppDbContext _context = context ?? throw new ArgumentNullException(nameof(context));

        public async Task<(OperationResult Result, Guid CreatedId)> AddAsync(TaskItem task)
        {
            try
            {
                await _context.TaskItems.AddAsync(task);
                await _context.SaveChangesAsync();

                return (OperationResult.Ok("Görev başarıyla eklendi."), task.Id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev eklenirken hata oluştu: {ex.Message}"), Guid.Empty);
            }
        }

        public async Task<TaskItem?> GetByIdAsync(Guid id)
        {
            return await _context.TaskItems.FindAsync(id);
        }

        public async Task<IEnumerable<TaskItem>> GetAllAsync()
        {
            return await _context.TaskItems.AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByPriorityAsync(int priorityLevel)
        {
            return await _context.TaskItems.Where(t => t.Priority!.Level == priorityLevel).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksByStateAsync(int taskStateLevel)
        {
            return await _context.TaskItems.Where(t => t.TaskState!.Level == taskStateLevel).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetTasksBySerachQuery(string serachQuery)
        {
            return await _context.TaskItems.Where(t => t.Title!.Contains(serachQuery) || t.Description!.Contains(serachQuery)).AsNoTracking().ToListAsync();
        }

        public async Task<IEnumerable<TaskItem>> GetAllOverDueTasks(DateTime date)
        {
            return await _context.TaskItems.Where(t => t.DueDate.Date < date.Date).AsNoTracking().ToListAsync();
        }

        public async Task<(OperationResult Result, Guid UpdatedId)> UpdateAsync(TaskItem task)
        {
            try
            {
                _context.TaskItems.Update(task);
                await _context.SaveChangesAsync();
                return (OperationResult.Ok("Görev başarıyla güncellendi."), task.Id);
            }
            catch (Exception ex)
            {
                return (OperationResult.Fail($"Görev güncellenirken hata oluştu: {ex.Message}"), Guid.Empty);
            }
        }

        public async Task<OperationResult> DeleteAsync(Guid id)
        {
            var entity = await _context.TaskItems.FindAsync(id);
            if (entity == null)
            {
                // Task bulunamadıysa, uygun bir başarısız sonuç döndür
                return OperationResult.Fail("Görev bulunamadı");
            }

            try
            {
                await _context.TaskItems.Where(t => t.Id == id).ExecuteDeleteAsync();
                return OperationResult.Ok("Görev başarıyla silindi");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail($"Görev silme sırasında bir hata meydana geldi: {ex.Message}" ?? "Bilinmeyen hata.");
            }
        }

        public async Task<List<TaskDeleteResult>> DeleteRangeAsync(IEnumerable<Guid> ids)
        {
            var entities = await _context.TaskItems
                .Where(t => ids.Contains(t.Id))
                .ToListAsync();

            var results = new List<TaskDeleteResult>();

            foreach (var entity in entities)
            {
                try
                {
                    await _context.TaskItems.Where(t => t.Id == entity.Id).ExecuteDeleteAsync();
                    results.Add(new TaskDeleteResult(entity.Id, entity.Title!, entity.DueDate, true));
                }
                catch (Exception ex)
                {
                    results.Add(new TaskDeleteResult(entity.Id, entity.Title!, entity.DueDate, false, ex.Message));
                }
            }

            // İstenilen id'ler içinde bulunamayanlar için de sonuç oluşturabilirsin
            var foundIds = entities.Select(e => e.Id).ToHashSet();
            var notFoundIds = ids.Where(id => !foundIds.Contains(id));
            foreach (var id in notFoundIds)
            {
                results.Add(new TaskDeleteResult(id, string.Empty, DateTime.MinValue, false, "Görev bulunamadı."));
            }

            return results;
        }
    }
}
