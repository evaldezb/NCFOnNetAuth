using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NCFOnNetAuth
{
    public class MyRequirementHandler : AuthorizationHandler<MyRequirementHandler>, IAuthorizationRequirement
    {
        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, MyRequirementHandler requirement)
        {

            var baseAddress = "http://localhost:8001/api/";
            HttpClient hc = new HttpClient();
            hc.BaseAddress = new Uri(baseAddress);

            var obj = new JObject();
            obj["username"] = "test";
            obj["password"] = "test";

            var content = new StringContent(obj.ToString(), Encoding.UTF8, "application/json");
            var result = await hc.PostAsync("common/v1/login", content);

            var token = result.Headers.GetValues("x-auth-token").FirstOrDefault();

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
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                
                Debug.WriteLine(objectDeserialize);
            }
            
            context.Succeed(requirement);
        }
    }

}
