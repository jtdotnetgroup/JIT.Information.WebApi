using Microsoft.AspNetCore.Antiforgery;
using JIT.InfomationSystem.Controllers;

namespace JIT.InfomationSystem.Web.Host.Controllers
{
    public class AntiForgeryController : InfomationSystemControllerBase
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
