using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CollegeApp.Controllers
{
    public class UploadsController : Controller
    {
        private readonly IHostingEnvironment hostingEnvironment;
        public UploadsController(IHostingEnvironment environment)
        {
            hostingEnvironment = environment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> PostFile(IFormFile formFile)
        {
            if (formFile.Length > 0)
            {
                string fileName = Path.GetRandomFileName() + formFile.FileName;
                string filePath = Path.Combine(hostingEnvironment.WebRootPath, "uploads", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }
                return Ok(new { fileName });
            }
            return null;
        }
    }
}