using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Game.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Game.Controllers
{
    [Area("Game")]
    [Authorize]
    public class GamePlatformController : Controller
    {
        private readonly ApplicationDBContext _context;

        public GamePlatformController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/GamePlatform
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.GamePlatform.Where(x => x.GamePlatformName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.GamePlatform.OrderBy(x => x.GamePlatformName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.GamePlatform.OrderByDescending(x => x.GamePlatformName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.GamePlatform.OrderBy(x => !x.IsActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.GamePlatform.OrderBy(x => x.IsActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }

            }
            return View(await _context.GamePlatform.ToListAsync());
        }

        // GET: Game/GamePlatform/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlatform = await _context.GamePlatform
                .FirstOrDefaultAsync(m => m.GamePlatformID == id);
            if (gamePlatform == null)
            {
                return NotFound();
            }

            return View(gamePlatform);
        }

        // GET: Game/GamePlatform/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/GamePlatform/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GamePlatformID,GamePlatformName,GamePlatformLink,IsActive")] GamePlatform gamePlatform)
        {
            if (ModelState.IsValid)
            {
                gamePlatform.GamePlatformID = Guid.NewGuid();
                _context.Add(gamePlatform);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamePlatform);
        }

        // GET: Game/GamePlatform/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlatform = await _context.GamePlatform.FindAsync(id);
            if (gamePlatform == null)
            {
                return NotFound();
            }
            return View(gamePlatform);
        }

        // POST: Game/GamePlatform/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GamePlatformID,GamePlatformName,GamePlatformLink,IsActive")] GamePlatform gamePlatform)
        {
            if (id != gamePlatform.GamePlatformID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamePlatform);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamePlatformExists(gamePlatform.GamePlatformID))
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
            return View(gamePlatform);
        }

        // GET: Game/GamePlatform/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamePlatform = await _context.GamePlatform
                .FirstOrDefaultAsync(m => m.GamePlatformID == id);
            if (gamePlatform == null)
            {
                return NotFound();
            }

            return View(gamePlatform);
        }

        // POST: Game/GamePlatform/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gamePlatform = await _context.GamePlatform.FindAsync(id);
            if (gamePlatform != null)
            {
                _context.GamePlatform.Remove(gamePlatform);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamePlatformExists(Guid id)
        {
            return _context.GamePlatform.Any(e => e.GamePlatformID == id);
        }
    }
}
