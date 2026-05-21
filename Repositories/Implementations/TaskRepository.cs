using Microsoft.EntityFrameworkCore;
using TaskManagement.Data;
using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;

namespace TaskManagement.Repositories.Implementations;


public class TaskRepository : ITaskRepository
{
    private readonly AppDbContext _context;

    public TaskRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync()
    {
        return await _context.Tasks
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _context.Tasks
            .AsNoTracking()
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<TaskItem> AddAsync(TaskItem entity)
    {
        entity.CreatedAt = DateTime.UtcNow;
        await _context.Tasks.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TaskItem> UpdateAsync(TaskItem entity)
    {
        _context.Tasks.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _context.Tasks.FindAsync(id);
        if (entity is not null)
        {
            _context.Tasks.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
