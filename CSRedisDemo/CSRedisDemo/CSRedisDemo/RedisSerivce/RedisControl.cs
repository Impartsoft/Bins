using System;

namespace CSRedisDemo
{
    /// <summary>
    /// Redis控制，可以控制是否启用Redis与是否需要关闭Redis
    /// </summary>
    public class RedisControl
    {
        public static bool UseRedis()
        {
            return true;
        }

        public static void StopUseRedis()
        {
            throw new NotImplementedException();
        }
    }

}