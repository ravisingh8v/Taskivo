using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_Common.Responses;
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
    [ProducesResponseType(typeof(ApiResponse<List<TaskDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetAll()
    {
        var tasks = await _taskService.GetAllTasksAsync();

        return Ok(ApiResponse.Success(tasks));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<TaskDto>>> GetById(Guid id)
    {
        var task = await _taskService.GetTaskByIdAsync(id);

        if (task is null)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success(task, "Task retrieved successfully."));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<Guid>>> Create([FromBody] CreateTaskDto request, CancellationToken cancellationToken = default)
    {
        var taskId = await _taskService.CreateTaskAsync(request, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = taskId },
            ApiResponse.Success(taskId, "Task created successfully."));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<TaskDto>>> Update(Guid id, [FromBody] UpdateTaskDto request, CancellationToken cancellationToken = default)
    {
        var updatedTask = await _taskService.UpdateTaskAsync(id, request, cancellationToken);

        if (updatedTask is null)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success(updatedTask, "Task updated successfully."));
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var deleted = await _taskService.DeleteTaskAsync(id, cancellationToken);

        if (!deleted)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success<object?>(null, "Task deleted successfully."));
    }
}
