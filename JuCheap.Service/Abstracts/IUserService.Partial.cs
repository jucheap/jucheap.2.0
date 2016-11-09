
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
    /// User业务契约
    /// </summary>
    public partial interface IUserService
    {
		/// <summary>
		/// 添加user
		/// </summary>
		/// <param name="user">user实体</param>
		/// <returns></returns>
		bool Add(UserDto user);

		/// <summary>
        /// 批量添加user
        /// </summary>
        /// <param name="models">user集合</param>
        /// <returns></returns>
        bool Add(List<UserDto> models);

		/// <summary>
		/// 编辑user
		/// </summary>
		/// <param name="user">实体</param>
		/// <returns></returns>
		bool Update(UserDto user);

		/// <summary>
		/// 批量更新user
		/// </summary>
		/// <param name="users">user实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<UserDto> users);

		/// <summary>
		/// 删除user
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除user
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<UserDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 user 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        UserDto GetOne(Expression<Func<UserDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 user
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<UserDto> Query<OrderKeyType>(Expression<Func<UserDto, bool>> exp, Expression<Func<UserDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取user
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<UserDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<UserDto, bool>> exp, Expression<Func<UserDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取user
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<UserDto> GetWithPages(QueryBase queryBase, Expression<Func<UserDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
