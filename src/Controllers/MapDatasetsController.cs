using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TempoMapRepository.Data.Context;
using TempoMapRepository.Models.Domain;

namespace TempoMapRepository.Controllers
{
    public class MapDatasetsController : Controller
    {
        private readonly AuthDbContext _context;

        public MapDatasetsController(AuthDbContext context)
        {
            _context = context;
        }

        // GET: MapDatasets
        public async Task<IActionResult> Index()
        {
            return View(await _context.MapDataset.ToListAsync());
        }

        // GET: MapDatasets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapDataset = await _context.MapDataset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mapDataset == null)
            {
                return NotFound();
            }

            return View(mapDataset);
        }

        // GET: MapDatasets/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MapDatasets/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tempo,Data")] MapDataset mapDataset)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mapDataset);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mapDataset);
        }

        // GET: MapDatasets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapDataset = await _context.MapDataset.FindAsync(id);
            if (mapDataset == null)
            {
                return NotFound();
            }
            return View(mapDataset);
        }

        // POST: MapDatasets/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tempo,Data")] MapDataset mapDataset)
        {
            if (id != mapDataset.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mapDataset);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MapDatasetExists(mapDataset.Id))
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
            return View(mapDataset);
        }

        // GET: MapDatasets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mapDataset = await _context.MapDataset
                .FirstOrDefaultAsync(m => m.Id == id);
            if (mapDataset == null)
            {
                return NotFound();
            }

            return View(mapDataset);
        }

        // POST: MapDatasets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mapDataset = await _context.MapDataset.FindAsync(id);
            if (mapDataset != null)
            {
                _context.MapDataset.Remove(mapDataset);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MapDatasetExists(int id)
        {
            return _context.MapDataset.Any(e => e.Id == id);
        }
    }
}
