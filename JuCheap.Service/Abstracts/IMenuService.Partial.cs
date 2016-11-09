
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
    /// Menu业务契约
    /// </summary>
    public partial interface IMenuService
    {
		/// <summary>
		/// 添加menu
		/// </summary>
		/// <param name="menu">menu实体</param>
		/// <returns></returns>
		bool Add(MenuDto menu);

		/// <summary>
        /// 批量添加menu
        /// </summary>
        /// <param name="models">menu集合</param>
        /// <returns></returns>
        bool Add(List<MenuDto> models);

		/// <summary>
		/// 编辑menu
		/// </summary>
		/// <param name="menu">实体</param>
		/// <returns></returns>
		bool Update(MenuDto menu);

		/// <summary>
		/// 批量更新menu
		/// </summary>
		/// <param name="menus">menu实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<MenuDto> menus);

		/// <summary>
		/// 删除menu
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除menu
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<MenuDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 menu 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        MenuDto GetOne(Expression<Func<MenuDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 menu
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<MenuDto> Query<OrderKeyType>(Expression<Func<MenuDto, bool>> exp, Expression<Func<MenuDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取menu
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<MenuDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<MenuDto, bool>> exp, Expression<Func<MenuDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取menu
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<MenuDto> GetWithPages(QueryBase queryBase, Expression<Func<MenuDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
