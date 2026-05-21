using TaskManagement.Models;

namespace TaskManagement.Services.Interfaces;

public interface ITaskService
{
    Task<IEnumerable<TaskItem>> GetAllAsync(string sortBy = "priority", string sortOrder = "asc");
    Task<TaskItem?> GetByIdAsync(int id);
    Task<TaskItem> CreateAsync(TaskItem task);
    Task<TaskItem> UpdateAsync(TaskItem task);
    Task DeleteAsync(int id);
}
