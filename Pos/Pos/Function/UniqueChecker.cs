using Microsoft.EntityFrameworkCore;
using Pos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Pos.Function
{
    internal class UniqueChecker
    {
            public bool IsUnique<T>(string propertyName, string value) where T : class
            {
                using (var _context = new AppDbContext())
                {
                    var dbSet = _context.Set<T>();

                    var parameter = Expression.Parameter(typeof(T), "e");
                    var property = Expression.Property(parameter, propertyName);
                    var valueExpression = Expression.Constant(value);

                    var equalityExpression = Expression.Equal(property, valueExpression);
                    var lambda = Expression.Lambda<Func<T, bool>>(equalityExpression, parameter);

                    return dbSet.Any(lambda);
                }
            }
    }
}
