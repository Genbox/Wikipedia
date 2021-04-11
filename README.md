# Wikipedia - An implementation of the full text search API of Wikipedia

[![NuGet](https://img.shields.io/nuget/v/Genbox.Wikipedia.svg?style=flat-square&label=nuget)](https://www.nuget.org/packages/Genbox.Wikipedia/)

### Features

* Based on RestSharp (http://restsharp.org) to deserialize the Wikipedia XML into objects
* Support for all 283 languages on Wikipedia
* Full support for all search parameters

### Examples

Here is the simplest form of getting data from Wikipedia:

```csharp
static void Main()
{
	WikipediaClient client = new WikipediaClient();
	client.Limit = 5;
	
	QueryResult results = wikipedia.Search("Microsoft C#");

	Console.WriteLine("Found " + results.Search.Count + " English results:");

	foreach (Search s in results.Search)
	{
		Console.WriteLine(s.Url);
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