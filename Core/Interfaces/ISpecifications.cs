using System.Linq.Expressions;

namespace Core.Interfaces;
public interface ISpecifications<T> where T : class
{
    public Expression<Func<T, bool>> WhereCriteria { get; set; }
    public List<Expression<Func<T, object>>> IncludesCriteria { get; set; }
}