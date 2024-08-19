using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using UTCGame.Data;
using UTCGame.Models;

namespace UTCGame.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDBContext _context;

        public HomeController(ILogger<HomeController> logger, ApplicationDBContext applicationDB)
        {
            _logger = logger;
            _context = applicationDB;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Games()
        {
            var games = _context.Game.Where(x => x.IsGameActive).ToList();
            return View(games);
        }

        public IActionResult Products()
        {
            var products = _context.ProductModel.Where(x => x.IsProductActive).ToList();
            return View(products);
        }

        public IActionResult NewsEvents(string? news_category)
        {
            var category = _context.NewsCategory.Where(x => x.IsActive).ToList();
            ViewBag.NewsCategory = category;

            var news = _context.NewEvent.Where(x => x.IsActive).ToList();
            var employee = _context.EmployeeModel.Where(x => x.IsEmployeeActive).ToList();

            if (news_category != null && news != null)
            {
                var ls = news.Where(x => x.NewsCategoryID.Equals(Guid.Parse(news_category))).ToList();
                return View(ls);
            }
            else
            {
                return View(news);
            }
        }

        public IActionResult Recruits(string? _region)
        {
            var region = _context.Region.Where(x => x.IsRegionActive).ToList();
            ViewBag.Regions = region;

            var recruits = _context.RecruitModel.Where(x => x.IsActive).ToList();
            if (_region != null && recruits != null)
            {
                var ls = recruits.Where(x => x.RegionID.Equals(Guid.Parse(_region))).ToList();
                return View(ls);
            }
            else
            {
                return View(recruits);
            }
        }

        public IActionResult Privacy()
        {
            var media = _context.FolderMediaModel.Where(x => x.IsActive && x.FolderMediaID.Equals(Guid.Parse("368BF5FE-F405-45C3-9EE0-809C0E7A0526")));
            if (!media.IsNullOrEmpty())
            {
                string path = "wwwroot/media/Privacy/";
                var file = Directory.GetFiles(path, "*.pdf", SearchOption.TopDirectoryOnly);
                if (file != null)
                {
                    ViewBag.Path = file.FirstOrDefault();
                }
                else
                {
                    ViewBag.Path = "We are updating the privacy and policy!";
                }
            }
            return View();
        }

        public IActionResult Support(string? _support)
        {
            if (_support != null && _support.Equals("Contact"))
            {
                var _Contact = _context.EmployeeModel.Where(x => x.IsEmployeeActive && x.RoleID.Equals(Guid.Parse("3BDC247F-9F53-4808-8298-AA9E975DAE75"))).ToList();
                ViewBag.Contact = _Contact;
                return View();
            }
            else
            {
                var _FAQ = _context.FAQ.Where(x => x.IsActive).ToList();
                ViewBag.FAQ = _FAQ;
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
