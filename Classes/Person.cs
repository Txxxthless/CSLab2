
namespace pppilab2.Classes
{
    public class Person
    {
        private string _name;
        private string _surname;
        private DateTime _dateOfBirth;

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string Surname
        {
            get { return _surname; }
            set { _surname = value; }
        }
        public DateTime DateOfBirth
        {
            get { return _dateOfBirth; }
            set { _dateOfBirth = value; }
        }
        public int YearOfBirth
        {
            get { return _dateOfBirth.Year; }
            set
            {
                int difference = value - DateOfBirth.Year;
                DateOfBirth.AddYears(difference);
            }
        }

        public Person(string name, string surname, DateTime dateOfBirth)
        {
            _name = name;
            _surname = surname;
            _dateOfBirth = dateOfBirth;
        }
        public Person()
        {
            _name = "Erich Maria";
            _surname = "Remarque";
            _dateOfBirth = new DateTime(1898, 5, 22);
        }

        public override string ToString()
        {
            return string.Format("{0} {1}, {2}", Name, Surname, DateOfBirth);
        }
        public virtual string ToShortString()
        {
            return string.Format("{0} {1}", Name, Surname);
        }
        public override bool Equals(object? obj)
        {
            Person otherPerson = (Person)obj;
            return Name == otherPerson?.Name 
                && Surname == otherPerson.Surname
                && DateOfBirth == otherPerson.DateOfBirth;
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }

        public static bool operator ==(Person firstPerson, Person secondPerson)
        {
            return firstPerson.Name == secondPerson.Name
                && firstPerson.Surname == secondPerson.Surname
                && firstPerson.DateOfBirth == secondPerson.DateOfBirth;
        }
        public static bool operator !=(Person firstPerson, Person secondPerson)
        {
            return !( firstPerson.Name == secondPerson.Name
                && firstPerson.Surname == secondPerson.Surname
                && firstPerson.DateOfBirth == secondPerson.DateOfBirth );
        }
    }
}
