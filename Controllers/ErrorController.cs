using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace TheBlogProject.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Oops! That page can’t be found.";
                    return View("NotFound");
                default:
                    ViewBag.ErrorMessage = "An error occurred.";
                    return View("Error");
            }


           
        }
    }
}
