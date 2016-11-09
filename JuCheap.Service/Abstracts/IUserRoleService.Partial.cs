
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
    /// UserRole业务契约
    /// </summary>
    public partial interface IUserRoleService
    {
		/// <summary>
		/// 添加userrole
		/// </summary>
		/// <param name="userrole">userrole实体</param>
		/// <returns></returns>
		bool Add(UserRoleDto userrole);

		/// <summary>
        /// 批量添加userrole
        /// </summary>
        /// <param name="models">userrole集合</param>
        /// <returns></returns>
        bool Add(List<UserRoleDto> models);

		/// <summary>
		/// 编辑userrole
		/// </summary>
		/// <param name="userrole">实体</param>
		/// <returns></returns>
		bool Update(UserRoleDto userrole);

		/// <summary>
		/// 批量更新userrole
		/// </summary>
		/// <param name="userroles">userrole实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<UserRoleDto> userroles);

		/// <summary>
		/// 删除userrole
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除userrole
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<UserRoleDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 userrole 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        UserRoleDto GetOne(Expression<Func<UserRoleDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 userrole
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<UserRoleDto> Query<OrderKeyType>(Expression<Func<UserRoleDto, bool>> exp, Expression<Func<UserRoleDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取userrole
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<UserRoleDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<UserRoleDto, bool>> exp, Expression<Func<UserRoleDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取userrole
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<UserRoleDto> GetWithPages(QueryBase queryBase, Expression<Func<UserRoleDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
