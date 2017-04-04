using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Controllers
{
    public class DashboardController : AdminBaseController
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
