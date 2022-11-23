using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;

namespace ErrorHandling.Utils;

public class BlazorSchoolErrorBoundary : ErrorBoundaryBase
{
    [Inject]
    public ExceptionRecorderService ExceptionRecorderService { get; set; } = default!;

    public BlazorSchoolErrorBoundary()
    {
        MaximumErrorCount = 2;
    }

    protected override Task OnErrorAsync(Exception exception)
    {
        ExceptionRecorderService.Exceptions.Add(exception);

        return Task.CompletedTask;
    }

    protected void RecoverAndClearErrors()
    {
        Recover();
        ExceptionRecorderService.Exceptions.Clear();
    }

    protected override void BuildRenderTree(RenderTreeBuilder builder)
    {
        if (CurrentException is null)
        {
            builder.AddContent(0, ChildContent);
        }
        else
        {
            if (ErrorContent is not null)
            {
                builder.AddContent(1, ErrorContent(CurrentException));
            }
            else
            {
                builder.OpenElement(2, "div");
                builder.AddAttribute(3, "class", "text-danger border border-danger p-3");
                builder.AddContent(4, "Blazor School Custom Error Boundary.");
                builder.AddContent(5, __innerBuilder =>
                {
                    __innerBuilder.OpenElement(6, "button");
                    __innerBuilder.AddAttribute(7, "type", "button");
                    __innerBuilder.AddAttribute(8, "class", "btn btn-link");
                    __innerBuilder.AddAttribute(9, "onclick", RecoverAndClearErrors);
                    __innerBuilder.AddContent(10, "Continue");
                    __innerBuilder.CloseElement();
                });
                builder.CloseElement();
            }
        }
    }
}