using System;
using WikipediaNET;
using WikipediaNET.Enums;
using WikipediaNET.Objects;

namespace WikipediaNETClient
{
    class Program
    {
        static void Main(string[] args)
        {
            //Default language is English
            Wikipedia wikipedia = new Wikipedia();
            
            //Use HTTPS instead of HTTP
            wikipedia.UseTLS = true;

            //We would like 5 results
            wikipedia.Limit = 5;

            //We would like to search inside the articles
            wikipedia.What = What.Text;

            const string searchText = "Microsoft C#";
            QueryResult results = wikipedia.Search(searchText);

            Console.WriteLine("Searching for {0}:{1}", searchText, Environment.NewLine);
            Console.WriteLine("Found " + results.Search.Count + " English results:");

            foreach (Search s in results.Search)
            {
                Console.WriteLine(s.Title);
            }

            Console.WriteLine();
            Console.WriteLine();

            //We change the language to Spanish
            wikipedia.Language = Language.Spanish;

            results = wikipedia.Search("Microsoft C#");

            Console.WriteLine("Found " + results.Search.Count + " Spanish results:");

            foreach (Search s in results.Search)
            {
                Console.WriteLine(s.Title);
            }

            Console.WriteLine("Press a key to continue");
            Console.ReadLine();
        }
    }
}
