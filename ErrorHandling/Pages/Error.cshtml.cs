using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Diagnostics;

namespace ErrorHandling.Pages;
[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
[IgnoreAntiforgeryToken]
public class ErrorModel : PageModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

    private readonly ILogger<ErrorModel> _logger;

    public ErrorModel(ILogger<ErrorModel> logger)
    {
        _logger = logger;
    }

    public void OnGet() => RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;

    public void OnPost()
    {
        var exceptionHandler = HttpContext.Features.Get<IExceptionHandlerFeature>();

        if (exceptionHandler is not null)
        {
            Console.WriteLine($"You can log the error with the detailed message in the exceptionHandler {exceptionHandler.Error.Message}");
            Console.WriteLine($"You can log the error with the stack trace in the exceptionHandler {exceptionHandler.Error.StackTrace}");
        }
    }
}
