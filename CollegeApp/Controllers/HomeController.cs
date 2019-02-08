using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
{
    public class HomeController : Controller
    {
        [Route("")]
        [Route("/Courses")]
        [Route("/Teachers")]
        [Route("/Students")]
        [Route("/Subjects")]
        public IActionResult Index()
        {
            return View();
        }
    }
}