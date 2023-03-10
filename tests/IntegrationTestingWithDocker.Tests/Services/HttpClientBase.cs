using System.Text;
using Newtonsoft.Json;

namespace IntegrationTestingWithDocker.Tests.Services;

public class HttpClientBase
{
    protected HttpClient HttpClient { get; }

    protected HttpClientBase(HttpClient httpHttpClient)
    {
        HttpClient = httpHttpClient;
    }

    protected async Task<bool> DeleteAsync(string path, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.DeleteAsync(path, cancellationToken).ConfigureAwait(false);
        return response.IsSuccessStatusCode;
    }

    protected async Task<T?> GetAsync<T>(string path, CancellationToken cancellationToken = default)
    {
        var response = await HttpClient.GetAsync(path, cancellationToken).ConfigureAwait(false);
        return await DeserializeContentAsync<T>(response, cancellationToken);
    }

    protected async Task<T?> PostAsync<T>(string path, T content, CancellationToken cancellationToken = default)
    {
        return await PostAsync<T, T>(path, content, cancellationToken);
    }

    protected async Task<TOut?> PostAsync<TIn, TOut>(string path, TIn content, CancellationToken cancellationToken = default)
    {
        var json = content == null ? null : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        var response = await HttpClient.PostAsync(path, json, cancellationToken).ConfigureAwait(false);
        return await DeserializeContentAsync<TOut>(response, cancellationToken);
    }

    protected async Task<TOut?> PutAsync<TIn, TOut>(string path, TIn content, CancellationToken cancellationToken = default)
    {
        var json = content == null ? null : new StringContent(JsonConvert.SerializeObject(content), Encoding.UTF8, "application/json");
        var response = await HttpClient.PutAsync(path, json, cancellationToken).ConfigureAwait(false);
        return await DeserializeContentAsync<TOut>(response, cancellationToken);
    }

    private static async Task<T?> DeserializeContentAsync<T>(HttpResponseMessage response, CancellationToken cancellationToken = default)
    {
        var responseString = await response.Content.ReadAsStringAsync(cancellationToken).ConfigureAwait(false);
        var result = !response.IsSuccessStatusCode || cancellationToken.IsCancellationRequested ? default : Convert<T>(responseString);
        return result;
    }

    private static TData? Convert<TData>(string content)
    {
        var jsonSettings = new JsonSerializerSettings
        {
            MetadataPropertyHandling = MetadataPropertyHandling.Ignore,
            DateParseHandling = DateParseHandling.None,
        };
        return JsonConvert.DeserializeObject<TData>(content, jsonSettings);
    }
}
