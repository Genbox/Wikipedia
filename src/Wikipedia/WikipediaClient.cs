using System.Text.Json;
using Genbox.Wikipedia.Enums;
using Genbox.Wikipedia.Misc;
using Genbox.Wikipedia.Objects;

namespace Genbox.Wikipedia;

public class WikipediaClient : IDisposable
{
    private readonly HttpClient? _httpClient;
    private readonly JsonSerializerOptions _options;
    private readonly HttpClient? _userClient;

    public WikipediaClient(HttpClient? client = null)
    {
        _userClient = client;

        if (_userClient == null)
            _httpClient = new HttpClient();

        _options = new JsonSerializerOptions();
        _options.PropertyNamingPolicy = LowerCasePolicy.Instance;
    }

    ///<summary>Set to true to use HTTPS instead of HTTP. Defaults to true.</summary>
    public bool UseTls { get; set; } = true;

    ///<summary>The default language to use. Can be overriden on each request.</summary>
    public Language DefaultLanguage { get; set; } = Language.English;

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
            _httpClient?.Dispose();
    }

    public async Task<WikiSearchResponse> SearchAsync(string query, CancellationToken token = default)
    {
        return await SearchAsync(new WikiSearchRequest(query), token).ConfigureAwait(false);
    }

    public async Task<WikiSearchResponse> SearchAsync(WikiSearchRequest request, CancellationToken token = default)
    {
        if (request.Language == Language.NotSet)
            request.Language = DefaultLanguage;

        HttpClient? client = _userClient ?? _httpClient;

        if (client == null)
            throw new InvalidOperationException("Bug check: HttpClient is null");

        using HttpRequestMessage httpReq = CreateHttpRequest(request);
        using HttpResponseMessage? httpResp = await client.SendAsync(httpReq, token).ConfigureAwait(false);
        using Stream? contentStream = await httpResp.Content.ReadAsStreamAsync().ConfigureAwait(false);

        QueryResponse? queryResp = await JsonSerializer.DeserializeAsync<QueryResponse>(contentStream, _options, token).ConfigureAwait(false);

        if (queryResp == null)
            throw new InvalidOperationException("Unable to read query response");

        WikiSearchResponse? wikiSearchResponse = queryResp.Query;

        //We do this to ensure users are not bothered with nullable responses
        if (wikiSearchResponse == null)
            wikiSearchResponse = new WikiSearchResponse();

        //For convenience, we autocreate uris that point directly to the wiki page.
        string prefix = UseTls ? "https://" : "http://";

        foreach (Search? search in wikiSearchResponse.Search)
            search.Url = new Uri(prefix + request.Language.GetStringValue() + ".wikipedia.org/wiki/" + search.Title);

        return wikiSearchResponse;
    }

    private HttpRequestMessage CreateHttpRequest(WikiSearchRequest searchRequest)
    {
        //Required
        Dictionary<string, string> queryParams = new Dictionary<string, string>
                                                 {
                                                     {"action", "query"},
                                                     {"list", "search"},
                                                     {"srsearch", searchRequest.Query},
                                                     {"format", "json"}
                                                 };

        //Optional
        if (searchRequest.Infos.HasElements())
            queryParams.Add("srinfo", string.Join("|", searchRequest.Infos).ToLower());

        if (searchRequest.Limit != 0)
            queryParams.Add("srlimit", searchRequest.Limit.ToString());

        if (searchRequest.Offset != 0)
            queryParams.Add("sroffset", searchRequest.Offset.ToString());

        if (searchRequest.Namespaces.HasElements())
            queryParams.Add("srnamespace", string.Join("|", searchRequest.Namespaces).ToLower());

        if (searchRequest.Properties.HasElements())
            queryParams.Add("srprop", string.Join("|", searchRequest.Properties).ToLower());

        if (searchRequest.Redirects)
            queryParams.Add("srredirects", searchRequest.Redirects.ToString().ToLower());

        if (searchRequest.What != What.Title)
            queryParams.Add("srwhat", searchRequest.What.ToString().ToLower());

        if (searchRequest.IncludeServedBy)
            queryParams.Add("servedby", searchRequest.IncludeServedBy.ToString().ToLower());

        if (searchRequest.RequestId != null)
            queryParams.Add("requestid", searchRequest.RequestId);

        //API example: http://en.wikipedia.org/w/api.php?action=query&list=search&srsearch=wikipedia&srprop=timestamp
        Uri baseUrl = new Uri(string.Format(UseTls ? "https://{0}.wikipedia.org/w/" : "http://{0}.wikipedia.org/w/", searchRequest.Language.GetStringValue()));

        return new HttpRequestMessage(HttpMethod.Get, new Uri(baseUrl, "api.php?" + UrlHelper.CreateQueryString(queryParams)));
    }
}