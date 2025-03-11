namespace CQRSWebApiProject.DAL.Concrete.Redis.Abstract
{
    public interface IRedisService
    {
        Task SetValueAsync(string key, string value);

        Task<string> GetValueAsync(string key);
    }
}