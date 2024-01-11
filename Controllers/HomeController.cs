using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using System.Diagnostics;
using System.Drawing.Printing;
using TheBlogProject.Data;
using TheBlogProject.Models;
using TheBlogProject.Services.Interfaces;
using TheBlogProject.ViewModels;
using X.PagedList;

namespace TheBlogProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBlogEmailSender _emailSender;
        private readonly IPostService _postService;
        private readonly ICategoryService _categoryService;
        private readonly ApplicationDbContext _context;

        public HomeController(ILogger<HomeController> logger, IBlogEmailSender emailSender, ApplicationDbContext context, IPostService postService, ICategoryService categoryService)
        {
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _postService = postService;
            _categoryService = categoryService;
        }

        #region Index
        public async Task<IActionResult> Index(int? page)
        {
            var posts = await _postService.GetAllPostAsync(5);
            var latestPosts =  _postService.GetLatestPostsAsync().Take(3).ToList();
            var categories = await _categoryService.GetCategoriesAsync(); // need improving
            var mostPopularPostsBasedOnComments = await _postService.GetMostPopularPostsBasedOnComments();

            var HomeViewModel = new HomePageViewModel
            {
                Posts = posts,
                LatestPosts = latestPosts,
                Categories = categories.Take(6).ToList(),
                PopularPosts = mostPopularPostsBasedOnComments
            };

            return View(HomeViewModel);
        } 
        #endregion

        #region About
        public IActionResult About()
        {
            return View();
        }
        #endregion

        #region Contact
        public IActionResult Contact()
        {
            return View();
        }
        #endregion

        #region Post -- Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMe model)
        {
            // This is where we will be emailing
            model.Message = $"{model.Message} <hr/>";
            await _emailSender.SendContactEmailAsync(model.Email, model.Name, model.Subject, model.Message);

            return RedirectToAction("Index");
        }
        #endregion


        #region Privacy
        public IActionResult Privacy()
        {
            return View();
        } 
        #endregion

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}