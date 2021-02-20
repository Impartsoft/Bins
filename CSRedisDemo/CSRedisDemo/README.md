## 0.介绍
> .NET Core or .NET Framework 4.0+ client for Redis and Redis Sentinel (2.8) and Cluster. Includes both synchronous and asynchronous clients.

本文记录CSRedis在开发过程中的简单使用，可以直接调试样例源码。

## 1. 参考资料
> github https://github.com/2881099/csredis

> 作者博客 https://www.cnblogs.com/kellynic/p/9952386.html

> Redis Runoob教程 https://www.runoob.com/redis/redis-install.html
## 2.核心内容

- #### 使用心得

1.科学使用缓存

- 从Redis中读取数据
 
从Redis中读取数据需要考虑"数据存在，但是Redis中过期或者未写入的情况"这时候就需要根据指定Key先获取数据再写入Redis中。
- 将数据写入Redis

写入Redis需要增加过期时。增加过期时间的时候可以将时间随机，这样可以避免缓存在相同时间过期而引发缓存雪崩。

在高并发的情况下，如果根据key获取的数据不存在，也将null保存至Redis中，而非抽象类（AbstractRedisService）中做删除动作，这样可避免缓存穿透。

> 我对高并发场景下的缓存使用理解不深，AbstractRedisService抽象类在更新失效值的时候使用了lock，使用lock写法容易造成堵塞，但是如果不使用lock的话，就会出现重复读取再写入Redis的情况，并且在当前情况下如何处理缓存击穿也不是很清楚，希望大家能不吝赐教~

2.在使用CSRedis的时候遇到了多个业务模块都有相识的代码，于是抽取了抽象类AbstractRedisService，在业务模块实现的时候只需要实现RedisGroup 属性与GetKeyByRedisInputKey、GetValueByKey两个方法。

3.在实际应用中，有可能会出现程序与Redis服务连接不稳定的情况，如果Redis服务没有发现问题的话，可以尝试使用下面三种方式解决(参考 https://github.com/2881099/csredis/issues)

- connectTimeout=30000  设置连接超时时间

- tryit=3 设置重试次数

- preheat=100  预热连接数
- #### 初始化RedisHelper

```
    
    // 初始化RedisHelper
    RedisHelper.Initialization(CSRedisInstance.GetRedis());

    /// <summary>
    /// CSRedisClient 单例
    /// </summary>
    internal class CSRedisInstance
    {
        private static readonly object _lock = new object();
        private static CSRedisClient _csRedis = null;

        private const string ip = "127.0.0.1";
        private const string port = "6379";
        private const string preheat = "100"; // 设置预热连接数
        private const string connectTimeout = "100"; // 设置连接超时时间
        private const string tryit = "1"; // 设置重试次数
        private const string prefix = "CSRedisTest."; // 设置前缀
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
```





- #### 业务应用 - 抽象类分享

##### AbstractRedisService抽象类


```

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
```

##### 1.StudentRedisService实现类


```
    /// <summary>
    /// Redis Student实现类
    /// </summary>
    public class StudentRedisService : AbstractRedisService<string, Student>,IStudentRedisService
    {
        protected override RedisGroup CacheGroup => RedisGroup.Student;

        protected override string GetKeyByRedisInputKey(string redisInputKey) => redisInputKey;

        protected override Student GetValueByKey(string redisInputKey)
        {
            return Test.AllStudents.Where(v=>v.Name == redisInputKey).FirstOrDefault();
        }
    }
```


##### 2.使用StudentRedisService
```


private static void AbstractRedisServiceTest()
{
    var studentRedisService = new StudentRedisService();

    // 一：通过key获取
    var mark = studentRedisService.GetRedisByRedisInputKey("Mark"); // 有此数据，获取的时候会写入Redis
    var linda = studentRedisService.GetRedisByRedisInputKey("Linda"); // 无数据则返回null

    // 二：数据变更通知
    string markName = "Mark";
    // 更新数据
    int random = new Random().Next();
    Test.AllStudents.Where(v => v.Name == markName).First().Age = random;

    var mark2 = studentRedisService.GetRedisByRedisInputKey(markName); // 旧值
    studentRedisService.NoticeRedisUpdateByKey(markName);// 更新Redis
    mark2 = studentRedisService.GetRedisByRedisInputKey(markName);// 新值
}
```




## 3.样例源码地址
调试Demo可以先参考Redis Runoob安装教程，部署Redis服务，再进行调试
 > https://github.com/Impartsoft/Bins/tree/main/CSRedisDemo/CSRedisDemo
 

---


> 欢迎大家批评指正，共同学习，共同进步！
> 作者：Iannnnnnnnnnnnn
> 出处：https://www.cnblogs.com/Iannnnnnnnnnnnn
> 本文版权归作者和博客园共有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文连接，否则保留追究法律责任的权利。