using System;

namespace CSRedisDemo
{
    /// <summary>
    /// Redis抽象类 - 缓存内容根据Key指定刷新
    /// </summary>
    /// <typeparam name="RedisInputKey">输入Key值</typeparam>
    /// <typeparam name="RedisValue">Redis保存的Value值</typeparam>
    public abstract class AbstractRedisService<RedisInputKey, RedisValue>
    {
        private readonly static object _lock = new object();
        private const int _expireTime = 3600;

        /// <summary>
        /// 缓存模块
        /// </summary>
        protected abstract RedisGroup CacheGroup { get; }

        /// <summary>
        /// 根据输入Key值，返回真正RedisKey
        /// </summary>
        protected abstract string GetKeyByRedisInputKey(RedisInputKey redisInputKey);

        /// <summary>
        /// 根据输入Key值，获取对应Value
        /// </summary>
        protected abstract RedisValue GetValueByKey(RedisInputKey redisInputKey);

        public RedisValue GetRedisByRedisInputKey(RedisInputKey redisInputKey)
        {
            if (!RedisControl.UseRedis())
                return default(RedisValue);

            var result = GetRedisValue(redisInputKey);
            // 刷新Redis之后还无法获取正确的值，则记录原因
            if (result == null)
            {
                // 日志输出
            };

            return result;
        }

        public void NoticeRedisUpdateByKey(RedisInputKey redisInputKey)
        {
            try
            {
                UpdateByKey(redisInputKey);
            }
            catch (Exception e)
            {
                // 日志输出
            }
        }

        /// <summary>
        /// 有可能没有Redis服务，则将异常捕捉，并停止使用Redis缓存
        /// </summary>
        private RedisValue GetRedisValue(RedisInputKey redisInputKey)
        {
            RedisValue value = default(RedisValue);
            string key = GetKeyByRedisInputKey(redisInputKey);
            try
            {
                value = GetRedisValueByKey(key);
                if (value != null)
                    return value;

                lock (_lock)
                {
                    value = GetRedisValueByKey(key);
                    if (value == null)
                        UpdateByKey(redisInputKey);
                }

                value = GetRedisValueByKey(key);
            }
            catch (Exception e)
            {
                RedisControl.StopUseRedis();
                // 日志输出
            }

            return value;
        }

        private void UpdateByKey(RedisInputKey redisInputKey)
        {
            var key = GetKeyByRedisInputKey(redisInputKey);
            RedisValue value = GetValueByKey(redisInputKey);

            if (value == null) //删除操作执行更新时，移除掉key
                RedisHelper.Del(key);
            else
                RedisHelper.Set(key, value, _expireTime + 200 * new Random().Next(1, 10));
        }

        private RedisValue GetRedisValueByKey(string key)
        {
            return RedisHelper.Get<RedisValue>(key);
        }
    }
}