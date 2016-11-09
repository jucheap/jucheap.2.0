/*******************************************************************************
* Copyright (C) JuCheap.Com
* 
* Author: dj.wong
* Create Date: 2016/2/22
* Description: Automated building by service@jucheap.com 
* 
* Revision History:
* Date         Author               Description
*
*********************************************************************************/

using Memcached.ClientLibrary;

namespace JuCheap.Core.Memcached
{
    /// <summary>
    /// MemcachedClient单利模式
    /// </summary>
    public class MemcachedClientSingleton : Singleton<MemcachedClient>
    {
        private MemcachedClientSingleton()
        {
            
        }
    }
}
