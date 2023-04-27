using System.Text.Json;

namespace BingCustomSearch
{
    public class CustomSearch
    {
        private HttpClient httpClient { get; set; }
        private string customConfigId { get; set; }
        public string query { get; set; }
        public ushort maxResults { get; set; } = 50;
        public string queryString { get; set; }
        private static readonly Dictionary<string, string> Endpoint = new()
        {
            ["WebSearch"] = @"https://api.bing.microsoft.com/v7.0/custom/search?",
            ["AutoSuggest"] = @"https://api.bing.microsoft.com/v7.0/custom/suggestions/search?",
            ["ImageSearch"] = @"https://api.bing.microsoft.com/v7.0/custom/images/search?",
            ["VideoSearch"] = @"https://api.bing.microsoft.com/v7.0/custom/videos/search?"
        };
        public CustomSearch(string subscriptionKey, string configId)
        {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", subscriptionKey);
            customConfigId = configId;
        }
        public BingResponse GetWebResults(string searchTerms)
        {
            var response = httpClient.GetAsync(Endpoint["WebSearch"] + "q=" + searchTerms + "&customconfig=" + customConfigId + "&count=" + maxResults).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<SearchResponse>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return JsonSerializer.Deserialize<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public BingResponse GetImageResults(string searchTerms)
        {
            var response = httpClient.GetAsync(Endpoint["ImageSearch"] + "q=" + searchTerms + "&customconfig=" + customConfigId + "&count=" + maxResults).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<ImageAnswer>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return JsonSerializer.Deserialize<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public BingResponse GetVideoResults(string searchTerms)
        {
            var response = httpClient.GetAsync(Endpoint["VideoSearch"] + "q=" + searchTerms + "&customconfig=" + customConfigId + "&count=" + maxResults).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<VideoAnswer>(response.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return JsonSerializer.Deserialize<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }
        public BingResponse GetSuggestionResults(string searchTerms)
        {
            var response = httpClient.GetAsync(Endpoint["AutoSuggest"] + "q=" + searchTerms + "&customconfig=" + customConfigId + "&count=" + maxResults).Result;
            if (response.IsSuccessStatusCode)
            {
                return JsonSerializer.Deserialize<Suggestions>(response.ToString());
            }
            else
            {
                return JsonSerializer.Deserialize<ErrorResponse>(response.Content.ReadAsStringAsync().Result);
            }
        }
    }

    internal class Error
    {
        public string code { get; set; }
        public string message { get; set; }
        public string? moreDetails { get; set; }
        public string? parameter { get; set; }
        public string? subCode { get; set; }
        public string? value { get; set; }
    }

    internal class ErrorResponse : BingResponse
    {
        //public string _type = "ErrorResponse";
        public Error[] errors { get; set; }
    }
    internal class Image
    {
        public string accentColor { get; set; }
        public string contentSize { get; set; }
        public string contentUrl { get; set; }
        public DateTime datePublished { get; set; }
        public string encodingFormat { get; set; }
        public ushort height { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string imageId { get; set; }
        public string name { get; set; }
        public MediaSize thumbnail { get; set; }
        public string thumbnailUrl { get; set; }
        public string webSearchUrl { get; set; }
        public ushort width { get; set; }
    }

    internal class ImageAnswer : BingResponse
    {
        //public string _type { get; set; }
        public int nextOffset { get; set; }
        public Pivot pivotSuggestions { get; set; }
        public Query queryExpansions { get; set; }
        public string readLink { get; set; }
        public Query similarTerms { get; set; }
        public long totalEstimatedMatches { get; set; }
        public Image[] value { get; set; }
        public string webSearchUrl { get; set; }

    }

    internal class MediaSize
    {
        public int height { get; set; }
        public int width { get; set; }
    }

    internal class MetaTag
    {
        public string content { get; set; }
        public string name { get; set; }
    }

    internal class OpenGraphImage
    {
        public Uri contentUrl { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    internal class Pivot
    {
        public string pivot { get; set; }
        public Query suggestions { get; set; }
    }

    internal class Publisher
    {
        public string? name { get; set; }
    }

    internal class Query
    {
        public string displayText { get; set; }
        public string searchUrl { get; set; }
        public string text { get; set; }
        public Thumbnail thumbnail { get; set; }
        public string webSearchUrl { get; set; }
    }

    internal class QueryContext
    {
        public bool adultIntent { get; set; }
        public string alterationOverrideQuery { get; set; }
        public string alteredQuery { get; set; }
        public bool askUserForLocation { get; set; }
        public string originalQuery { get; set; }
    }

    internal class SearchAction
    {
        public string displayText { get; set; }
        public string query { get; set; }
        public string searchKind { get; set; }
    }

    internal class SearchResponse : BingResponse
    {
        //public string _type { get; set; }
        public WebAnswer webPages { get; set; }
        public QueryContext queryContext { get; set; }
        public ImageAnswer images { get; set; }
        public VideoAnswer videos { get; set; }
    }

    internal class SuggestionGroup
    {
        public string name { get; set; }
        public SearchAction[] searchSuggestions { get; set; }
    }

    internal class Suggestions : BingResponse
    {
        //public string _type { get; set; }
        public SuggestionGroup[] suggestionsGroups { get; set; }
    }

    internal class Thing
    {
        public string name { get; set; }
    }

    internal class Thumbnail
    {
        public string url { get; set; }
    }

    internal class Video
    {
        public bool allowHttpsEmbed { get; set; }
        public bool allowMobileEmbed { get; set; }
        public Publisher creator { get; set; }
        public string contentUrl { get; set; }
        public DateTime? datePublished { get; set; }
        public string description { get; set; }
        public string duration { get; set; }
        public string embedHtml { get; set; }
        public string encodingFormat { get; set; }
        public int height { get; set; }
        public string hostPageDisplayUrl { get; set; }
        public string hostPageUrl { get; set; }
        public string id { get; set; }
        public bool isAccessibleForFree { get; set; }
        public bool isSuperFresh { get; set; }
        public Thing mainEntity { get; set; }
        public string motionThumbnailUrl { get; set; }
        public string name { get; set; }
        // this is not listed as array in MSDOCS
        public Publisher[] publisher { get; set; }
        public MediaSize thumbnail { get; set; }
        public string thumbnailUrl { get; set; }
        public string videoId { get; set; }
        public int viewCount { get; set; }
        public string webSearchUrl { get; set; }
        public int width { get; set; }
    }
    internal class VideoAnswer : BingResponse
    {
        //public string _type { get; set; }
        public int nextOffset { get; set; }
        public Pivot pivotSuggestions { get; set; }
        public Query queryExpansions { get; set; }
        public long totalEstimatedMatches { get; set; }
        public Video[] value { get; set; }
        public string webSearchUrl { get; set; }
    }

    internal class WebAnswer
    {
        public string webSearchUrl { get; set; }
        public string webSearchUrlPingSuffix { get; set; }
        public int totalEstimatedMatches { get; set; }
        public List<WebPage> value { get; set; }
    }

    internal class WebPage
    {
        public string name { get; set; }
        public string url { get; set; }
        public string id { get; set; }
        public string urlPingSuffix { get; set; }
        public string snippet { get; set; }
        public bool isFamilyFriendly { get; set; }
        public string displayUrl { get; set; }
        public DateTime? dateLastCrawled { get; set; }
        public bool isNavigational { get; set; }
        public bool fixedPosition { get; set; }
        public string language { get; set; }
        public OpenGraphImage openGraphImage { get; set; }
    }
    public abstract class BingResponse
    {
        public string _type { get; set; }
    }
}