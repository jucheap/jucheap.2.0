
/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 02/17/2016 17:07:54
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using JuCheap.Core.Extentions;
using JuCheap.Data;
using Mehdime.Entity;

namespace JuCheap.Service.Abstracts
{
    public abstract class ServiceBase<T> where T: class
    {
        #region Private

        /// <summary>
        /// 获取DB
        /// </summary>
        /// <param name="scope">上下文</param>
        /// <returns></returns>
	    protected DbContext GetDb(IDbContextScope scope)
        {
            return scope.DbContexts.Get<JuCheapContext>();
        }

        /// <summary>
        /// 获取只读DB
        /// </summary>
        /// <param name="scope">上下文</param>
        /// <returns></returns>
        protected DbContext GetDb(IDbContextReadOnlyScope scope)
        {
            return scope.DbContexts.Get<JuCheapContext>();
        }

        /// <summary>
        /// 获取DbSet
        /// </summary>
        /// <param name="db"></param>
        /// <returns></returns>
	    protected DbSet<T> GetDbSet(DbContext db)
        {
            return db.Set<T>();
        }

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <typeparam name="OrderKeyType"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="exp"></param>
        /// <param name="orderExp"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        protected IQueryable<T> GetQuery<OrderKeyType>(DbSet<T> dbSet, Expression<Func<T, bool>> exp,
            Expression<Func<T, OrderKeyType>> orderExp, bool isDesc = true)
        {
            var query = dbSet.AsNoTracking().OrderByDescending(orderExp).Where(exp);
            if (!isDesc)
                query = dbSet.AsNoTracking().OrderBy(orderExp).Where(exp);
            return query;
        }

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="exp"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        protected IQueryable<T> GetQuery(DbSet<T> dbSet, Expression<Func<T, bool>> exp,
            string orderBy, string orderDir)
        {
            var query = dbSet.AsNoTracking().OrderBy(orderBy, orderDir).Where(exp);
            return query;
        }

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="OrderKeyType"></typeparam>
        /// <typeparam name="IncludeKeyType"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="includeExp"></param>
        /// <param name="exp"></param>
        /// <param name="orderExp"></param>
        /// <param name="isDesc"></param>
        /// <returns></returns>
        protected IQueryable<T> GetQuery<IncludeKeyType, OrderKeyType>(DbSet<T> dbSet, Expression<Func<T, IncludeKeyType>> includeExp, Expression<Func<T, bool>> exp,
            Expression<Func<T, OrderKeyType>> orderExp, bool isDesc = true)
        {
            var query = dbSet.AsNoTracking().Include(includeExp).OrderByDescending(orderExp).Where(exp);
            if (!isDesc)
                query = dbSet.AsNoTracking().Include(includeExp).OrderBy(orderExp).Where(exp);
            return query;
        }

        /// <summary>
        /// 获取IQueryable对象
        /// </summary>
        /// <typeparam name="IncludeKeyType"></typeparam>
        /// <param name="dbSet"></param>
        /// <param name="includeExp"></param>
        /// <param name="exp"></param>
        /// <param name="orderBy"></param>
        /// <param name="orderDir"></param>
        /// <returns></returns>
        protected IQueryable<T> GetQuery<IncludeKeyType>(DbSet<T> dbSet, Expression<Func<T, IncludeKeyType>> includeExp, Expression<Func<T, bool>> exp,
            string orderBy, string orderDir)
        {
            var query = dbSet.AsNoTracking().Include(includeExp).OrderBy(orderBy, orderDir).Where(exp);
            return query;
        }

        #endregion
    }
}
