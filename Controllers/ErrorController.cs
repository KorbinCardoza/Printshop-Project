using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using PrintShop.ViewModels;

namespace PrintShop.Controllers
{
    public class ErrorController : Controller
    {
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult Error(int? statusCode = null)
        {
            return View(new ErrorViewModel
            {
                Title = "Something went wrong...",
                Explanation = "Your request resulted in an error.",
                StatusCode = statusCode,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [AllowAnonymous]
        public IActionResult StatusError(int statusCode)
        {
            HttpContext.Response.StatusCode = statusCode;

            return statusCode switch
            {
                404 => View("Error",
                    new ErrorViewModel
                    {
                        Title = "You seem a little lost...",
                        Explanation = "The page you requested could not be found.",
                        StatusCode = statusCode,
                        RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                    }),
                _ => RedirectToAction("Error", new { statusCode })
            };
        }
    }
}
