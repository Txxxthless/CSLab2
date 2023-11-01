
using System.Text;

namespace pppilab2.Classes
{
    public class Listener<TKey>
    {
        private List<ListEntry> _entries = new List<ListEntry>();
        
        public void EventHandler(object sender, MagazinesChangedEventArgs<TKey> args)
        {
            _entries.Add(new ListEntry(args.CollectionName, args.ChangeInfo, args.ChangedProperty, args.Key.ToString()));
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (ListEntry entry in _entries)
            {
                sb.Append(entry.ToString() + "\n");
            }
            return sb.ToString();
        }
    }
}
