using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.EntityFrameworkCore;
using TempoMapRepository.Data.Context;
using TempoMapRepository.Models.Domain;
using TempoMapRepository.Models.Identity;
using TempoMapRepository.Models.ViewModel;
using TempoMapRepository.Policies;

namespace TempoMapRepository.Controllers
{
    [Authorize]
    public class MapsController : Controller
    {
        private readonly AuthDbContext _context;
        private readonly IAuthorizationService _authorizationService;
        private readonly UserManager<User> _userManager;

        public MapsController(AuthDbContext context, IAuthorizationService authorizationService, UserManager<User> userManager)
        {
            _context = context;
            _authorizationService = authorizationService; 
            _userManager = userManager;
        }

        #region View Endpoints
        // GET: Maps
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _context.Maps
                               .Where(e => e.User.Id == user.Id)
                               .Select(e => new MapDisplayViewModel(e))
                               .ToListAsync());
        }

        // GET: Maps/Details/5
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = await _context.Maps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (map == null)
            {
                return NotFound();
            }

            return View(new MapDisplayViewModel(map));
        }

        // GET: Maps/Create
        public async Task<IActionResult> Create()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(new MapViewModel() { UserId = user?.Id });
        }

        // POST: Maps/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserId,Name,Description,CoverImage,Filename")] MapViewModel mapViewModel)
        {
            if(mapViewModel is null)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                var memoryStream = new MemoryStream();
                mapViewModel?.CoverImage?.CopyTo(memoryStream);
                var map = new Map()
                {
                    Name = mapViewModel?.Name,
                    Description = mapViewModel?.Description,
                    CoverImageFormat = mapViewModel?.CoverImage?.ContentType,
                    CoverImage = memoryStream.ToArray(),
                    User = await _userManager.FindByIdAsync(mapViewModel?.UserId),
                    Filename = mapViewModel?.Filename,
                };
                _context.Add(map);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(new Map());
        }

        // GET: Maps/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var map = await _context.Maps.Include(m => m.User).FirstOrDefaultAsync(m => m.Id == id);//.FindAsync(id);
            if (map == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, map.User?.Id, "IdPolicy");
            if (authorizationResult.Succeeded)
                return View(new MapDisplayViewModel(map));
            else if (User.Identity.IsAuthenticated)
                return new ForbidResult();
            else
                return new ChallengeResult();
        }

        // POST: Maps/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,[Bind("Id,Name,Description,CoverImage,Filename")] MapViewModel mapViewModel)
        {
            if (id != mapViewModel.Id)
            {
                return NotFound();
            }
            var memoryStream = new MemoryStream();
            mapViewModel?.CoverImage?.CopyTo(memoryStream);
            var map = new Map()
            {
                Id = mapViewModel.Id,
                Name = mapViewModel?.Name,
                Description = mapViewModel?.Description,
                CoverImage = memoryStream.ToArray(),
                CoverImageFormat = mapViewModel.CoverImage.ContentType,
                Filename = mapViewModel?.Filename,
            };

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(map);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MapExists(mapViewModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(new MapDisplayViewModel(map));
        }

        // GET: Maps/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var map = await _context.Maps
                .FirstOrDefaultAsync(m => m.Id == id);
            if (map == null)
            {
                return NotFound();
            }

            return View(map);
        }

        // POST: Maps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var map = await _context.Maps.FindAsync(id);
            if (map != null)
            {
                _context.Maps.Remove(map);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        #endregion

        private bool MapExists(int id)
        {
            return _context.Maps.Any(e => e.Id == id);
        }
    }
}
