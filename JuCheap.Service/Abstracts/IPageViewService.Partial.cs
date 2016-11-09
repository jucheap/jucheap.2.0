
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
    /// PageView业务契约
    /// </summary>
    public partial interface IPageViewService
    {
		/// <summary>
		/// 添加pageview
		/// </summary>
		/// <param name="pageview">pageview实体</param>
		/// <returns></returns>
		bool Add(PageViewDto pageview);

		/// <summary>
        /// 批量添加pageview
        /// </summary>
        /// <param name="models">pageview集合</param>
        /// <returns></returns>
        bool Add(List<PageViewDto> models);

		/// <summary>
		/// 编辑pageview
		/// </summary>
		/// <param name="pageview">实体</param>
		/// <returns></returns>
		bool Update(PageViewDto pageview);

		/// <summary>
		/// 批量更新pageview
		/// </summary>
		/// <param name="pageviews">pageview实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<PageViewDto> pageviews);

		/// <summary>
		/// 删除pageview
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除pageview
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<PageViewDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 pageview 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        PageViewDto GetOne(Expression<Func<PageViewDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 pageview
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<PageViewDto> Query<OrderKeyType>(Expression<Func<PageViewDto, bool>> exp, Expression<Func<PageViewDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取pageview
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<PageViewDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<PageViewDto, bool>> exp, Expression<Func<PageViewDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取pageview
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<PageViewDto> GetWithPages(QueryBase queryBase, Expression<Func<PageViewDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
