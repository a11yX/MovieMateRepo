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
    public class WatchedController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchedController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Watched
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Watched.Include(w => w.Media).Include(w => w.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Watched/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watched = await _context.Watched
                .Include(w => w.Media)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (watched == null)
            {
                return NotFound();
            }

            return View(watched);
        }

        // GET: Watched/Create
        public IActionResult Create()
        {
            ViewData["WatchedId"] = new SelectList(_context.Media, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: Watched/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,WatchedId")] Watched watched)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watched);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["WatchedId"] = new SelectList(_context.Media, "Id", "Id", watched.WatchedId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watched.UserId);
            return View(watched);
        }

        // GET: Watched/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watched = await _context.Watched.FindAsync(id);
            if (watched == null)
            {
                return NotFound();
            }
            ViewData["WatchedId"] = new SelectList(_context.Media, "Id", "Id", watched.WatchedId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watched.UserId);
            return View(watched);
        }

        // POST: Watched/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,WatchedId")] Watched watched)
        {
            if (id != watched.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watched);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchedExists(watched.UserId))
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
            ViewData["WatchedId"] = new SelectList(_context.Media, "Id", "Id", watched.WatchedId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", watched.UserId);
            return View(watched);
        }

        // GET: Watched/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var watched = await _context.Watched
                .Include(w => w.Media)
                .Include(w => w.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (watched == null)
            {
                return NotFound();
            }

            return View(watched);
        }

        // POST: Watched/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var watched = await _context.Watched.FindAsync(id);
            if (watched != null)
            {
                _context.Watched.Remove(watched);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchedExists(string id)
        {
            return _context.Watched.Any(e => e.UserId == id);
        }
    }
}
