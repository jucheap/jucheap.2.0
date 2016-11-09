
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
    /// EmailReceiver业务契约
    /// </summary>
    public partial interface IEmailReceiverService
    {
		/// <summary>
		/// 添加emailreceiver
		/// </summary>
		/// <param name="emailreceiver">emailreceiver实体</param>
		/// <returns></returns>
		bool Add(EmailReceiverDto emailreceiver);

		/// <summary>
        /// 批量添加emailreceiver
        /// </summary>
        /// <param name="models">emailreceiver集合</param>
        /// <returns></returns>
        bool Add(List<EmailReceiverDto> models);

		/// <summary>
		/// 编辑emailreceiver
		/// </summary>
		/// <param name="emailreceiver">实体</param>
		/// <returns></returns>
		bool Update(EmailReceiverDto emailreceiver);

		/// <summary>
		/// 批量更新emailreceiver
		/// </summary>
		/// <param name="emailreceivers">emailreceiver实体集合</param>
		/// <returns></returns>
		bool Update(IEnumerable<EmailReceiverDto> emailreceivers);

		/// <summary>
		/// 删除emailreceiver
		/// </summary>
		/// <param name="id">Id</param>
		/// <returns></returns>
		bool Delete(int id);

		/// <summary>
        /// 批量删除emailreceiver
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        bool Delete(Expression<Func<EmailReceiverDto, bool>> exp);

		/// <summary>
        ///  获取单条符合条件的 emailreceiver 数据
        /// </summary>
        /// <param name="exp">条件表达式</param>
        /// <returns></returns>
        EmailReceiverDto GetOne(Expression<Func<EmailReceiverDto, bool>> exp);

		/// <summary>
        /// 查询符合调价的 emailreceiver
        /// </summary>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        List<EmailReceiverDto> Query<OrderKeyType>(Expression<Func<EmailReceiverDto, bool>> exp, Expression<Func<EmailReceiverDto, OrderKeyType>> orderExp, bool isDesc = true);

		/// <summary>
        /// 分页获取emailreceiver
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderExp">排序条件</param>
		/// <param name="isDesc">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<EmailReceiverDto> GetWithPages<OrderKeyType>(QueryBase queryBase, Expression<Func<EmailReceiverDto, bool>> exp, Expression<Func<EmailReceiverDto, OrderKeyType>> orderExp, bool isDesc = true);

        /// <summary>
        /// 分页获取emailreceiver
        /// </summary>
        /// <param name="queryBase">QueryBase</param>
		/// <param name="exp">过滤条件</param>
		/// <param name="orderBy">排序条件</param>
		/// <param name="orderDir">是否是降序排列</param>
        /// <returns></returns>
        ResultDto<EmailReceiverDto> GetWithPages(QueryBase queryBase, Expression<Func<EmailReceiverDto, bool>> exp, string orderBy, string orderDir = "desc");
    } 
}
