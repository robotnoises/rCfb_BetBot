using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft;
using RedditBet.Bot.Utils;
using RestSharp;

namespace RedditBet.Bot.Data
{
    public class Api
    {
        public void Get()
        {
            var client = new RestClient();

            client.BaseUrl = Converter.ToUri(Config.Api_Base());

            var request = new RestRequest(Method.GET);

            request.Resource = Config.Api_Tasks(false);

            var response = client.Execute(request);

            var foo = "bar";
            
        }

        
    }
}
