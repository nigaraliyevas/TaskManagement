using TaskManagement.Models;
using TaskManagement.Repositories.Interfaces;
using TaskManagement.Services.Interfaces;

namespace TaskManagement.Services.Implementations;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;

    public TaskService(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskItem>> GetAllAsync(string sortBy = "priority", string sortOrder = "asc")
    {
        var tasks = await _repository.GetAllAsync();
        return ApplySorting(tasks, sortBy, sortOrder);
    }

    public async Task<TaskItem?> GetByIdAsync(int id)
    {
        return await _repository.GetByIdAsync(id);
    }

    public async Task<TaskItem> CreateAsync(TaskItem task)
    {
        return await _repository.AddAsync(task);
    }

    public async Task<TaskItem> UpdateAsync(TaskItem task)
    {
        return await _repository.UpdateAsync(task);
    }

    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

    //  Helpers 

    private static IEnumerable<TaskItem> ApplySorting(
        IEnumerable<TaskItem> tasks, string sortBy, string sortOrder)
    {
        return (sortBy.ToLower(), sortOrder.ToLower()) switch
        {
            ("priority", "asc")  => tasks.OrderBy(t => (int)t.Priority),
            ("priority", "desc") => tasks.OrderByDescending(t => (int)t.Priority),
            ("deadline", "asc")  => tasks.OrderBy(t => t.Deadline ?? DateTime.MaxValue),
            ("deadline", "desc") => tasks.OrderByDescending(t => t.Deadline ?? DateTime.MinValue),
            _                    => tasks.OrderBy(t => (int)t.Priority)
        };
    }
}
