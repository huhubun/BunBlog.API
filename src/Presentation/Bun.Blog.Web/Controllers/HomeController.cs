using Bun.Blog.Data;
using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Controllers
{
    public class HomeController : BaseController
    {
        public HomeController(BlogContext context)
        {

        }

        public IActionResult Index()
        {

            return View();
        }
    }
}
