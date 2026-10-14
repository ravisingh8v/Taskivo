using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Taskivo_AppServices;
using Taskivo_Common.Extensions;
using Taskivo_Common.Responses;
using Taskivo_DTO.Reminder;

namespace Taskivo_Controller;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class RemindersController : ControllerBase
{
    private readonly IReminderService _reminderService;
    public RemindersController(IReminderService reminderService)
    {
        _reminderService = reminderService;
    }
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReminderDto reminder, CancellationToken cancellationToken)
    {
        var userId = User.GetCurrentUserId(); 
       await _reminderService.CreateAsync(reminder, userId, cancellationToken);
       return NoContent();
    }

[HttpGet]
    public async Task<ActionResult<List<ReminderDto>>> GetAll(CancellationToken cancellationToken)
    {
        var userid = User.GetCurrentUserId();
       var reminders= await _reminderService.GetAllAsync(userid, cancellationToken);
       return Ok(ApiResponse.Success(reminders, "Reminders retrieved successfully."));
    }
}