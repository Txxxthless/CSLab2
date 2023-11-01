
namespace pppilab2.Classes
{
    public class MagazinesChangedEventArgs<TKey>
    {
        public string CollectionName { get; set; }
        public Update ChangeInfo { get; set; }
        public string ChangedProperty { get; set; }
        public TKey Key { get; set; }

        public MagazinesChangedEventArgs(string collectionName, Update changeInfo, string changedProperty, TKey key)
        {
            CollectionName = collectionName;
            ChangeInfo = changeInfo;
            ChangedProperty = changedProperty;
            Key = key;
        }

        public override string ToString()
        {
            return $"{CollectionName} {ChangeInfo} {ChangedProperty} {Key}";
        }
    }
}
