using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NasaImageOfTheDay.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using NasaImageOfTheDay.Util;
using System.IO;

namespace NasaImageOfTheDay.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            Image image = new Image();
            dynamic config = JSON.Parsh(JSON.GetLocal(Directory.GetCurrentDirectory() + @"\Data\Config.json"));
            dynamic nasa = JSON.Parsh(JSON.GetOnline("https://api.nasa.gov/planetary/apod?api_key=" + config.key));

            image.Title = nasa.title;
            image.Explanation = nasa.explanation;
            image.Url = nasa.url;

            return View(image);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
