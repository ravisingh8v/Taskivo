
namespace Taskivo_Queries;
public interface IQueryHandler<TQuery, TResult>
{
    Task<TResult> Handle(TQuery query, CancellationToken cancellationToken = default);
}