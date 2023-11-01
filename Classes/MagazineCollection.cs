
using pppilab2.Classes;
using System.ComponentModel;
using System.Text;

public delegate TKey KeySelector<TKey>(Magazine mg);
public delegate void MagazinesChangedHandler<TKey>(object source, MagazinesChangedEventArgs<TKey> args);

namespace pppilab2.Classes
{
    public class MagazineCollection<TKey>
    {
        public string Name { get; set; }
        public event MagazinesChangedHandler<TKey> MagazinesChanged;

        private Dictionary<TKey, Magazine> _collection = new Dictionary<TKey, Magazine>();
        private KeySelector<TKey> _keySelector;

        public MagazineCollection(KeySelector<TKey> keySelector)
        {
            _keySelector = keySelector;
        }

        public void AddDefaults()
        {
            AddMagazines(
                new Magazine("Default1", Frequency.Weekly, DateTime.Now, 2000),
                new Magazine("Default2", Frequency.Monthly, new DateTime(2020, 10, 4), 2200),
                new Magazine("Default3", Frequency.Yearly, new DateTime(2019, 2, 1), 1500)
            );
        }

        public void AddMagazines(params Magazine[] magazines)
        {
            foreach (var magazine in magazines)
            {
                TKey key = _keySelector(magazine);
                _collection[key] = magazine;
                InvokeMagazineChanged(Update.Add, magazine);
                magazine.PropertyChanged += MagazinePropertyChanged;
            }
        }

        public double MaxAverageArticleRating
        {
            get
            {
                if (_collection.Count == 0)
                {
                    return 0.0;
                }
                    
                 return _collection.Values.Max(mg => mg.AvarageRating);
            }
        }

        void MagazinePropertyChanged(object? sender, PropertyChangedEventArgs args)
        {
            Magazine magazine = (Magazine)sender;
            MagazinesChanged?.Invoke(sender, new MagazinesChangedEventArgs<TKey>(Name, Update.Property, args.PropertyName, _keySelector(magazine)));
        }

        void InvokeMagazineChanged(Update changeInfo, Magazine magazine)
        {
            TKey key = _keySelector(magazine);
            MagazinesChanged?.Invoke(this, new MagazinesChangedEventArgs<TKey>(Name, changeInfo, "", key));
        }

        public IEnumerable<KeyValuePair<TKey, Magazine>> FrequencyGroup(Frequency value)
        {
            return _collection.Where(pair => pair.Value.Frequency == value);
        }

        public IEnumerable<IGrouping<Frequency, KeyValuePair<TKey, Magazine>>> FrequencyGroups
        {
            get
            {
                return _collection.GroupBy(pair => pair.Value.Frequency);
            }
        }

        public bool Replace(Magazine mOld, Magazine mNew)
        {
            TKey oldKey = _keySelector(mOld);
            TKey newKey = _keySelector(mNew);

            if (_collection.ContainsKey(oldKey))
            {
                _collection.Remove(oldKey);
                mOld.PropertyChanged -= MagazinePropertyChanged;
                AddMagazines(mNew);
                InvokeMagazineChanged(Update.Replace, mNew);
                return true;
            }

            return false;
        }

        public override string ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var magazine in _collection.Values)
            {
                stringBuilder.Append(magazine.ToString() + "\n");
            }
            return stringBuilder.ToString();
        }

        public string ToShortString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var magazine in _collection.Values)
            {
                stringBuilder.Append(magazine.ToShortString() + "\n");
            }
            return stringBuilder.ToString();
        }
    }
}
