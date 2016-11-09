
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
using System.Linq.Expressions;
using JuCheap.Service.Dto;

namespace JuCheap.Service.Abstracts
{ 
	/// <summary>
    /// LoginLog业务契约
    /// </summary>
    public partial interface ILoginLogService
    {
		/// <summary>
		/// 添加loginlog
		/// </summary>
		/// <param name="loginlog">loginlog实体</param>
		/// <returns></returns>
		bool Add(LoginLogDto loginlog);

		/// <summary>
        /// 批量添加loginlog
        /// </summary>
        /// <param name="models">loginlog集合</param>
        /// <returns></returns>
        bool Add(List<LoginLogDto> models);

		/// <summary>
		/// 编辑loginlog
		/// </summary>
		/// <param name="loginlog">实体</param>
		/// <returns></returns>
		bool Update(LoginLogDto loginlog);

		/// <summary>
		/// 批量更新loginlog
		/// </summary>
		/// <param name="loginlogs">loginlog实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<LoginLogDto> loginlogs);

		/// <summary>
		/// 删除loginlog
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除loginlog
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<LoginLogDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 loginlog 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        LoginLogDto GetOne(Expression<Func<LoginLogDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 loginlog
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<LoginLogDto> Query<OrderKeyType>(Expression<Func<LoginLogDto, bool>> exp, Expression<Func<LoginLogDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取loginlog
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<LoginLogDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<LoginLogDto, bool>> exp, Expression<Func<LoginLogDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取loginlog
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<LoginLogDto> GetWithPages(QueryBase queryBase, Expression<Func<LoginLogDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
