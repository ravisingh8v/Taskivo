using System.Threading;
namespace Taskivo_Commands;

public interface ICommandHandler<TCommand, TResult>
{
    System.Threading.Tasks.Task<TResult> Handle(TCommand command, CancellationToken cancellationToken = default);
}   