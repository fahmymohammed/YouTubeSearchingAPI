using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using YouTubeServices.Services;
using YouTubeServices.Validation;
using YouTubeWeb.Models;

namespace YouTubeWeb.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IYoutubeService _youtubeService;
        private readonly IValidation _validation;

        public HomeController(ILogger<HomeController> logger, IYoutubeService youtubeService, IValidation validation)
        {
            _logger = logger;
            _youtubeService = youtubeService;
            _validation = validation;
        }

        public IActionResult Index()
        {
            return View();
        }

        //Json Call for Get Youtube Videos List
        public async Task<ActionResult> YoutubeEndpointCall(string keywords)
        {
            //Validation
            if (_validation.ValidationForNull(keywords))
            {
                return Json(new { status = false, responseText = "Please Make Sure you Write Something" });
            }

            try
            {
                //Prepare and call the youtube Endpoit
                var youtuberetrieve = await _youtubeService.GetYouTubeVideos(keywords);

                //check the Flag/Status
                if (youtuberetrieve.status)
                {
                    return Json(new { status = true, responseText = youtuberetrieve.message, allvideos = youtuberetrieve.videoslist.ToList() });
                }
                else
                {
                    return Json(new { status = false, responseText = youtuberetrieve.message });
                }

            }
            catch (Exception)
            {
                return Json(new { status = false, responseText = "Error fetching data..." });
            }
        }

        public IActionResult About()
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
    }
}