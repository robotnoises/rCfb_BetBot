﻿using System;
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
        private Uri _url;
        private Method _requestMethod;
        private object _data;
        private readonly string _userAgent = Config.Bot_UserAgent;

        public Requester(string url, RequestMethod method = RequestMethod.GET, object data = null)
        {
            _client = new RestClient();
            _requestMethod = GetRequestMethod(method);
            _url = url.ToUri();
            _data = data;
        }

        public IRestResponse GetResponse()
        {
            _client.BaseUrl = _url;

            var request = new RestRequest(_requestMethod);

            request.AddHeader("user-agent", _userAgent);
            request.RequestFormat = DataFormat.Json;
            
            if (_data != null)
            {
                request.AddBody(_data);
            }

            return _client.Execute(request);
        }

        // Private methods

        private Method GetRequestMethod(RequestMethod method)
        {
            switch (method)
            { 
                case RequestMethod.GET:
                    return Method.GET;
                case RequestMethod.POST:
                    return Method.POST;
                case RequestMethod.PUT:
                    return Method.PUT;
                case RequestMethod.DELETE:
                    return Method.DELETE;
                default:
                    return Method.GET;
            }
        }
    }
}
