using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Objects;

namespace Genbox.Wikipedia.Examples;

internal static class Program
{
    private static async Task Main()
    {
        //Default language is English
        using WikipediaClient client = new WikipediaClient();

        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.Limit = 5; //We would like 5 results
        req.WhatToSearch = WikiWhat.Text; //We would like to search inside the articles

        WikiSearchResponse resp = await client.SearchAsync(req).ConfigureAwait(false);

        Console.WriteLine($"Searching for {req.Query}");
        Console.WriteLine();
        Console.WriteLine($"Found {resp.QueryResult.SearchResults.Count} English results:");

        foreach (SearchResult s in resp.QueryResult.SearchResults)
        {
            Console.WriteLine($" - {s.Title}");
        }

        Console.WriteLine();
        Console.WriteLine();

        //We change the language to Spanish
        req.WikiLanguage = WikiLanguage.Spanish;

        resp = await client.SearchAsync(req).ConfigureAwait(false);

        Console.WriteLine($"Found {resp.QueryResult.SearchResults.Count} Spanish results:");

        foreach (SearchResult s in resp.QueryResult.SearchResults)
        {
            Console.WriteLine($" - {s.Title}");
        }
    }
}