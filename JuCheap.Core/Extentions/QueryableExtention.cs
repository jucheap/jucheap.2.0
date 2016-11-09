using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace JuCheap.Core.Extentions
{
    public static class QueryableExtention
    {
        /// <summary>
        /// 排序方法扩展
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">数据源</param>
        /// <param name="orderBy">排序字段</param>
        /// <param name="orderDir">排序方向</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string orderBy, string orderDir)
        {
            var isDesc = orderDir.ToLower().Equals("desc");
            var property = typeof(T).GetProperty(orderBy);
            var propertyType = property.PropertyType;
            if (propertyType == typeof(string))
            {
                var orderExp = GetKeySelector<T, string>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(int))
            {
                var orderExp = GetKeySelector<T, int>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(decimal))
            {
                var orderExp = GetKeySelector<T, decimal>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(DateTime))
            {
                var orderExp = GetKeySelector<T, DateTime>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(double))
            {
                var orderExp = GetKeySelector<T, double>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(bool))
            {
                var orderExp = GetKeySelector<T, bool>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            if (propertyType == typeof(byte))
            {
                var orderExp = GetKeySelector<T, byte>(property);
                return isDesc ? source.OrderByDescending(orderExp) : source.OrderBy(orderExp);
            }
            return default(IOrderedQueryable<T>);
        }

        #region Private

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="R"></typeparam>
        /// <param name="property"></param>
        /// <returns></returns>
        static Expression<Func<T, R>> GetKeySelector<T, R>(PropertyInfo property)
        {
            Type type = typeof(T);
            ParameterExpression param = Expression.Parameter(type);
            Expression propertyAccess = param;
            propertyAccess = Expression.MakeMemberAccess(propertyAccess, property);
            var keySelector = Expression.Lambda<Func<T, R>>(propertyAccess, param);
            return keySelector;
        }

        #endregion
    }
}
