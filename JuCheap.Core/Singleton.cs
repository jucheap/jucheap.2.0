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

using System;
using System.Linq;
using System.Reflection;

namespace JuCheap.Core
{
    /// <summary>
    /// 单例
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class Singleton<T> where T : new()
    {
        private static readonly Lazy<T> _instance
            = new Lazy<T>(() =>
            {
                var ctors = typeof (T).GetConstructors(
                    BindingFlags.Instance
                    | BindingFlags.NonPublic
                    | BindingFlags.Public);
                if (ctors.Count() != 1)
                    throw new InvalidOperationException(string.Format("Type {0} must have exactly one constructor.",
                        typeof (T)));
                var ctor = ctors.SingleOrDefault(c => c.GetParameters().Count() == 0 && c.IsPrivate);
                if (ctor == null)
                    throw new InvalidOperationException(
                        string.Format("The constructor for {0} must be private and take no parameters.", typeof (T)));
                return (T) ctor.Invoke(null);
            });

        /// <summary>
        /// 实例
        /// </summary>
        public static T Instance
        {
            get { return _instance.Value; }
        }
    }
}
