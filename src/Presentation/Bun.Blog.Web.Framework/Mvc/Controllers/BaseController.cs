using Bun.Blog.Web.Framework.Mvc.Json;
using Microsoft.AspNetCore.Mvc;

namespace Bun.Blog.Web.Framework.Mvc.Controllers
{
    public class BaseController : Controller
    {
        protected IActionResult BunJson()
        {
            return Json(new JsonResponse());
        }

        protected IActionResult BunJson(JsonResponseStatus status, string message)
        {
            return Json(new JsonResponse(status, message));
        }

        protected IActionResult BunJson<T>(T content)
        {
            return Json(new JsonResponse<T>(content));
        }

        protected IActionResult BunJson<T>(T content, JsonResponseStatus status, string message)
        {
            return Json(new JsonResponse<T>(content, status, message));
        }

        protected IActionResult PlainJson(object content)
        {
            return Json(new PlainJsonResponse(content));
        }
    }
}
