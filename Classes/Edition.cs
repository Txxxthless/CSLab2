
using System.ComponentModel;

namespace pppilab2.Classes
{
    public class Edition : INotifyPropertyChanged
    {
        protected string _name;
        protected DateTime _firstPublicationDate;
        protected int _printing;

        public event PropertyChangedEventHandler? PropertyChanged;

        public string Name
        {
            get { return _name; }
            set 
            { 
                _name = value;
                InvokeChangedEvent("Name");
            }
        }
        public DateTime FirstPublicationDate
        {
            get { return _firstPublicationDate; }
            set 
            {
                _firstPublicationDate = value;
                InvokeChangedEvent("FirstPublicationDate");
            }
        }
        public int Printing
        {
            get { return _printing; }
            set 
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("Printing cannot be below 0");
                }
                _printing = value;
                InvokeChangedEvent("Printing");
            }
        }

        public Edition(string name, DateTime firstPublicationDate, int printing)
        {
            _name = name;
            _firstPublicationDate = firstPublicationDate;
            _printing = printing;
        }
        
        public Edition()
        {
            _name = "The Rolling Stones";
            _firstPublicationDate = new DateTime(1967, 9, 11);
            _printing = 20000;
        }

        protected void InvokeChangedEvent(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        public virtual object DeepCopy()
        {
            return new Edition()
            {
                Name = this.Name,
                FirstPublicationDate = this.FirstPublicationDate,
                Printing = this.Printing
            };
        }
        public override bool Equals(object? obj)
        {
            Edition otherEdition = (Edition)obj;
            return Name == otherEdition?.Name 
                && FirstPublicationDate == otherEdition.FirstPublicationDate
                && Printing == otherEdition.Printing;
        }
        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
        public override string ToString()
        {
            return string.Format("{0}, first published on {1}, printing {2}",
                Name, FirstPublicationDate, Printing);
        }

        public static bool operator == (Edition firstEdition, Edition secondEdition)
        {
            return firstEdition.Name == secondEdition.Name
                && firstEdition.FirstPublicationDate == secondEdition.FirstPublicationDate
                && firstEdition.Printing == secondEdition.Printing;
        }
        public static bool operator !=(Edition firstEdition, Edition secondEdition)
        {
            return !( firstEdition.Name == secondEdition.Name
                && firstEdition.FirstPublicationDate == secondEdition.FirstPublicationDate
                && firstEdition.Printing == secondEdition.Printing );
        }
    }
}
