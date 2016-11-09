
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
    /// EmailReceiver业务契约
    /// </summary>
    public partial class EmailReceiverService : ServiceBase<EmailReceiverEntity>, IDependency, IEmailReceiverService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public EmailReceiverService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IEmailReceiverService 接口实现

		/// <summary>
		/// 添加emailreceiver
		/// </summary>
		/// <param name="dto">emailreceiver实体</param>
		/// <returns></returns>
		public bool Add(EmailReceiverDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<EmailReceiverDto, EmailReceiverEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加emailreceiver
        /// </summary>
        /// <param name="dtos">emailreceiver集合</param>
        /// <returns></returns>
        public bool Add(List<EmailReceiverDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<EmailReceiverDto>, List<EmailReceiverEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑emailreceiver
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(EmailReceiverDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<EmailReceiverDto, EmailReceiverEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新emailreceiver
		/// </summary>
		/// <param name="dtos">emailreceiver实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<EmailReceiverDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<EmailReceiverDto>, IEnumerable<EmailReceiverEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除emailreceiver
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
        /// 批量删除emailreceiver
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<EmailReceiverDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailReceiverDto, EmailReceiverEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 emailreceiver 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public EmailReceiverDto GetOne(Expression<Func<EmailReceiverDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailReceiverDto, EmailReceiverEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<EmailReceiverEntity, EmailReceiverDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 emailreceiver
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<EmailReceiverDto> Query<OrderKeyType>(Expression<Func<EmailReceiverDto, bool>> exp, Expression<Func<EmailReceiverDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailReceiverDto, EmailReceiverEntity, bool>();
				var order = orderExp.Cast<EmailReceiverDto, EmailReceiverEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<EmailReceiverEntity>, List<EmailReceiverDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取EmailReceiver
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<EmailReceiverDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<EmailReceiverDto, bool>> exp, Expression<Func<EmailReceiverDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailReceiverDto, EmailReceiverEntity, bool>();
				var order = orderExp.Cast<EmailReceiverDto, EmailReceiverEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<EmailReceiverDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<EmailReceiverEntity>, List<EmailReceiverDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取EmailReceiver
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<EmailReceiverDto> GetWithPages(QueryBase queryBase, Expression<Func<EmailReceiverDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<EmailReceiverDto, EmailReceiverEntity, bool>();
				//var order = orderExp.Cast<EmailReceiverDto, EmailReceiverEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<EmailReceiverDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<EmailReceiverEntity>, List<EmailReceiverDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
