using Microsoft.EntityFrameworkCore;
using Taskivo_Infrastructure.Models;

namespace Taskivo_Infrastructure.Repositories;

public class ReminderRepository: IReminderRepository
{
    public readonly AppDbContext _context;

    public ReminderRepository(AppDbContext context)
    {
        _context = context;
    }

   public async Task AddAsync(ReminderEntity reminder, CancellationToken cancellationToken)
    {
        await _context.Reminders.AddAsync(reminder, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }
    public async Task<List<ReminderEntity>> GetAsync(Guid userId, CancellationToken cancellationToken)
    {
        return  await _context.Reminders.AsNoTracking().Where(r=> r.UserId == userId && r.DeletedAt==null).OrderBy(r=> r.ReminderAt).ToListAsync(cancellationToken);
    }

    public async Task<ReminderEntity?> GetByIdAsync(Guid id, Guid userId, CancellationToken cancellationToken)
    {
        return await _context.Reminders.AsNoTracking().FirstOrDefaultAsync(r=> r.Id == id && r.UserId == userId && r.DeletedAt == null, cancellationToken);
    }
    
    //  public async Task<List<ReminderEntity>> GetDueAsync(DateTime currentTime,CancellationToken cancellationToken)
    // {
    //     return await _context.Reminders
    // }

}