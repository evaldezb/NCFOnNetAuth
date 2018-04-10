using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NCFOnNetAuth.Controllers
{
    [Route("api/home")]
    [Authorize(Roles = "ROLE_ADMIN, ROLE_CC_SUPERVISOR")]
    public class HomeController : Controller
    {

        public string Get()
        {
            var pHeader = HttpContext.Items["pito"];
            return "Hola Mundo !" + pHeader;
        }

    }
}