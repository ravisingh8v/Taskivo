using Taskivo_Common.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_Common.Responses;
using Taskivo_DTO;
using Taskivo_Common.Authorization;

namespace Taskivo_Controller.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TasksController : ControllerBase
{
    private readonly ITaskService _taskService;

    public TasksController(ITaskService taskService)
    {
        _taskService = taskService;
    }

    [HttpGet]
    [Authorize(Policy = PermissionNames.TasksRead)]
    [ProducesResponseType(typeof(ApiResponse<List<TaskDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<List<TaskDto>>>> GetAll()
    {
        var userId = User.GetCurrentUserId();
        var tasks = await _taskService.GetAllTasksAsync(userId);

        return Ok(ApiResponse.Success(tasks));
    }

    [HttpGet("{id}")]
    [Authorize(Policy = PermissionNames.TasksRead)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<TaskDto>>> GetById(Guid id)
    {
        var userId = User.GetCurrentUserId();
        var task = await _taskService.GetTaskByIdAsync(id, userId);

        if (task is null)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success(task, "Task retrieved successfully."));
    }

    [HttpPost]
    [Authorize(Policy = PermissionNames.TasksCreate)]
    [ProducesResponseType(typeof(ApiResponse<Guid>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<Guid>>> Create([FromBody] CreateTaskDto request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetCurrentUserId();
        var taskId = await _taskService.CreateTaskAsync(request, userId, cancellationToken);

        return CreatedAtAction(
            nameof(GetById),
            new { id = taskId },
            ApiResponse.Success(taskId, "Task created successfully."));
    }

    [HttpPut("{id}")]
    [Authorize(Policy = PermissionNames.TasksUpdate)]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<TaskDto>>> Update(Guid id, [FromBody] UpdateTaskDto request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetCurrentUserId();
        var updatedTask = await _taskService.UpdateTaskAsync(id, request, userId, cancellationToken);

        if (updatedTask is null)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success(updatedTask, "Task updated successfully."));
    }

    [HttpPatch("{id}/status")]
    [Authorize(Policy = PermissionNames.TasksUpdate)]
    [ProducesResponseType(typeof(ApiResponse<TaskDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<TaskDto>>> UpdateStatus(Guid id, [FromBody] UpdateTaskStatusDto request, CancellationToken cancellationToken = default)
    {
        var userId = User.GetCurrentUserId();
        var updatedTask = await _taskService.UpdateTaskStatusAsync(id, request, userId, cancellationToken);

        if (updatedTask is null)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success(updatedTask, "Task status updated successfully."));
    }

    [HttpDelete("{id}")]
    [Authorize(Policy = PermissionNames.TasksDelete)]
    [ProducesResponseType(typeof(ApiResponse<object?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var userId = User.GetCurrentUserId();
        var deleted = await _taskService.DeleteTaskAsync(id, userId, cancellationToken);

        if (!deleted)
        {
            return NotFound(ApiResponse.Error("Task not found."));
        }

        return Ok(ApiResponse.Success<object?>(null, "Task deleted successfully."));
    }
}
