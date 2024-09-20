namespace Core.Interfaces;
public interface IGenericRepository<T> where T : class
{
    //CRUD Opretions Just To Deal With DB It So Simple All Hard Work In Service
    public Task<IReadOnlyList<T>> GetAllAsync();
    Task<IReadOnlyList<T>> GetAllAsync(ISpecifications<T> spec);

    public Task<T?> GetEntityAsync(ISpecifications<T> spec);
    public Task<T?> GetAsync(int id);

    public Task AddAsync(T entity);
    public void Update(T entity);
    public void Delete(T entity);
}