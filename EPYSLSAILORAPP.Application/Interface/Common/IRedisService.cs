namespace EPYSLSAILORAPP.Application.Interface.Common
{
    public interface IRedisService<T> where T : class
    {

        void SaveDataInRedisCache(string key, Object value);
        List<T> GetDataFromRedisCache(string key);
        //void SaveDataInRedisCache(string key, Object value, int timeoutInMinute);
        bool IsKeyExists(string key);
        bool RemoveKey(string key);

        void ClearAllKeys();
    }
}
