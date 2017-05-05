using Bun.Blog.Web.Framework.Mvc.Json;
using Bun.Blog.Web.Framework.UI;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Bun.Blog.Web.Framework.Mvc.Controllers
{
    public class BaseController : Controller
    {
        private static string NOTIFICATION_KEY = "bun.notifications.{0}";

        #region Json

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

        #endregion

        #region Notification

        protected void SuccessNotification(string message)
        {
            AddNotification(NotifyType.Success, message);
        }

        protected void ErrorNotification(string message)
        {
            AddNotification(NotifyType.Error, message);
        }

        protected void AddNotification(NotifyType type, string message)
        {
            var key = String.Format(NOTIFICATION_KEY, type);

            if (TempData[key] == null)
            {
                TempData[key] = new List<string>();
            }

            (TempData[key] as List<string>).Add(message);
        }

        #endregion
    }
}
