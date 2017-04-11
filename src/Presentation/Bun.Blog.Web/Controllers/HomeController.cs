using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Data;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly UserManager<User> _userManager;

        public HomeController(BlogContext context,
            UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            ViewData["CurrentUser"] = (await _userManager.GetUserAsync(HttpContext.User))?.UserName;

            return View();
        }
    }
}
