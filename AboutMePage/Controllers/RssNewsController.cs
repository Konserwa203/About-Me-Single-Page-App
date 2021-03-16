using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AboutMePage.Controllers
{
    public class RssNewsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
