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
    public class PlanToWatchController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PlanToWatchController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PlanToWatch
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.PlanToWatch.Include(p => p.Media).Include(p => p.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PlanToWatch/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planToWatch = await _context.PlanToWatch
                .Include(p => p.Media)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (planToWatch == null)
            {
                return NotFound();
            }

            return View(planToWatch);
        }

        // GET: PlanToWatch/Create
        public IActionResult Create()
        {
            ViewData["PlanId"] = new SelectList(_context.Media, "Id", "Id");
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id");
            return View();
        }

        // POST: PlanToWatch/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,PlanId")] PlanToWatch planToWatch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(planToWatch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PlanId"] = new SelectList(_context.Media, "Id", "Id", planToWatch.PlanId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", planToWatch.UserId);
            return View(planToWatch);
        }

        // GET: PlanToWatch/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planToWatch = await _context.PlanToWatch.FindAsync(id);
            if (planToWatch == null)
            {
                return NotFound();
            }
            ViewData["PlanId"] = new SelectList(_context.Media, "Id", "Id", planToWatch.PlanId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", planToWatch.UserId);
            return View(planToWatch);
        }

        // POST: PlanToWatch/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("UserId,PlanId")] PlanToWatch planToWatch)
        {
            if (id != planToWatch.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(planToWatch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PlanToWatchExists(planToWatch.UserId))
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
            ViewData["PlanId"] = new SelectList(_context.Media, "Id", "Id", planToWatch.PlanId);
            ViewData["UserId"] = new SelectList(_context.Set<User>(), "Id", "Id", planToWatch.UserId);
            return View(planToWatch);
        }

        // GET: PlanToWatch/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var planToWatch = await _context.PlanToWatch
                .Include(p => p.Media)
                .Include(p => p.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (planToWatch == null)
            {
                return NotFound();
            }

            return View(planToWatch);
        }

        // POST: PlanToWatch/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var planToWatch = await _context.PlanToWatch.FindAsync(id);
            if (planToWatch != null)
            {
                _context.PlanToWatch.Remove(planToWatch);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PlanToWatchExists(string id)
        {
            return _context.PlanToWatch.Any(e => e.UserId == id);
        }
    }
}
