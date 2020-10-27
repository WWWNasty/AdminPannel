using Microsoft.AspNetCore.Mvc;

namespace Admin.Panel.Web.Controllers
{
    public class ErrorController : Controller
    {
        // GET
        public IActionResult Error()
        {
            return View();
        }
    }
}