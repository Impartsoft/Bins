using System.Linq;

namespace CSRedisDemo
{
    /// <summary>
    /// Redis Student实现类
    /// </summary>
    public class StudentRedisService : AbstractRedisService<string, Student>,IStudentRedisService
    {
        private readonly static object _lock = new object();
        private const int _expireTime = 3600;

        protected override RedisGroup CacheGroup => RedisGroup.Student;

        protected override string GetKeyByRedisInputKey(string redisInputKey) => redisInputKey;

        protected override Student GetValueByKey(string redisInputKey)
        {
            return Test.AllStudents.Where(v=>v.Name == redisInputKey).FirstOrDefault();
        }
    }
}