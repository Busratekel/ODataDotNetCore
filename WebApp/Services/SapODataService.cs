using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

public class SapODataService : ISapODataService
{
    private readonly HttpClient _client;

    public SapODataService(HttpClient client)
    {
        _client = client;
    }

    public async Task<string> GetDataAsync(string serviceUrl, string username, string password, string entitySet)
    {
        var byteArray = new System.Text.UTF8Encoding().GetBytes($"{username}:{password}");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var response = await _client.GetAsync($"{serviceUrl}/{entitySet}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
    public async Task<string> CreateDataAsync(string serviceUrl, string username, string password, string entitySet, string jsonData)
    {
        var byteArray = new System.Text.UTF8Encoding().GetBytes($"{username}:{password}");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _client.PostAsync($"{serviceUrl}/{entitySet}", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }

    public async Task<string> UpdateDataAsync(string serviceUrl, string username, string password, string entitySet, string key, string jsonData)
    {
        var byteArray = new System.Text.UTF8Encoding().GetBytes($"{username}:{password}");
        _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
        _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        var content = new StringContent(jsonData, Encoding.UTF8, "application/json");
        var response = await _client.PutAsync($"{serviceUrl}/{entitySet}({key})", content);
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}