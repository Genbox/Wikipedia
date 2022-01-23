using Genbox.Wikipedia.Enums;
using Xunit;

namespace Genbox.Wikipedia.Tests;

public sealed class WikiMediaRequestTests : IDisposable
{
    private readonly WikipediaClient _client;

    public WikiMediaRequestTests()
    {
        _client = new WikipediaClient();
    }

    [Fact]
    public async Task AssertTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.Assert = "anon";

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Null(resp.Error); //We expect there to be no error since we are not logged in

        req.Assert = "user";
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.NotNull(resp.Error); //We expect an error because we are not logged in
        Assert.Equal("assertuserfailed", resp.Error.Code);
        Assert.Equal("See https://en.wikipedia.org/w/api.php for API usage. Subscribe to the mediawiki-api-announce mailing list at &lt;https://lists.wikimedia.org/postorius/lists/mediawiki-api-announce.lists.wikimedia.org/&gt; for notice of API deprecations and breaking changes.", resp.Error.DocumentReference);
        Assert.Equal("You are no longer logged in, so the action could not be completed.", resp.Error.Info);
    }

    [Fact]
    public async Task AssertUserTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.AssertUser = "someone";

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.Error.Code);
        Assert.Equal("See https://en.wikipedia.org/w/api.php for API usage. Subscribe to the mediawiki-api-announce mailing list at &lt;https://lists.wikimedia.org/postorius/lists/mediawiki-api-announce.lists.wikimedia.org/&gt; for notice of API deprecations and breaking changes.", resp.Error.DocumentReference);
        Assert.Equal("You are no longer logged in as \"Someone\", so the action could not be completed.", resp.Error.Info);
    }

    [Fact]
    public async Task IncludeServedByTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.IncludeServedBy = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.NotNull(resp.ServedBy);
    }

    [Fact]
    public async Task IncludeCurrentTimestampTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.IncludeCurrentTimestamp = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal(DateTime.UtcNow, resp.CurrentTimestamp.DateTime, TimeSpan.FromMinutes(1));
    }

    [Fact]
    public async Task IncludeLanguageUsedTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.IncludeLanguageUsed = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("en", resp.ErrorLanguage);
        Assert.Equal("en", resp.Language);
    }

    [Fact]
    public async Task RequestIdTest()
    {
        Guid id = Guid.NewGuid();

        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.RequestId = id.ToString();

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal(id.ToString(), resp.RequestId);
    }

    [Fact]
    public async Task LanguageToUseAndVariantTest()
    {
        //Go to https://www.mediawiki.org/w/api.php?action=query&meta=siteinfo&siprop=languages&formatversion=2 to see languages

        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.LanguageToUse = "es";
        req.LanguageVariant = "es-419";
        req.IncludeLanguageUsed = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("es", resp.Language);
    }

    [Fact(Skip = "Unable to create a working test case")]
    public async Task ErrorLanguageToUseTest()
    {
        //Go to https://www.mediawiki.org/w/api.php?action=query&meta=siteinfo&siprop=languages&formatversion=2 to see languages

        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.WikiLanguage = WikiLanguage.Spanish;
        req.IncludeLanguageUsed = true;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("es", resp.ErrorLanguage);
    }

    [Fact]
    public async Task ErrorFormatTest()
    {
        WikiSearchRequest req = new WikiSearchRequest("Albert Einstein");
        req.AssertUser = "whatever"; //We use AssertUser to provoke an error
        req.ErrorFormat = WikiErrorFormat.Bc;

        WikiSearchResponse resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("You are no longer logged in as \"Whatever\", so the action could not be completed.", resp.Error.Info);

        req.ErrorFormat = WikiErrorFormat.Html;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.ModuleErrors[0].Code);
        Assert.Equal("main", resp.ModuleErrors[0].Module);
        Assert.Equal("You are no longer logged in as \"Whatever\", so the action could not be completed.", resp.ModuleErrors[0].Html);

        req.ErrorFormat = WikiErrorFormat.Plaintext;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.ModuleErrors[0].Code);
        Assert.Equal("main", resp.ModuleErrors[0].Module);
        Assert.Equal("You are no longer logged in as \"Whatever\", so the action could not be completed.", resp.ModuleErrors[0].Text);

        req.ErrorFormat = WikiErrorFormat.None;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.ModuleErrors[0].Code);
        Assert.Equal("main", resp.ModuleErrors[0].Module);
        Assert.Null(resp.ModuleErrors[0].Text);
        Assert.Null(resp.ModuleErrors[0].Html);

        req.ErrorFormat = WikiErrorFormat.Raw;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.ModuleErrors[0].Code);
        Assert.Equal("main", resp.ModuleErrors[0].Module);
        Assert.Equal("apierror-assertnameduserfailed", resp.ModuleErrors[0].Key);
        Assert.Equal("Whatever", resp.ModuleErrors[0].Params[0]);

        req.ErrorFormat = WikiErrorFormat.WikiText;
        resp = await _client.SearchAsync(req).ConfigureAwait(false);
        Assert.Equal("assertnameduserfailed", resp.ModuleErrors[0].Code);
        Assert.Equal("main", resp.ModuleErrors[0].Module);
        Assert.Equal("You are no longer logged in as \"Whatever\", so the action could not be completed.", resp.ModuleErrors[0].Text);
    }

    public void Dispose()
    {
        _client.Dispose();
    }
}