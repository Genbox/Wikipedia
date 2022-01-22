# Wikipedia - An implementation of the full text search API of Wikipedia

[![NuGet](https://img.shields.io/nuget/v/Genbox.Wikipedia.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Genbox.Wikipedia/)

### Features

* Support for all 283 languages on Wikipedia
* Full support for all search parameters

### Example

Here is the simplest form of getting data from Wikipedia:

```csharp
static void Main()
{
    WikipediaClient client = new WikipediaClient();

    //We would like to search inside articles
    client.What = What.Text;

    QueryResult results = await client.SearchAsync("Microsoft C#");

    foreach (Search result in results.Search)
    {
        Console.WriteLine(result.Title);
    }
}
```

Output:
```
http://en.wikipedia.org/wiki/Visual C++
http://en.wikipedia.org/wiki/Microsoft Visual C Sharp
http://en.wikipedia.org/wiki/Microsoft Roslyn
http://en.wikipedia.org/wiki/C Sharp (programming language)
http://en.wikipedia.org/wiki/Microsoft Visual Studio
```