using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Extensions;
using Genbox.Wikipedia.Objects;
using Xunit;

namespace Genbox.Wikipedia.Tests;

public sealed class WikiSearchRequestTests : IDisposable
{
    private readonly WikipediaClient _client;

    public WikiSearchRequestTests()
    {
        _client = new WikipediaClient();
    }

    [Fact]
    public async Task NamespacesToIncludeTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.NamespacesToInclude = WikiNamespace.Category | WikiNamespace.CategoryTalk;
        req.Limit = 100;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);

        //Ideally we should have results from both categories
        Assert.Contains(resp.QueryResult.SearchResults, x => x.Namespace.ToString() == WikiNamespace.Category.GetStringValue());
        Assert.Contains(resp.QueryResult.SearchResults, x => x.Namespace.ToString() == WikiNamespace.CategoryTalk.GetStringValue());
    }

    [Fact]
    public async Task LimitAndOffsetTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.Limit = 1;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        SearchResult? item = Assert.Single(resp.QueryResult.SearchResults);

        req.Offset = 1;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);

        SearchResult? otherItem = Assert.Single(resp.QueryResult.SearchResults);

        Assert.NotEqual(item, otherItem);
    }

    [Fact]
    public async Task QueryIndependentProfileTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.QueryIndependentProfile = WikiQueryProfile.Classic;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.NotNull(resp.QueryResult);
    }

    [Fact]
    public async Task WhatToSearchTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.WhatToSearch = WikiWhat.Text;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.NotNull(resp.QueryResult);
    }

    [Fact]
    public async Task InfoToIncludeTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstei");
        req.InfoToInclude = WikiInfo.TotalHits | WikiInfo.Suggestion;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("albert einstein", resp.QueryResult.SearchInfo.Suggestion);
        Assert.Equal("albert einstein", resp.QueryResult.SearchInfo.SuggestionSnippet);
        Assert.NotEqual(0, resp.QueryResult.SearchInfo.TotalHits);
    }

    [Fact]
    public async Task PropertiesToIncludeTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.PropertiesToInclude = WikiProperty.All;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);

        SearchResult s = resp.QueryResult.SearchResults[0];

        Assert.NotNull(s.CategorySnippet);
        Assert.NotNull(s.IsFileMatch);
        Assert.NotEqual(0, s.Size);
        Assert.NotEqual(0, s.PageId);
        Assert.NotNull(s.Snippet);
        Assert.NotNull(s.TimeStamp);
        Assert.NotNull(s.Title);
        Assert.NotNull(s.TitleSnippet);
        Assert.NotEqual(0, s.WordCount);

        req = new WikiSearchRequest("Albert Einstein");
        req.PropertiesToInclude = WikiProperty.Size;

        resp = await _client.SearchAsync(req).ConfigureAwait(false);

        s = resp.QueryResult.SearchResults[0];
        Assert.NotEqual(0, s.Size);
        Assert.NotNull(s.Title);
        Assert.Null(s.CategorySnippet);
        Assert.Null(s.IsFileMatch);
        Assert.Null(s.Snippet);
        Assert.Null(s.TimeStamp);
        Assert.Null(s.TitleSnippet);
        Assert.Null(s.WordCount);
    }

    [Fact]
    public async Task IncludeInterWikiResultsTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstei");
        req.IncludeInterWikiResults = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.NotNull(resp.QueryResult);
    }

    [Fact]
    public async Task SortOrderTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.SortOrder = WikiSortOrder.CreateTimestampAsc;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        SearchResult item = resp.QueryResult.SearchResults[0];

        req.SortOrder = WikiSortOrder.CreateTimestampDesc;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);

        SearchResult otherItem = resp.QueryResult.SearchResults[0];

        Assert.NotEqual(item, otherItem);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}