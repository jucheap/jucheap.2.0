
/*******************************************************************************
* Copyright (C)  JuCheap.Com
* 
* Author: dj.wong
* Create Date: 04/20/2016 18:42:18
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Linq.Expressions;
using EntityFramework.Extensions;
using AutoMapper;
using JuCheap.Core;
using JuCheap.Core.Extentions;
using JuCheap.Entity;
using JuCheap.Service.Dto;
using Mehdime.Entity;

namespace JuCheap.Service.Abstracts
{ 
	/// <summary>
    /// LoginLog业务契约
    /// </summary>
    public partial class LoginLogService : ServiceBase<LoginLogEntity>, IDependency, ILoginLogService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public LoginLogService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region ILoginLogService 接口实现

		/// <summary>
		/// 添加loginlog
		/// </summary>
		/// <param name="dto">loginlog实体</param>
		/// <returns></returns>
		public bool Add(LoginLogDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<LoginLogDto, LoginLogEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加loginlog
        /// </summary>
        /// <param name="dtos">loginlog集合</param>
        /// <returns></returns>
        public bool Add(List<LoginLogDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<LoginLogDto>, List<LoginLogEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑loginlog
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(LoginLogDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<LoginLogDto, LoginLogEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新loginlog
		/// </summary>
		/// <param name="dtos">loginlog实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<LoginLogDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<LoginLogDto>, IEnumerable<LoginLogEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除loginlog
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		public bool Delete(int id)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);

                var model = dbSet.FirstOrDefault(item => item.Id == id);
                dbSet.Remove(model);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        /// 批量删除loginlog
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<LoginLogDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<LoginLogDto, LoginLogEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 loginlog 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public LoginLogDto GetOne(Expression<Func<LoginLogDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<LoginLogDto, LoginLogEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<LoginLogEntity, LoginLogDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 loginlog
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<LoginLogDto> Query<OrderKeyType>(Expression<Func<LoginLogDto, bool>> exp, Expression<Func<LoginLogDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<LoginLogDto, LoginLogEntity, bool>();
				var order = orderExp.Cast<LoginLogDto, LoginLogEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<LoginLogEntity>, List<LoginLogDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取LoginLog
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<LoginLogDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<LoginLogDto, bool>> exp, Expression<Func<LoginLogDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<LoginLogDto, LoginLogEntity, bool>();
				var order = orderExp.Cast<LoginLogDto, LoginLogEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<LoginLogDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<LoginLogEntity>, List<LoginLogDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取LoginLog
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<LoginLogDto> GetWithPages(QueryBase queryBase, Expression<Func<LoginLogDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<LoginLogDto, LoginLogEntity, bool>();
				//var order = orderExp.Cast<LoginLogDto, LoginLogEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<LoginLogDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<LoginLogEntity>, List<LoginLogDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
