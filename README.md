# Wikipedia - An implementation of the full text search API of Wikipedia

[![NuGet](https://img.shields.io/nuget/v/Genbox.Wikipedia.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Genbox.Wikipedia/)
[![Build](https://img.shields.io/github/workflow/status/Genbox/Wikipedia/Generic%20build?label=Build)](https://github.com/Genbox/Wikipedia/actions)
[![Release](https://img.shields.io/github/workflow/status/Genbox/Wikipedia/Nuget%20release?label=Release)](https://github.com/Genbox/Wikipedia/actions)
[![License](https://img.shields.io/github/license/Genbox/Wikipedia)](https://github.com/Genbox/Wikipedia/blob/master/LICENSE.txt)

### Features

* Support for all 283 languages on Wikipedia
* Support for all search parameters as of MediaWiki v1.24

### Example

Here is the simplest way of getting data from Wikipedia:

```csharp
static void Main()
{
    using WikipediaClient client = new WikipediaClient();
    
    WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
    req.Limit = 5; //We would like 5 results
    
    WikiSearchResponse resp = await client.SearchAsync(req);
    
    foreach (SearchResult s in resp.QueryResult.SearchResults)
    {
        Console.WriteLine($" - {s.Title}");
    }
}
```

Output:
```
 - Albert Einstein
 - Hans Albert Einstein
 - Einstein family
 - Albert Brooks
 - Albert Einstein College of Medicine
```