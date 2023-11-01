
using pppilab2.Interfaces;
using System.Collections;
using System.ComponentModel;
using System.Text;

namespace pppilab2.Classes
{
    public class Magazine : Edition, IRateAndCopy
    {
        private Frequency _frequency;
        private List<Article> _articles = new List<Article>();
        private List<Person> _editors = new List<Person>();

        public Frequency Frequency
        {
            get { return _frequency; }
            set 
            {
                _frequency = value;
                InvokeChangedEvent("Frequency");
            }
        }
        public List<Article> Articles
        {
            get { return _articles; }
            set 
            { 
                _articles = value;
                InvokeChangedEvent("Articles");
            }
        }
        public List<Person> Editors
        {
            get { return _editors; }
            set 
            { 
                _editors = value;
                InvokeChangedEvent("Editors");
            }
        }
        public double AvarageRating
        {
            get
            {
                if (Articles.Count == 0) return 0;

                double sum = 0;
                foreach (Article article in _articles)
                {
                    sum += article.Rating;
                }
                return sum / Articles.Count;
            }
        }
        public double Rating
        {
            get { return AvarageRating; }
        }
        public Edition Edition 
        { 
            get
            {
                return new Edition
                {
                    FirstPublicationDate = this._firstPublicationDate,
                    Name = this._name,
                    Printing = this._printing
                };
            } 
        }

        public Magazine(string name, Frequency frequency, DateTime firstPublicationDate, int printing) :
            base(name, firstPublicationDate, printing)
        {
            _frequency = frequency;
        }
        public Magazine()
        {
            _frequency = Frequency.Monthly;
        }
        public void AddAtricles(params Article[] articles)
        {
            foreach (Article a in articles)
            {
                _articles = _articles.Append(a).ToList();
            }
        }
        public void AddEditors(params Person[] editors)
        {
            foreach (Person editor in editors)
            {
                _editors = _editors.Append(editor).ToList();
            }
        }
        
        public IEnumerable<Article> GetByRating(double rating)
        {
            foreach (Article article in _articles.Where(article => article.Rating > rating))
            {
                yield return article;
            }
        }
        public IEnumerable<Article> GetBySubstring(string substring)
        {
            foreach (Article article in _articles.Where(article => article.Name.Contains(substring)))
            {
                yield return article;
            }
        }

        public virtual string ToShortString()
        {
            return string.Format("{0}, published {1}, first published on {2}, avarage rating is {3}",
                Name, Frequency, FirstPublicationDate, AvarageRating);
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(string.Format("{0}, published {1}, first published on {2}, avarage rating is {3}. Include articles: ",
                Name, Frequency, FirstPublicationDate, AvarageRating));
            foreach (Article a in Articles)
            {
                sb.Append(a.Name);
                sb.Append(" ");
            }
            return sb.ToString();
        }
        public override object DeepCopy()
        {
            List<Article> articlesCopy = new List<Article>();
            foreach(Article a in this.Articles)
            {
                articlesCopy.Add((Article)a.DeepCopy());
            }

            List<Person> editorsCopy = new List<Person>();
            foreach(Person p in this.Editors)
            {
                editorsCopy.Add(new Person()
                {
                    Name = p.Name,
                    DateOfBirth = p.DateOfBirth,
                    Surname = p.Surname
                });
            }

            return new Magazine()
            {
                Name = this.Name,
                FirstPublicationDate = this.FirstPublicationDate,
                Frequency = this.Frequency,
                Printing = this.Printing,
                Articles = articlesCopy,
                Editors = editorsCopy
            };
        }
    }
}
