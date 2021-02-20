using System;
using System.Linq;
using System.Threading;

namespace CSRedisDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // 初始化RedisHelper
            RedisHelper.Initialization(CSRedisInstance.GetRedis());

            // CSRedis 简单测试
            CSRedisTest();

            // CSRedis 抽象类测试
            AbstractRedisServiceTest();
        }

        private static void CSRedisTest()
        {
            RedisHelper.Set("Jordan", "2");
            string jordan = RedisHelper.Get("Jordan");

            string lemon = RedisHelper.Get("Lemon");

            RedisHelper.Set("Bill", "200", 5);
            string bill = RedisHelper.Get("Bill");
            Thread.Sleep(5000);
            string bill2 = RedisHelper.Get("Bill");
        }

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
    }
}
