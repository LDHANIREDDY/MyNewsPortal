using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MyNewsPortals.Models;
using MyNewsPortals.Services;

namespace MyNewsPortals.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly NewsService _newsService;

        public HomeController(ILogger<HomeController> logger, NewsService newsService)
        {
            _logger = logger;
            _newsService = newsService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> India()
        {
            var news = await _newsService.GetTopHeadlinesByCountryAsync("India");
            ViewBag.Country = "India";
            return View("Country", news);
        }

        public async Task<IActionResult> SriLanka()
        {
            var news = await _newsService.GetTopHeadlinesByCountryAsync("lk");
            ViewBag.Country = "Sri Lanka";
            return View("Country", news);
        }

        public async Task<IActionResult> Germany()
        {
            var news = await _newsService.GetTopHeadlinesByCountryAsync("de");
            ViewBag.Country = "Germany";
            return View("Country", news);
        }

        public async Task<IActionResult> Russia()
        {
            var news = await _newsService.GetTopHeadlinesByCountryAsync("ru");
            ViewBag.Country = "Russia";
            return View("Country", news);
        }

        public async Task<IActionResult> USA()
        {
            var news = await _newsService.GetTopHeadlinesByCountryAsync("us");
            ViewBag.Country = "USA";
            return View("Country", news);
        }
    }
}
