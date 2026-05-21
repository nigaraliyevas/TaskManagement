using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TaskManagement.Models;
using TaskManagement.Services.Interfaces;
using TaskManagement.ViewModels;

namespace TaskManagement.Controllers;

public class TasksController : Controller
{
    private readonly ITaskService _taskService;
    private readonly IMapper      _mapper;

    public TasksController(ITaskService taskService, IMapper mapper)
    {
        _taskService = taskService;
        _mapper      = mapper;
    }

    public async Task<IActionResult> Index(string sortBy = "priority", string sortOrder = "asc")
    {
        var tasks = await _taskService.GetAllAsync(sortBy, sortOrder);
        return View(new TaskListViewModel
        {
            Tasks     = tasks,
            SortBy    = sortBy,
            SortOrder = sortOrder
        });
    }

    public async Task<IActionResult> Details(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        return task is null ? NotFound() : View(task);
    }

    public IActionResult Create() => View(new TaskViewModel());

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(TaskViewModel vm)
    {
        if (!vm.IsValid())
            return View(vm);

        var entity = _mapper.Map<TaskItem>(vm);
        await _taskService.CreateAsync(entity);

        TempData["Success"] = "Task uğurla yaradıldı!";
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Edit(int id)
    {
        var task = await _taskService.GetByIdAsync(id);
        if (task is null) return NotFound();

        var vm = _mapper.Map<TaskViewModel>(task);
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, TaskViewModel vm)
    {
        if (id != vm.Id) return BadRequest();

        if (!vm.IsValid())
            return View(vm);

        var existing = await _taskService.GetByIdAsync(id);
        if (existing is null) return NotFound();

        _mapper.Map(vm, existing);
        await _taskService.UpdateAsync(existing);

        TempData["Success"] = "Task uğurla yeniləndi!";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(int id)
    {
        await _taskService.DeleteAsync(id);
        TempData["Success"] = "Task silindi.";
        return RedirectToAction(nameof(Index));
    }
}
