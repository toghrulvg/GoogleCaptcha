using GoogleRecaptcha.Models;
using GoogleRecaptcha.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Diagnostics;

namespace GoogleRecaptcha.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IOptions<GoogleCaptureConfig> _GoogleCaptchaConfiggg;
        private readonly GoogleCaptchaService _capthcaService;

        [BindProperty] public AppUser AppUser { get; set; } = new AppUser();


        public HomeController(ILogger<HomeController> logger , IOptions<GoogleCaptureConfig> GoogleCaptchaConfiggg , GoogleCaptchaService capthcaService)
        {
            _logger = logger;
            _GoogleCaptchaConfiggg = GoogleCaptchaConfiggg;
            _capthcaService = capthcaService;
        }

        public IActionResult Index()
        {
            //var element = _GoogleCaptchaConfiggg.Value;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AppUser appUser)
        {

            var captchaResult = await _capthcaService.VerfiyToken(AppUser.Token);

            if (!captchaResult)
            {
                return View();
            }

            return Content("YOU HAVE SUCCESSFULLY PASSED GOOGLE CAPTCHA");
        }

        [HttpPost]
        public IActionResult Send()
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
