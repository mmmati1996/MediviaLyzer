using Prism.Events;
using System.Collections.ObjectModel;

namespace MediviaLyzer.Tabs.Events
{
    public class IsBedmakerEnabled : PubSubEvent<bool> {}
    public class ListOfBedmakers : PubSubEvent<ObservableCollection<Models.CharacterModel>> {}
    public class BedmakerOfflineTime : PubSubEvent<int> {}
}
