using System.Threading.Tasks;

public interface ISapODataService
{
    Task<string> GetDataAsync(string serviceUrl, string username, string password, string entitySet);
    Task<string> CreateDataAsync(string serviceUrl, string username, string password, string entitySet, string jsonData);
    Task<string> UpdateDataAsync(string serviceUrl, string username, string password, string entitySet, string key, string jsonData);
}