
using pppilab2.Classes;

MagazineCollection<string> firstCollection = new MagazineCollection<string>(KeySelection) { Name = "FirstCol" };
MagazineCollection<string> secondCollection = new MagazineCollection<string>(KeySelection) { Name = "SecondCol" };

Listener<string> listener = new Listener<string>();

firstCollection.MagazinesChanged += listener.EventHandler;
secondCollection.MagazinesChanged += listener.EventHandler;

firstCollection.AddDefaults();

Magazine foo = new Magazine("Foo", Frequency.Weekly, DateTime.Now, 3123);
Magazine bar = new Magazine("Bar", Frequency.Monthly, DateTime.Now, 4242);

secondCollection.AddMagazines(foo);
foo.Printing = 12;

secondCollection.Replace(foo, bar);
bar.Printing = 13;
foo.Printing = 13;

Console.WriteLine(listener.ToString());

List<Article> articles1 = new List<Article>();
articles1.Add(new Article());

List<Article> articles2 = new List<Article>();
articles2.Add(new Article(new Person(), "Foo", 4.3));

firstCollection.AddMagazines(
    new Magazine("Blabla", Frequency.Yearly, DateTime.Now, 112) { Articles = articles1 },
    new Magazine("Trtrtrt", Frequency.Yearly, DateTime.Now, 400002) { Articles = articles2 },
    new Magazine("JKjkjkjkj", Frequency.Weekly, DateTime.Now, 4)
    );

Console.WriteLine("====================Max===========================");

Console.WriteLine(firstCollection.MaxAverageArticleRating);

Console.WriteLine("====================Where===========================");

var yearly = firstCollection.FrequencyGroup(Frequency.Yearly);

foreach(var magazine in yearly)
{
    Console.WriteLine(magazine.ToString() + "\n");
}

Console.WriteLine("====================Group By===========================");

var grouping = firstCollection.FrequencyGroups;

foreach(var group in grouping)
{
    Console.WriteLine("KEY = " + group.Key);
    foreach(var magazine in group)
    {
        Console.WriteLine(magazine.ToString());
    }
}

string KeySelection(Magazine magazine)
{
    return magazine.Name;
}