using System.Net;

namespace ErrorHandling.Utils;

public class BlazorSchoolHttpClientWrapper
{
    private readonly HttpClient _httpClient;
    private readonly ExceptionRecorderService _exceptionRecorderService;

    public BlazorSchoolHttpClientWrapper(HttpClient httpClient, ExceptionRecorderService exceptionRecorderService)
    {
        _httpClient = httpClient;
        _exceptionRecorderService = exceptionRecorderService;
    }

    public async Task<HttpResponseMessage> GetAsync(string? requestUri)
    {
        var response = new HttpResponseMessage();

        try
        {
            response = await _httpClient.GetAsync(requestUri);

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                case HttpStatusCode.Unauthorized:
                    _exceptionRecorderService.Exceptions.Add(new($"The request returns {response.StatusCode}."));
                    break;
                // Handle other status codes
                default:
                    _exceptionRecorderService.Exceptions.Add(new($"{response.StatusCode}"));
                    break;
            }
        }
        catch (Exception ex)
        {
            _exceptionRecorderService.Exceptions.Add(ex);
        }

        return response;
    }
}