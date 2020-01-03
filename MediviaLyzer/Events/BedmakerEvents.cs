using Prism.Events;
using System.Collections.ObjectModel;

namespace MediviaLyzer.Events
{
    public class IsBedmakerEnabled : PubSubEvent<bool> {}
    public class ListOfBedmakers : PubSubEvent<ObservableCollection<Models.CharacterModel>> {}
}
