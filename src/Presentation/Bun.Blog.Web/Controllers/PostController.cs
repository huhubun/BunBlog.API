using Bun.Blog.Web.Framework.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bun.Blog.Web.Controllers
{

    public class PostController : AdminBaseController
    {
        public IActionResult List()
        {
            return View();
        }

        public IActionResult New()
        {
            return View();
        }
    }
}
