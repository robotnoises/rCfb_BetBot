﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RestSharp;
using RedditBet.Bot.Utils;
using RedditBet.Bot.Enums;

namespace RedditBet.Bot.Utils
{
    public class Requester
    {
        // Private fields
        
        private RestClient _client;
        private Method _requestMethod;
        private Uri _url;
        private string _cookieValue;
        private object _data;

        // Constants

        private const string _userAgent = Config.Bot_UserAgent;

        public Requester(string url, object data = null)
        {
            _client = new RestClient();
            _requestMethod = Method.GET;
            _url = Converter.ToUri(url);
            _data = data;
        }

        public Requester(string url, RequestMethod method, object data = null)
        {
            _client = new RestClient();
            _requestMethod = GetRequestMethod(method);
            _url = Converter.ToUri(url);
            _data = data;
        }

        public Requester(string url, string cookie, RequestMethod method, object data = null)
        {
            _client = new RestClient();
            _requestMethod = GetRequestMethod(method);
            _url = Converter.ToUri(url);
            _data = data;
            _cookieValue = cookie;
        }

        public IRestResponse GetResponse()
        {
            _client.BaseUrl = _url;

            var request = new RestRequest(_requestMethod);

            request.AddHeader("user-agent", _userAgent);
            request.RequestFormat = DataFormat.Json;
            // request.AddCookie("__cfduid", "da45b6233659c5efdb44109e01ae097e11419025763"); // temp, need to persist this

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
