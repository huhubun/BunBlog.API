using Bun.Blog.Core.Domain.Users;
using Bun.Blog.Data;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Admin.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<User> _userManager;

        public HomeController(ILogger<HomeController> logger,
            UserManager<User> userManager)
        {
            _logger = logger;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var currentUser = (await _userManager.GetUserAsync(HttpContext.User))?.UserName;

            _logger.LogInformation($"Index page says hello, current user is {currentUser}");

            ViewData["CurrentUser"] = currentUser;

            return View();
        }
    }
}
