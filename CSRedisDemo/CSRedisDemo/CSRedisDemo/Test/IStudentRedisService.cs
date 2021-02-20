namespace CSRedisDemo
{
    public interface IStudentRedisService
    {
        /// <summary>
        /// 根据key参数从Redis中获取值
        /// </summary>
        Student GetRedisByRedisInputKey(string redisInputKey);

        /// <summary>
        /// 通知缓存刷新
        /// </summary>
        void NoticeRedisUpdateByKey(string redisInputKey);
    }
}