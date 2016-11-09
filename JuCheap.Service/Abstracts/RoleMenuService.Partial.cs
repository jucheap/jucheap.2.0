
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
    /// RoleMenu业务契约
    /// </summary>
    public partial class RoleMenuService : ServiceBase<RoleMenuEntity>, IDependency, IRoleMenuService
    {
		#region 构造函数注册上下文
		public IDbContextScopeFactory _dbScopeFactory {get;set;}

        //private readonly IDbContextScopeFactory _dbScopeFactory;

        //public RoleMenuService(IDbContextScopeFactory dbScopeFactory)
        //{
        //    _dbScopeFactory = dbScopeFactory;
        //}

        #endregion

		#region IRoleMenuService 接口实现

		/// <summary>
		/// 添加rolemenu
		/// </summary>
		/// <param name="dto">rolemenu实体</param>
		/// <returns></returns>
		public bool Add(RoleMenuDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<RoleMenuDto, RoleMenuEntity>(dto);
                dbSet.Add(entity);
                var count = db.SaveChanges();
                return count > 0;
            }
		}

		/// <summary>
        /// 批量添加rolemenu
        /// </summary>
        /// <param name="dtos">rolemenu集合</param>
        /// <returns></returns>
        public bool Add(List<RoleMenuDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<List<RoleMenuDto>, List<RoleMenuEntity>>(dtos);
                dbSet.AddRange(entities);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 编辑rolemenu
		/// </summary>
		/// <param name="dto">实体</param>
		/// <returns></returns>
		public bool Update(RoleMenuDto dto)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entity = Mapper.Map<RoleMenuDto, RoleMenuEntity>(dto);
                dbSet.AddOrUpdate(entity);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 批量更新rolemenu
		/// </summary>
		/// <param name="dtos">rolemenu实体集合</param>
		/// <returns></returns>
		public bool Update(IEnumerable<RoleMenuDto> dtos)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var entities = Mapper.Map<IEnumerable<RoleMenuDto>, IEnumerable<RoleMenuEntity>>(dtos);
                dbSet.AddOrUpdate(entities.ToArray());
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
		/// 删除rolemenu
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
        /// 批量删除rolemenu
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public bool Delete(Expression<Func<RoleMenuDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.Create())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<RoleMenuDto, RoleMenuEntity, bool>();
				
                var models = dbSet.Where(where);
                dbSet.RemoveRange(models);
                return db.SaveChanges() > 0;
            }
		}

		/// <summary>
        ///  获取单条符合条件的 rolemenu 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        public RoleMenuDto GetOne(Expression<Func<RoleMenuDto, bool>> exp)
		{
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<RoleMenuDto, RoleMenuEntity, bool>();
                var entity = dbSet.AsNoTracking().FirstOrDefault(where);

				return Mapper.Map<RoleMenuEntity, RoleMenuDto>(entity);
            }
		}

		/// <summary>
        /// 查询符合调价的 rolemenu
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public List<RoleMenuDto> Query<OrderKeyType>(Expression<Func<RoleMenuDto, bool>> exp, Expression<Func<RoleMenuDto, OrderKeyType>> orderExp, bool isDesc = true)
		{
            using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<RoleMenuDto, RoleMenuEntity, bool>();
				var order = orderExp.Cast<RoleMenuDto, RoleMenuEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);
				var list = query.ToList();
				return Mapper.Map<List<RoleMenuEntity>, List<RoleMenuDto>>(list);
            }
		}

		/// <summary>
        /// 分页获取RoleMenu
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        public ResultDto<RoleMenuDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<RoleMenuDto, bool>> exp, Expression<Func<RoleMenuDto, OrderKeyType>> orderExp, bool isDesc = true)
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<RoleMenuDto, RoleMenuEntity, bool>();
				var order = orderExp.Cast<RoleMenuDto, RoleMenuEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, order, isDesc);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<RoleMenuDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<RoleMenuEntity>, List<RoleMenuDto>>(list)
                };
				return dto;
            }
        }

		/// <summary>
        /// 分页获取RoleMenu
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">排序类型：desc(默认)/asc</param>
        /// <returns></returns>
        public ResultDto<RoleMenuDto> GetWithPages(QueryBase queryBase, Expression<Func<RoleMenuDto, bool>> exp, string orderBy, string orderDir = "desc")
        {
			using (var scope = _dbScopeFactory.CreateReadOnly())
            {
                var db = GetDb(scope);
                var dbSet = GetDbSet(db);
				var where = exp.Cast<RoleMenuDto, RoleMenuEntity, bool>();
				//var order = orderExp.Cast<RoleMenuDto, RoleMenuEntity, OrderKeyType>();
				var query = GetQuery(dbSet, where, orderBy, orderDir);

                var query_count = query.FutureCount();
                var query_list = query.Skip(queryBase.Start).Take(queryBase.Length).Future();
				var list = query_list.ToList();

                var dto = new ResultDto<RoleMenuDto>
				{
					recordsTotal = query_count.Value,
					data = Mapper.Map<List<RoleMenuEntity>, List<RoleMenuDto>>(list)
                };
				return dto;
            }
        }

		#endregion
    } 
}
