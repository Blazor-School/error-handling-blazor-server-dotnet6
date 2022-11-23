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
        }
        catch (Exception ex)
        {
            _exceptionRecorderService.Exceptions.Add(ex);
        }

        return response;
    }
}