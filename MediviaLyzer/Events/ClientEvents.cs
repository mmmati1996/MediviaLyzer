using Prism.Events;

namespace MediviaLyzer.Events
{
    public class IsWindowVisible : PubSubEvent<bool> { }
    public class ActivateWindow : PubSubEvent { }
}
