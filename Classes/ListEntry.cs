
namespace pppilab2.Classes
{
    public class ListEntry
    {
        public string CollectionName { get; set; }
        public Update ChangeType { get; set; }
        public string ChangedProperty { get; set; }
        public string Key { get; set; }

        public ListEntry(string collectionName, Update changeType, string changedProp, string key)
        {
            CollectionName = collectionName;
            ChangeType = changeType;
            ChangedProperty = changedProp;
            Key = key;
        }

        public override string ToString()
        {
            return $"{CollectionName} {ChangeType} {ChangedProperty} {Key}";
        }
    }
}
