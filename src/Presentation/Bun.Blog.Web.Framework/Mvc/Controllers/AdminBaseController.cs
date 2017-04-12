using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bun.Blog.Web.Framework.Mvc.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class AdminBaseController : BaseController
    {
    }
}
