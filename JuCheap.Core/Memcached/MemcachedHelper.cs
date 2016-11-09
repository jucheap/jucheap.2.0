using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using Memcached.ClientLibrary;

namespace JuCheap.Core.Memcached
{
    /// <summary>
    /// 页 面 名：缓存管理类
    /// 说    明：设置、获取、移除Cache
    /// 作    者：dj.wong
    /// 时    间：2015-12-12
    /// 修 改 者：
    /// 修改时间：
    /// </summary>
    public class MemcachedHelper
    {
        #region 变量和构造函数
        //缓存服务器地址和端口,这样就实现了分布式缓存。服务器可以多个，用Socket读写数据"127.0.0.1:12345",
        private static readonly Dictionary<string, string> Servers = new Dictionary<string, string>();
        private static readonly MemcachedClient mc;
        //服务器缓存
        static Dictionary<string, SockIOPool> Servers1 = new Dictionary<string, SockIOPool>();
        static object LOCK_OBJECT = new object();//安全锁定
        /// <summary>
        /// 静态构造函数
        /// </summary>
        static MemcachedHelper()
        {
            InitServer();
            foreach (var k in Servers.Keys)
            {
                SockIOPool pool = SockIOPool.GetInstance(k);
                string[] s = { Servers[k] };
                pool.SetServers(s);//设置服务器
                pool.MaxConnections = 10000;
                pool.MinConnections = 10;
                pool.SocketConnectTimeout = 1000;
                pool.SocketTimeout = 100;
                pool.Initialize();//初始化缓存线程池
            }
            //默认池
            SockIOPool defaultPool = SockIOPool.GetInstance("DefaultPool");
            defaultPool.SetServers(Servers.Keys.Select(k => Servers[k]).ToArray());//设置服务器
            defaultPool.MaxConnections = 10000;
            defaultPool.MinConnections = 10;
            defaultPool.SocketConnectTimeout = 1000;
            defaultPool.SocketTimeout = 100;
            defaultPool.Initialize(); //初始化默认线程池
            mc = new MemcachedClient {PoolName = "DefaultPool"};
        }
        /// <summary>
        /// 初始化服务器列表，这里默认两台服务器. 
        /// </summary>
        static void InitServer()
        {
            //这里可以写复杂灵活点，动态从配置文件获取，我这里固定了两个
            Servers.Add("Svr1", ConfigurationManager.AppSettings["Svr1"]);
            Servers.Add("Svr2", ConfigurationManager.AppSettings["Svr2"]);
        }
        private MemcachedHelper() { }
        #endregion

        #region 获取客户端
        public static MemcachedClient GetClient(string server)
        {
            MemcachedClient current = MemcachedClientSingleton.Instance;
            current.PoolName = server;
            return current;
        }
        #endregion

        #region 默认


