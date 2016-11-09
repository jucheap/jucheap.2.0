/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 09/04/2015 11:47:14
* Description: Automated building by service@JuCheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using JuCheap.Data;

namespace JuCheap.Service.Data
{
    /// <summary>
    /// 数据初始化
    /// </summary>
    public class DbInitService
    {
        /// <summary>
        /// 数据初始化
        /// </summary>
        public static void Init()
        {
            InitData.Init();
        }
    }
}
