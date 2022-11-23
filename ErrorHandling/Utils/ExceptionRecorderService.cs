using System.Collections.ObjectModel;

namespace ErrorHandling.Utils;

public class ExceptionRecorderService
{
    public ObservableCollection<Exception> Exceptions { get; set; } = new();
}
