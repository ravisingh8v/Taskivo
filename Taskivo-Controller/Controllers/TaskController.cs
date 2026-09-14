using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_DTO;

namespace Taskivo_Controller.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TaskController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TaskController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();
        return Ok(tasks);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);
        return task is null ? NotFound() : Ok(task);
    }

    [HttpPost]
    public async Task<ActionResult<CreateTaskResponseDto>> Create([FromBody] CreateTaskDto request, CancellationToken cancellationToken = default)
    {
        var taskId = await _taskService.CreateTaskAsync(request, cancellationToken);
        return Created($"/api/task/{taskId}", new { id = taskId });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTaskDto request, CancellationToken cancellationToken = default)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, request, cancellationToken);
        return updatedTask is null ? NotFound() : Ok(updatedTask);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var deleted = await _taskService.DeleteTaskAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }
}
