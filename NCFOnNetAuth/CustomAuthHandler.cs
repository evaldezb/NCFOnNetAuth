using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;

namespace NCFOnNetAuth
{
    public class CustomAuthHandler : AuthenticationHandler<CustomAuthOptions>
    {
        public CustomAuthHandler(IOptionsMonitor<CustomAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder, ISystemClock clock) : base(options, logger, encoder, clock)
        {
            // store custom services here...
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var authorizationHeader = Context.Request.Headers["Authorization"];
            if (!authorizationHeader.Any())
                return AuthenticateResult.Fail("No contiene header");

            var value = authorizationHeader.ToString();
            if (string.IsNullOrWhiteSpace(value))
                return AuthenticateResult.Fail("El header no contiene valor");

            var token = value.Split(' ')[1];

            var claims = new Claim[] { };
            var baseAddress = "http://localhost:8001/api/";
            HttpClient query2 = new HttpClient();
            query2.BaseAddress = new Uri(baseAddress);
            query2.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");

            var response = query2.GetAsync("backoffice/v1/users/userInfo").Result;

            if (response.IsSuccessStatusCode)
            {
                RootObject objectDeserialize = new RootObject();
                try
                {
                    var result2 = response.Content.ReadAsStringAsync().Result;
                    objectDeserialize = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(result2);

                    claims = new[]
                    {
                        new Claim(ClaimTypes.Name, objectDeserialize.username),
                        new Claim(ClaimTypes.Role, objectDeserialize.roles.First().name)
                    };

                }
                catch (Exception ex)
                {
                    throw ex;
                }

                Debug.WriteLine(objectDeserialize);
            }
            
            // create a new claims identity and return an AuthenticationTicket 
            // with the correct scheme
            var claimsIdentity = new ClaimsIdentity(claims, "Custom Auth");

            var ticket = new AuthenticationTicket(new ClaimsPrincipal(claimsIdentity), new AuthenticationProperties(), "Custom Scheme");

            return AuthenticateResult.Success(ticket);
        }
    }

}
