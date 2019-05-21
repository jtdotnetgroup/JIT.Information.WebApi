using Microsoft.AspNetCore.Antiforgery;
using JIT.InformationSystem.Controllers;

namespace JIT.InformationSystem.Web.Host.Controllers
{
    public class AntiForgeryController : InformationSystemControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
