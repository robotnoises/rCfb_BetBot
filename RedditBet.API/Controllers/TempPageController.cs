using System;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace RedditBet.API.Controllers
{
    using RedditBet.API.Models;
    using RedditBet.API.Services;

    [RoutePrefix("api/token")]
    public class TempPageController : ApiController
    {
        [Route("validate/{token}")]
        public TokenValidationResponse PostValidateToken(string token)
        {
            var service = new TempPageService();

            var status = service.ValidateToken(token);

            return new TokenValidationResponse(status);
        }
    }
}
