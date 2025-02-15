using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieMate.Data;
using MovieMate.Models;

namespace MovieMate.Controllers
{
    public class WatchingController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchingController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Watching
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Watching.Include(w => w.Media).Include(w => w.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Watching/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watching = await _context.Watching
                .Include(w => w.Media)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (watching == null)
            {
                return NotFound();
            }

            return View(watching);
        }

        // GET: Watching/Create
        public IActionResult Create()
        {
            ViewData["WatchingId"] = new SelectList(_context.Media, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Watching/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,WatchingId")] Watching watching)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watching);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WatchingId"] = new SelectList(_context.Media, "Id", "Id", watching.WatchingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watching.UserId);
            return View(watching);
        }

        // GET: Watching/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watching = await _context.Watching.FindAsync(id);
            if (watching == null)
            {
                return NotFound();
            }
            ViewData["WatchingId"] = new SelectList(_context.Media, "Id", "Id", watching.WatchingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watching.UserId);
            return View(watching);
        }

        // POST: Watching/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,WatchingId")] Watching watching)
        {
            if (id != watching.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watching);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchingExists(watching.UserId))
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
            ViewData["WatchingId"] = new SelectList(_context.Media, "Id", "Id", watching.WatchingId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watching.UserId);
            return View(watching);
        }

        // GET: Watching/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watching = await _context.Watching
                .Include(w => w.Media)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (watching == null)
            {
                return NotFound();
            }

            return View(watching);
        }

        // POST: Watching/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var watching = await _context.Watching.FindAsync(id);
            if (watching != null)
            {
                _context.Watching.Remove(watching);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchingExists(string id)
        {
            return _context.Watching.Any(e => e.UserId == id);
        }
    }
}
