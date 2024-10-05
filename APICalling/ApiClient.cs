using System.Net.Http;
using System.Web;
using System.Windows;
using System.Xml.Serialization;
using VremenskaPrognoza.Model;

namespace VremenskaPrognoza.APICalling
{
    public static class ApiClient
    {
        private static readonly HttpClient client = new HttpClient();
        private const String APIKEY = "key";
        private const String LOCATION = "q";
        private const String LANGUAGE = "lang";
        private const String AIR_QUALITY = "aqi";
        public const String BaseRealtimeUrl = "http://api.weatherapi.com/v1/current.xml";
        public const String BaseAstronomyUrl = "http://api.weatherapi.com/v1/astronomy.xml";        

        static ApiClient() 
        {
            client.DefaultRequestHeaders.CacheControl = new System.Net.Http.Headers.CacheControlHeaderValue
            {
                NoCache = true,
                NoStore = true,
            }; 
        }

        public static async Task<String> SendGetRequestAsync(String url, String key, String location, String lang)
        {
            // creating URL with parameters
            var builder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(builder.ToString());
            query[APIKEY] = key;
            query[LOCATION] = location;
            query[LANGUAGE] = lang;
            query[AIR_QUALITY] = "yes";

            builder.Query = query.ToString();
            string urlWithParams = builder.ToString();

            // sending http GET request and returning response
            HttpResponseMessage response = await client.GetAsync(urlWithParams);
            string responseBody = await response.Content.ReadAsStringAsync();

            if (response.IsSuccessStatusCode)
            {
                return responseBody;
            }
            else
            {
                throw new RequestError(responseBody);                
            }
        }
    }
}
