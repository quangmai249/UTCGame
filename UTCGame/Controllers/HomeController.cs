using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Security.Policy;
using UTCGame.Data;
using UTCGame.Models;
using X.PagedList;

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

        public IActionResult Games(int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 2;
            var games = _context.Game.Where(x => x.IsGameActive).ToPagedList(page, pageSize);
            return View(games);
        }

        public IActionResult Products(string _types, string _games, int page = 1)
        {
            var productsType = _context.ProductType.Where(x => x.IsActive).ToList();
            ViewBag.ProductTypes = productsType;

            var products = _context.ProductModel.Where(x => x.IsProductActive).ToList();
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            if (products != null && _types != null)
            {
                var ls = products.Where(x => x.ProductTypeID.Equals(Guid.Parse(_types))).ToPagedList(page, pageSize);
                return View(ls);
            }
            else
            {
                return View(products.ToPagedList(page, pageSize));
            }

        }

        public IActionResult NewsEvents(string? news_category, int page = 1)
        {
            var category = _context.NewsCategory.Where(x => x.IsActive).ToList();
            ViewBag.NewsCategory = category;

            var news = _context.NewEvent.Where(x => x.IsActive).ToList();
            var employee = _context.EmployeeModel.Where(x => x.IsEmployeeActive).ToList();

            page = page < 1 ? 1 : page;
            int pageSize = 3;
            if (news_category != null && news != null)
            {
                var ls = news.Where(x => x.NewsCategoryID.Equals(Guid.Parse(news_category)));
                return View(ls.ToPagedList(page, pageSize));
            }
            else
            {
                return View(news.ToPagedList(page, pageSize));
            }
        }

        public IActionResult Recruits(string? _region, int page = 1)
        {
            var region = _context.Region.Where(x => x.IsRegionActive).ToList();
            ViewBag.Regions = region;

            var recruits = _context.RecruitModel.Where(x => x.IsActive).ToList();
            page = page < 1 ? 1 : page;
            int pageSize = 3;
            if (_region != null && recruits != null)
            {
                var ls = recruits.Where(x => x.RegionID.Equals(Guid.Parse(_region)));
                return View(ls.ToPagedList(page, pageSize));
            }
            else
            {
                return View(recruits.ToPagedList(page, pageSize));
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
                ViewBag.Contact = _Contact.ToList();
                return View();
            }
            else
            {
                var _FAQ = _context.FAQ.Where(x => x.IsActive);
                ViewBag.FAQ = _FAQ.ToList();
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
