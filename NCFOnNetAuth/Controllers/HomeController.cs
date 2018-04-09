using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace NCFOnNetAuth.Controllers
{
    [Route("api/home")]
    [Authorize(Roles = "ROLE_ADMIN")]
    public class HomeController : Controller
    {

        public string Get()
        {
            var pHeader = HttpContext.Items["pito"];
            return "Hola Mundo !" + pHeader;
        }

    }
}