        #region 写(Set)
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Set(string key, object value)
        {
            mc.Set(key, value);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hashCode">哈希码</param>
        public static void Set(string key, object value, int hashCode)
        {
            mc.Set(key, value, hashCode);
        }
        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        public static void Set(string key, object value, DateTime expiry)
        {
            mc.Set(key, value, expiry);
        }

        /// <summary>
        /// 设置数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="hashCode"></param>
        public static void Set(string key, object value, DateTime expiry, int hashCode)
        {
            mc.Set(key, value, expiry, hashCode);
        }
        #endregion

        #region 读(Get)

        #region 返回泛型
        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        public static T Get<T>(string key)
        {
            return (T)mc.Get(key);
        }
        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        public static T Get<T>(string key, int hashCode)
        {
            return (T)mc.Get(key, hashCode);
        }

        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value"></param>
        /// <param name="asString">是否把值作为字符串返回</param>
        public static T Get<T>(string key, object value, bool asString)
        {
            return (T)mc.Get(key, value, asString);
        }
        #endregion

        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        public static object Get(string key)
        {
            return mc.Get(key);
        }
        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        public static object Get(string key, int hashCode)
        {
            return mc.Get(key, hashCode);
        }

        /// <summary>
        /// 读取数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value"></param>
        /// <param name="asString">是否把值作为字符串返回</param>
        public static object Get(string key, object value, bool asString)
        {
            return mc.Get(key, value, asString);
        }
        #endregion

        #region 批量写(Set)

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public static void SetMultiple(string[] keys, object[] values)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                mc.Set(keys[i], values[i]);
            }
        }
        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="hashCodes">哈希码</param>
        public static void SetMultiple(string[] keys, object[] values, int[] hashCodes)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                mc.Set(keys[i], values[i], hashCodes[i]);
            }
        }
        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="expirys">过期时间</param>
        public static void SetMultiple(string[] keys, object[] values, DateTime[] expirys)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                mc.Set(keys[i], values[i], expirys[i]);
            }
        }

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="expirys">过期时间</param>
        /// <param name="hashCodes"></param>
        public static void Set(string[] keys, object[] values, DateTime[] expirys, int[] hashCodes)
        {
            for (int i = 0; i < keys.Length; i++)
            {
                mc.Set(keys[i], values[i], expirys[i], hashCodes[i]);
            }
        }
        #endregion

        #region 批量读取(Multiple),返回哈希表 Hashtable
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        public static Hashtable GetMultiple(string[] keys)
        {
            return mc.GetMultiple(keys);
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static Hashtable GetMultiple(string[] keys, int[] hashCodes)
        {
            return mc.GetMultiple(keys, hashCodes);
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static Hashtable GetMultiple(string[] keys, int[] hashCodes, bool asString)
        {
            return mc.GetMultiple(keys, hashCodes, asString);
        }
        #endregion

        #region 批量读取(Multiple),返回对象数组object[]
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        public static object[] GetMultipleArray(string[] keys)
        {
            return mc.GetMultipleArray(keys);
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static object[] GetMultipleArray(string[] keys, int[] hashCodes)
        {
            return mc.GetMultipleArray(keys, hashCodes);
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static object[] GetMultipleArray(string[] keys, int[] hashCodes, bool asString)
        {
            return mc.GetMultipleArray(keys, hashCodes, asString);
        }
        #endregion

        #region 批量读取(Multiple),返回泛型集合List[T]
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        public static List<T> GetMultipleList<T>(string[] keys)
        {
            object[] obj = mc.GetMultipleArray(keys);
            return obj.Select(o => (T) o).ToList();
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static List<T> GetMultipleList<T>(string[] keys, int[] hashCodes)
        {
            object[] obj = mc.GetMultipleArray(keys, hashCodes);
            return obj.Select(o => (T) o).ToList();
        }
        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static List<T> GetMultipleList<T>(string[] keys, int[] hashCodes, bool asString)
        {
            object[] obj = mc.GetMultipleArray(keys, hashCodes, asString);
            return obj.Select(o => (T) o).ToList();
        }
        #endregion

        #region 替换更新(Replace)
        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void Replace(string key, object value)
        {
            mc.Replace(key, value);
        }
        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hashCode">哈希码</param>
        public static void Replace(string key, object value, int hashCode)
        {
            mc.Replace(key, value, hashCode);
        }
        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        public static void Replace(string key, object value, DateTime expiry)
        {
            mc.Replace(key, value, expiry);
        }

        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="hashCode"></param>
        public static void Replace(string key, object value, DateTime expiry, int hashCode)
        {
            mc.Replace(key, value, expiry, hashCode);
        }
        #endregion

        #region 删除(Delete)

        /// <summary>
        ///删除指定条件缓存
        /// </summary>
        /// <param name="key">键</param>
        public static bool Delete(string key)
        {
            return mc.Delete(key);
        }
        /// <summary>
        /// 删除指定条件缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        /// <param name="expiry">过期时间</param>
        public static bool Delete(string key, int hashCode, DateTime expiry)
        {
            return mc.Delete(key, hashCode, expiry);
        }
        /// <summary>
        /// 删除指定条件缓存
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="expiry">过期时间</param>
        public static bool Delete(string key, DateTime expiry)
        {
            return mc.Delete(key, expiry);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemovAllCache()
        {
            mc.FlushAll();
        }
        /// <summary>
        /// 移除全部缓存
        /// </summary>
        /// <param name="list">移除指定服务器缓存</param>
        public static void RemovAllCache(ArrayList list)
        {
            mc.FlushAll(list);
        }
        #endregion

        #region 是否存在(Exists)
        /// <summary>
        /// 判断指定键的缓存是否存在
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool IsExists(string key)
        {
            return mc.KeyExists(key);
        }
        #endregion

        #region 数值增减

        #region 存储一个数值元素

        /// <summary>
        /// 存储一个数值元素
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public static bool StoreCounter(string key, long counter)
        {
            return mc.StoreCounter(key, counter);
        }

        /// <summary>
        ///  存储一个数值元素
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="counter"></param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static bool StoreCounter(string key, long counter, int hashCode)
        {
            return mc.StoreCounter(key, counter, hashCode);
        }
        #endregion

        #region 获取一个数值元素
        /// <summary>
        /// 获取一个数值元素
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long GetCounter(string key)
        {
            return mc.GetCounter(key);
        }
        /// <summary>
        ///  获取一个数值元素
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long GetCounter(string key, int hashCode)
        {
            return mc.GetCounter(key, hashCode);
        }
        #endregion

        #region 增加一个数值元素的值(Increment)
        /// <summary>
        /// 将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long Increment(string key)
        {
            return mc.Increment(key);
        }
        /// <summary>
        ///  将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <returns></returns>
        public static long Increment(string key, long inc)
        {
            return mc.Increment(key, inc);
        }
        /// <summary>
        ///  将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long Increment(string key, long inc, int hashCode)
        {
            return mc.Increment(key, inc, hashCode);
        }
        #endregion

        #region 减小一个数值元素的值(Decrement)
        /// <summary>
        /// 减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long Decrement(string key)
        {
            return mc.Decrement(key);
        }
        /// <summary>
        ///  减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <returns></returns>
        public static long Decrement(string key, long inc)
        {
            return mc.Decrement(key, inc);
        }
        /// <summary>
        ///  减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long Decrement(string key, long inc, int hashCode)
        {
            return mc.Decrement(key, inc, hashCode);
        }
        #endregion

        #endregion


        #endregion

        #region 指定服务器

        #region 获取(Get)

        /// <summary>
        /// 从指定服务器获取
        /// </summary>
        /// <param name="server">服务器，Svr1，Svr2</param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static object GetFrom(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.Get(key);
        }
        /// <summary>
        /// 从指定服务器获取
        /// </summary>
        /// <param name="server">服务器，Svr1，Svr2</param>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        public static object GetFrom(string server, string key, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            return client.Get(key, hashCode);
        }

        /// <summary>
        /// 从指定服务器获取
        /// </summary>
        /// <param name="server">服务器，Svr1，Svr2</param>
        /// <param name="key">键</param>
        /// <param name="value"></param>
        /// <param name="asString">是否把值作为字符串返回</param>
        public static object GetFrom(string server, string key, object value, bool asString)
        {
            MemcachedClient client = GetClient(server);
            return client.Get(key, value, asString);
        }
        #endregion

        #region 写入(Set)
        /// <summary>
        ///  设置数据缓存
        /// </summary>
        /// <param name="server">服务器，格式为Svr1，Svr2，Svr3，对应配置文件host</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void SetTo(string server, string key, object value)
        {
            MemcachedClient client = GetClient(server);
            client.Set(key, value);
        }
        /// <summary>
        ///  设置数据缓存
        /// </summary>
        /// <param name="server">服务器，格式为Svr1，Svr2，Svr3，对应配置文件host</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hashCode">哈希码</param>
        public static void SetTo(string server, string key, object value, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            client.Set(key, value, hashCode);
        }
        /// <summary>
        ///  设置数据缓存
        /// </summary>
        /// <param name="server">服务器，格式为Svr1，Svr2，Svr3，对应配置文件host</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        public static void SetTo(string server, string key, object value, DateTime expiry)
        {
            MemcachedClient client = GetClient(server);
            client.Set(key, value, expiry);
        }

        /// <summary>
        ///  设置数据缓存
        /// </summary>
        /// <param name="server">服务器，格式为Svr1，Svr2，Svr3，对应配置文件host</param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="hashCode"></param>
        public static void SetTo(string server, string key, object value, DateTime expiry, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            client.Set(key, value, expiry, hashCode);
        }
        #endregion

        #region 批量写(Set)

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys"></param>
        /// <param name="values"></param>
        public static void SetMultipleTo(string server, string[] keys, object[] values)
        {
            MemcachedClient client = GetClient(server);
            for (int i = 0; i < keys.Length; i++)
            {
                client.Set(keys[i], values[i]);
            }
        }

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="hashCodes">哈希码</param>
        /// <param name="server"></param>
        public static void SetMultipleTo(string server, string[] keys, object[] values, int[] hashCodes)
        {
            MemcachedClient client = GetClient(server);
            for (int i = 0; i < keys.Length; i++)
            {
                client.Set(keys[i], values[i], hashCodes[i]);
            }
        }

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="expirys">过期时间</param>
        public static void SetMultipleTo(string server, string[] keys, object[] values, DateTime[] expirys)
        {
            MemcachedClient client = GetClient(server);
            for (int i = 0; i < keys.Length; i++)
            {
                client.Set(keys[i], values[i], expirys[i]);
            }
        }

        /// <summary>
        /// 批量设置数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键</param>
        /// <param name="values">值</param>
        /// <param name="expirys">过期时间</param>
        /// <param name="hashCodes"></param>
        public static void SetMultipleTo(string server, string[] keys, object[] values, DateTime[] expirys, int[] hashCodes)
        {
            MemcachedClient client = GetClient(server);
            for (int i = 0; i < keys.Length; i++)
            {
                client.Set(keys[i], values[i], expirys[i], hashCodes[i]);
            }
        }
        #endregion

        #region 批量读取(Multiple),返回哈希表 Hashtable

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        public static Hashtable GetMultipleFrom(string server, string[] keys)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultiple(keys);
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static Hashtable GetMultipleFrom(string server, string[] keys, int[] hashCodes)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultiple(keys, hashCodes);
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static Hashtable GetMultipleFrom(string server, string[] keys, int[] hashCodes, bool asString)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultiple(keys, hashCodes, asString);
        }
        #endregion

        #region 批量读取(Multiple),返回对象数组object[]

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        public static object[] GetMultipleArrayFrom(string server, string[] keys)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultipleArray(keys);
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static object[] GetMultipleArrayFrom(string server, string[] keys, int[] hashCodes)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultipleArray(keys, hashCodes);
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static object[] GetMultipleArrayFrom(string server, string[] keys, int[] hashCodes, bool asString)
        {
            MemcachedClient client = GetClient(server);
            return client.GetMultipleArray(keys, hashCodes, asString);
        }
        #endregion

        #region 批量读取(Multiple),返回泛型集合List[T]

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        public static List<T> GetMultipleListFrom<T>(string server, string[] keys)
        {
            MemcachedClient client = GetClient(server);
            object[] obj = client.GetMultipleArray(keys);
            return obj.Select(o => (T) o).ToList();
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        public static List<T> GetMultipleListFrom<T>(string server, string[] keys, int[] hashCodes)
        {
            MemcachedClient client = GetClient(server);
            object[] obj = client.GetMultipleArray(keys, hashCodes);
            return obj.Select(o => (T) o).ToList();
        }

        /// <summary>
        /// 批量读取数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="keys">键集合</param>
        /// <param name="hashCodes">哈希码集合</param>
        /// <param name="asString">所有值返回字符</param>
        public static List<T> GetMultipleListFrom<T>(string server, string[] keys, int[] hashCodes, bool asString)
        {
            MemcachedClient client = GetClient(server);
            object[] obj = client.GetMultipleArray(keys, hashCodes, asString);
            return obj.Select(o => (T) o).ToList();
        }
        #endregion

        #region 替换更新(Replace)

        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        public static void ReplaceFrom(string server, string key, object value)
        {
            MemcachedClient client = GetClient(server);
            client.Replace(key, value);
        }

        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="hashCode">哈希码</param>
        public static void ReplaceFrom(string server, string key, object value, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            client.Replace(key, value, hashCode);
        }

        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        public static void ReplaceFrom(string server, string key, object value, DateTime expiry)
        {
            MemcachedClient client = GetClient(server);
            client.Replace(key, value, expiry);
        }

        /// <summary>
        /// 替换更新数据缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="value">值</param>
        /// <param name="expiry">过期时间</param>
        /// <param name="hashCode"></param>
        public static void ReplaceFrom(string server, string key, object value, DateTime expiry, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            client.Replace(key, value, expiry, hashCode);
        }
        #endregion

        #region 删除(Delete)

        ///  <summary>
        /// 删除指定条件缓存
        ///  </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        public static bool DeleteFrom(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.Delete(key);
        }

        /// <summary>
        /// 删除指定条件缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        /// <param name="expiry">过期时间</param>
        public static bool DeleteFrom(string server, string key, int hashCode, DateTime expiry)
        {
            MemcachedClient client = GetClient(server);
            return client.Delete(key, hashCode, expiry);
        }

        /// <summary>
        /// 删除指定条件缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="expiry">过期时间</param>
        public static bool DeleteFrom(string server, string key, DateTime expiry)
        {
            MemcachedClient client = GetClient(server);
            return client.Delete(key, expiry);
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        public static void RemovAllCacheFrom(string server)
        {
            MemcachedClient client = GetClient(server);
            client.FlushAll();
        }

        /// <summary>
        /// 移除全部缓存
        /// </summary>
        /// <param name="server"></param>
        /// <param name="list">移除指定服务器缓存</param>
        public static void RemovAllCacheFrom(string server, ArrayList list)
        {
            MemcachedClient client = GetClient(server);
            client.FlushAll(list);
        }
        #endregion

        #region 是否存在(Exists)

        /// <summary>
        /// 判断指定键的缓存是否存在
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static bool IsExists(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.KeyExists(key);
        }
        #endregion

        #region 数值增减

        #region 存储一个数值元素

        /// <summary>
        /// 存储一个数值元素
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="counter"></param>
        /// <returns></returns>
        public static bool StoreCounterTo(string server, string key, long counter)
        {
            MemcachedClient client = GetClient(server);
            return client.StoreCounter(key, counter);
        }

        /// <summary>
        ///  存储一个数值元素
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="counter"></param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static bool StoreCounterTo(string server, string key, long counter, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            return client.StoreCounter(key, counter, hashCode);
        }
        #endregion

        #region 获取一个数值元素

        /// <summary>
        /// 获取一个数值元素
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long GetCounterFrom(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.GetCounter(key);
        }

        /// <summary>
        ///  获取一个数值元素
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long GetCounterFrom(string server, string key, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            return client.GetCounter(key, hashCode);
        }
        #endregion

        #region 增加一个数值元素的值(Increment)

        /// <summary>
        /// 将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long IncrementTo(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.Increment(key);
        }

        /// <summary>
        ///  将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <returns></returns>
        public static long IncrementTo(string server, string key, long inc)
        {
            MemcachedClient client = GetClient(server);
            return client.Increment(key, inc);
        }

        /// <summary>
        ///  将一个数值元素增加。 如果元素的值不是数值类型，将其作为0处理
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long IncrementTo(string server, string key, long inc, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            return client.Increment(key, inc, hashCode);
        }
        #endregion

        #region 减小一个数值元素的值(Decrement)

        /// <summary>
        /// 减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <returns></returns>
        public static long DecrementFrom(string server, string key)
        {
            MemcachedClient client = GetClient(server);
            return client.Decrement(key);
        }

        /// <summary>
        ///  减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <returns></returns>
        public static long DecrementFrom(string server, string key, long inc)
        {
            MemcachedClient client = GetClient(server);
            return client.Decrement(key, inc);
        }

        /// <summary>
        ///  减小一个数值元素的值，减小多少由参数offset决定。 如果元素的值不是数值，以0值对待。如果减小后的值小于0,则新的值被设置为0
        /// </summary>
        /// <param name="server"></param>
        /// <param name="key">键</param>
        /// <param name="inc">增长幅度</param>
        /// <param name="hashCode">哈希码</param>
        /// <returns></returns>
        public static long DecrementFrom(string server, string key, long inc, int hashCode)
        {
            MemcachedClient client = GetClient(server);
            return client.Decrement(key, inc, hashCode);
        }
        #endregion

        #endregion

        #endregion
    }
}

