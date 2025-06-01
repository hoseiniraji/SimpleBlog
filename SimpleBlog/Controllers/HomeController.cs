using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using SimpleBlog.Data;
using SimpleBlog.Framework;
using SimpleBlog.Models;

namespace SimpleBlog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UnitOfWork _unitOfWork;
        //private readonly ApplicationDbContext _context;
        public HomeController(ILogger<HomeController> logger, UnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="p">current page</param>
        /// <param name="c">page capacity</param>
        /// <param name="q">search term</param>
        /// <returns></returns>
        public async Task<IActionResult> Index(int p = 1, int c = 10 , string q = "")
        {
            PageList<BlogPost> posts = await _unitOfWork.BlogPosts.GetPagedAsync(p , c, q);
            return View(posts);
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
