using System;
using System.Threading.Tasks;
using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Objects;

namespace Genbox.Wikipedia.Examples
{
    internal class Program
    {
        private static async Task Main()
        {
            //Default language is English
            WikipediaClient client = new WikipediaClient();

            //Use HTTPS instead of HTTP
            client.UseTls = true;

            //We would like 5 results
            client.Limit = 5;

            //We would like to search inside the articles
            client.What = What.Text;

            const string searchText = "Microsoft C#";
            QueryResult results = await client.SearchAsync(searchText);

            Console.WriteLine("Searching for {0}:{1}", searchText, Environment.NewLine);
            Console.WriteLine("Found " + results.Search.Count + " English results:");

            foreach (Search s in results.Search)
            {
                Console.WriteLine(s.Title);
            }

            Console.WriteLine();
            Console.WriteLine();

            //We change the language to Spanish
            client.Language = Language.Spanish;

            results = client.Search("Microsoft C#");

            Console.WriteLine("Found " + results.Search.Count + " Spanish results:");

            foreach (Search s in results.Search)
            {
                Console.WriteLine(s.Title);
            }
        }
    }
}
