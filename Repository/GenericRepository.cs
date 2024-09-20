using Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Identity;

namespace Repository;
public class GenericRepository<T> (IdentityContext _identityContext) : IGenericRepository<T> where T : class
{
    //GetAll(R)  && GetAllWith Spec
    public async Task<IReadOnlyList<T>> GetAllAsync() => await _identityContext.Set<T>().ToListAsync();
    public async Task<IReadOnlyList<T>> GetAllAsync(ISpecifications<T> spec)
        => await SpecificationsEvaluator<T>.GetQuery(_identityContext.Set<T>(), spec).ToListAsync();

    //Get(R)    &&  GetWith Spec
    public async Task<T?> GetAsync(int id) => await _identityContext.Set<T>().FindAsync(id);
    public async Task<T?> GetEntityAsync(ISpecifications<T> spec)
        => await SpecificationsEvaluator<T>.GetQuery(_identityContext.Set<T>(), spec).FirstOrDefaultAsync();

    //CUD
    public async Task AddAsync(T entity) => await _identityContext.Set<T>().AddAsync(entity);
    public void Update(T entity) => _identityContext.Set<T>().Update(entity);
    public void Delete(T entity) => _identityContext.Set<T>().Remove(entity);
}