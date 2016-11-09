
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
    /// EmailPool业务契约
    /// </summary>
    public partial class EmailPoolService : ServiceBase<EmailPoolEntity>, IDependency, IEmailPoolService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public EmailPoolService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IEmailPoolService 接口实现

		/// <summary>
		/// 添加emailpool
		/// </summary>
		/// <param name="dto">emailpool实体</param>
		/// <returns></returns>
		public bool Add(EmailPoolDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<EmailPoolDto, EmailPoolEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加emailpool
        /// </summary>
        /// <param name="dtos">emailpool集合</param>
        /// <returns></returns>
        public bool Add(List<EmailPoolDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<EmailPoolDto>, List<EmailPoolEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑emailpool
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(EmailPoolDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<EmailPoolDto, EmailPoolEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新emailpool
		/// </summary>
		/// <param name="dtos">emailpool实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<EmailPoolDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<EmailPoolDto>, IEnumerable<EmailPoolEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除emailpool
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
        /// 批量删除emailpool
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<EmailPoolDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailPoolDto, EmailPoolEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 emailpool 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public EmailPoolDto GetOne(Expression<Func<EmailPoolDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailPoolDto, EmailPoolEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<EmailPoolEntity, EmailPoolDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 emailpool
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<EmailPoolDto> Query<OrderKeyType>(Expression<Func<EmailPoolDto, bool>> exp, Expression<Func<EmailPoolDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailPoolDto, EmailPoolEntity, bool>();
				var order = orderExp.Cast<EmailPoolDto, EmailPoolEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<EmailPoolEntity>, List<EmailPoolDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取EmailPool
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<EmailPoolDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<EmailPoolDto, bool>> exp, Expression<Func<EmailPoolDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailPoolDto, EmailPoolEntity, bool>();
				var order = orderExp.Cast<EmailPoolDto, EmailPoolEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<EmailPoolDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<EmailPoolEntity>, List<EmailPoolDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取EmailPool
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<EmailPoolDto> GetWithPages(QueryBase queryBase, Expression<Func<EmailPoolDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailPoolDto, EmailPoolEntity, bool>();
				//var order = orderExp.Cast<EmailPoolDto, EmailPoolEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<EmailPoolDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<EmailPoolEntity>, List<EmailPoolDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
