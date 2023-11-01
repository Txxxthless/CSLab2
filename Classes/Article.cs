
using pppilab2.Interfaces;

namespace pppilab2.Classes
{
    public class Article : IRateAndCopy
    {
        public Person Author { get; set; }
        public string Name { get; set; }
        public double Rating { get; set; }

        public Article(Person author, string name, double rating)
        {
            Author = author;
            Name = name;
            Rating = rating;
        }
        public Article()
        {
            Author = new Person();
            Name = "War expirience influence";
            Rating = 8.6;
        }

        public override string ToString()
        {
            return string.Format("{0} by {1} with rating of {2}", Name, Author.ToShortString(), Rating);
        }
        public virtual object DeepCopy()
        {
            return new Article()
            {
                Author = new Person()
                {
                    Name = this.Author.Name,
                    Surname = this.Author.Surname,
                    DateOfBirth = this.Author.DateOfBirth
                },
                Name = this.Name,
                Rating = this.Rating
            };
        }
    }
}
