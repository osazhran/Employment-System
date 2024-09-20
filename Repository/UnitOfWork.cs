using Core.Interfaces;
using Core.Interfaces.Repositories;
using Repository.Identity;
using System.Collections.Concurrent;

namespace Repository;
public class UnitOfWork(IdentityContext _identityContext) : IUnitOfWork
{
    private readonly ConcurrentDictionary<string, object> _repositories = new();

    public IGenericRepository<T> Repository<T>() where T : class
    {
        var key = typeof(T).Name;

        return (IGenericRepository<T>)_repositories.GetOrAdd(key, _ => new GenericRepository<T>(_identityContext));
    }

    public async Task<int> CompleteAsync() => await _identityContext.SaveChangesAsync();

    public void Dispose() => _identityContext.DisposeAsync();

    public async ValueTask DisposeAsync() => await _identityContext.DisposeAsync();
}