using System;

namespace JuCheap.Core
{
    /// <summary>
    /// GUID生成器
    /// </summary>
    public class GuidGeneratorHelper
    {
        /// <summary>
        /// 返回Guid用于数据库操作，特定的时间代码可以提高检索效率
        /// </summary>
        /// <returns>COMB类型 Guid 数据</returns>
        public static Guid NewGuid()
        {
            byte[] guidArray = Guid.NewGuid().ToByteArray();
            DateTime dtBase = new DateTime(1900, 1, 1);
            DateTime dtNow = DateTime.Now;
            //获取用于生成byte字符串的天数与毫秒数
            TimeSpan days = new TimeSpan(dtNow.Ticks - dtBase.Ticks);
            TimeSpan msecs = new TimeSpan(dtNow.Ticks - (new DateTime(dtNow.Year, dtNow.Month, dtNow.Day).Ticks));
            //转换成byte数组
            //注意SqlServer的时间计数只能精确到1/300秒
            byte[] daysArray = BitConverter.GetBytes(days.Days);
            byte[] msecsArray = BitConverter.GetBytes((long)(msecs.TotalMilliseconds / 3.333333));

            //反转字节以符合SqlServer的排序
            Array.Reverse(daysArray);
            Array.Reverse(msecsArray);

            //把字节复制到Guid中
            Array.Copy(daysArray, daysArray.Length - 2, guidArray, guidArray.Length - 6, 2);
            Array.Copy(msecsArray, msecsArray.Length - 4, guidArray, guidArray.Length - 4, 4);
            return new Guid(guidArray);
        }
    }

    /// <summary>
    /// ID生成器
    /// </summary>
    public class IdGeneratorHelper
    {
        private readonly IdGenerator _idGenerator = new IdGenerator(2);
        private readonly byte _identity;

        public IdGeneratorHelper()
        {
            _identity = 1;
        }
        /// <summary>
        /// 生成ID
        /// </summary>
        /// <returns></returns>
        public decimal GetId()
        {
            return _idGenerator.GetId(DateTime.Now, _identity);
        }

        /// <summary>
        /// ID生成器实例
        /// </summary>
        public static readonly IdGeneratorHelper Instance = new IdGeneratorHelper();
    }

    /// <summary>
    /// Id生成器，保证每秒钟生成 10的seedWidth次方减1 个不同的数字编号
    /// </summary>
    public class IdGenerator
    {
        private readonly int _seedWith;
        private readonly long _max;
        private int _seed;
        private readonly object _locker = new object();

        public IdGenerator(int seedWith)
        {
            _seedWith = seedWith;
            _max = (long)Math.Pow(10, seedWith) - 1;
        }

        private const string TimeFormat = "yyMMddHHmmss";

        /// <summary>
        /// 生成Id
        /// </summary>
        /// <param name="time"></param>
        /// <param name="identity">服务器标识</param>
        /// <returns></returns>
        public decimal GetId(DateTime time, byte identity)
        {
            var prefix = (decimal)(identity * Math.Pow(10, TimeFormat.Length + _seedWith));

            var stamp = decimal.Parse(time.ToString(TimeFormat)) * (decimal)Math.Pow(10, _seedWith);

            lock (_locker)
            {
                _seed++;
                var id = prefix + stamp + _seed;

                if (_seed >= _max)
                {
                    _seed = 0;
                }
                return id;
            }
        }
    }
}
