using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RedditBet.Bot.Utils;

namespace RedditBet.Bot.Utils
{
    public class Requester
    {
        // Private fields
        
        private RestClient _client;
        private Method _requestMethod;
        private Uri _url;

        // Constants

        private const string _userAgent = "cfb_betbot 0.1 @robotnoises";
        
        public Requester(string url)
        {
            _client = new RestClient();
            _requestMethod = Method.GET;
            _url = Converter.ToUri(url);
        }

        public Requester(string url, Method method)
        {
            _client = new RestClient();
            _requestMethod = method;
            _url = Converter.ToUri(url);
        }

        public IRestResponse GetResponse()
        {
            _client.BaseUrl = _url;

            var request = new RestRequest(_requestMethod);

            request.AddHeader("user-agent", _userAgent);

            return _client.Execute(request);
        }
    }
}
