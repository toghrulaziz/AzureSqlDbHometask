using AzureSQLHometask.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace AzureSQLHometask.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyDbContext _dbContext;

        public HomeController(ILogger<HomeController> logger, MyDbContext dbContext)
        {
            _logger = logger;
            _dbContext = dbContext;
        }


        public ActionResult Index()
        {
            var categories = _dbContext.ProductCategories.ToList();
            ViewData["Categories"] = new SelectList(categories, "ProductCategoryId", "Name");
            return View();
        }

        [HttpPost]
        public ActionResult Index(int selectedCategoryId)
        {
            var products = _dbContext.Products.Where(p => p.ProductCategoryId == selectedCategoryId).ToList();
            ViewData["Products"] = products;
            var categories = _dbContext.ProductCategories.ToList();
            ViewData["Categories"] = new SelectList(categories, "ProductCategoryId", "Name", selectedCategoryId);

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
