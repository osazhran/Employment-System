using Core.Interfaces;
using System.Linq.Expressions;

namespace Core.Specifications;
public class BaseSpecifications<T> : ISpecifications<T> where T : class
{
    public Expression<Func<T, bool>>? WhereCriteria { get; set; } = default;
    public List<Expression<Func<T, object>>> IncludesCriteria { get; set; } = [];

    public BaseSpecifications(Expression<Func<T, bool>> whereCriteria)
    {
        WhereCriteria = whereCriteria;
    }

    public BaseSpecifications(Expression<Func<T, object>> Include)
    {
        IncludesCriteria.Add(Include);
    }

    public BaseSpecifications(Expression<Func<T, bool>> whereCriteria, Expression<Func<T, object>> Include)
    {
        WhereCriteria = whereCriteria;
        IncludesCriteria.Add(Include);
    }
}
