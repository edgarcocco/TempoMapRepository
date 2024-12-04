using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using TempoMapRepository.Data.Context;
using TempoMapRepository.Models.ViewModel;

namespace TempoMapRepository.Controllers
{
    public class HomeController(AuthDbContext _authDbContext, ILogger<HomeController> _logger) : Controller
    {
       

        public async Task<IActionResult> Index()
        {
              return View(await _authDbContext.Maps
                                .Include(e => e.User)
                               .Select(e => new MapDisplayViewModel(e))
                               .ToListAsync());
        }

        public IActionResult ForgotPassword()
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
