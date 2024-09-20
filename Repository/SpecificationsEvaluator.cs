using Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Repository;
public class SpecificationsEvaluator<T> where T : class
{
    
    public static IQueryable<T> GetQuery(IQueryable<T> inputQuery, ISpecifications<T> spec)
    {
        var query = inputQuery;

        if (spec.WhereCriteria != null)
            query = query.Where(spec.WhereCriteria);

        query = spec.IncludesCriteria.Aggregate(query, (currentQuery, includeExpression) => currentQuery.Include(includeExpression));

        return query;
    }
}
/*
 الميثود دي المسئوله عن تكوين الكويري النهائي وارجاعه
بتاخد مني اسم ال  دي بي كونتكست بالاضافه الي السبيك
*/