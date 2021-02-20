using CSRedis;

namespace CSRedisDemo
{
    /// <summary>
    /// CSRedisClient 单例
    /// </summary>
    internal class CSRedisInstance
    {
        private static readonly object _lock = new object();
        private static CSRedisClient _csRedis = null;

        private const string ip = "127.0.0.1";
        private const string port = "6379";
        private const string preheat = "100";
        private const string connectTimeout = "100";
        private const string tryit = "1";
        private const string prefix = "CSRedisTest.";
        private static readonly string _connectString = $"{ip}:{port}," +
          $"preheat={preheat},connectTimeout={connectTimeout},tryit={tryit},prefix={prefix}";

        internal static CSRedisClient GetRedis()
        {
            if (_csRedis == null)
            {
                lock (_lock)
                {
                    if (_csRedis == null)
                    {
                        _csRedis = GetCSRedisClient();
                    }
                }
            }

            return _csRedis;
        }

        private static CSRedisClient GetCSRedisClient()
        {
            return new CSRedisClient(_connectString);
        }
    }
}
