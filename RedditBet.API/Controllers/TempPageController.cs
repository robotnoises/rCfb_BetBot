using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace RedditBet.API.Controllers
{
    using Newtonsoft.Json;
    using RedditBet.API.Models;
    using RedditBet.API.Services;
    
    [RoutePrefix("api/temppage")]
    [EnableCors(origins: "http://localhost:9000", headers: "*", methods: "GET, POST, PUT")]
    public class TempPageController : ApiController
    {
        private TempPageService _service = new TempPageService();
        
        [HttpPost, Route("validate/{token}")]
        public TokenValidationResponse PostValidateToken(string token)
        {
            var status = _service.ValidateToken(token);

            return new TokenValidationResponse(status);
        }

        [HttpGet, Route("bet/{token}")]
        public BetViewModel GetBet(string token)
        {
            var data = _service.GetForToken(token);

            if (data == null)
            {
                return null;
            }

            return data.Bet.ToMappedType();
        }
    }
}
