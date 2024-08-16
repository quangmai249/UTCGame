using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Game.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Game.Controllers
{
    [Area("Game")]
    public class GameTypeController : Controller
    {
        private readonly ApplicationDBContext _context;

        public GameTypeController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/GameType
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.GameType.Where(x => x.GameTypeName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.GameType.OrderBy(x => x.GameTypeName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.GameType.OrderByDescending(x => x.GameTypeName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.GameType.OrderBy(x => !x.IsActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.GameType.OrderBy(x => x.IsActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }

            }
            return View(await _context.GameType.ToListAsync());
        }

        // GET: Game/GameType/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameType
                .FirstOrDefaultAsync(m => m.GameTypeID == id);
            if (gameType == null)
            {
                return NotFound();
            }

            return View(gameType);
        }

        // GET: Game/GameType/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Game/GameType/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("GameTypeID,GameTypeName,IsActive")] GameType gameType)
        {
            if (ModelState.IsValid)
            {
                gameType.GameTypeID = Guid.NewGuid();
                _context.Add(gameType);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gameType);
        }

        // GET: Game/GameType/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameType.FindAsync(id);
            if (gameType == null)
            {
                return NotFound();
            }
            return View(gameType);
        }

        // POST: Game/GameType/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("GameTypeID,GameTypeName,IsActive")] GameType gameType)
        {
            if (id != gameType.GameTypeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gameType);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GameTypeExists(gameType.GameTypeID))
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
            return View(gameType);
        }

        // GET: Game/GameType/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameType = await _context.GameType
                .FirstOrDefaultAsync(m => m.GameTypeID == id);
            if (gameType == null)
            {
                return NotFound();
            }

            return View(gameType);
        }

        // POST: Game/GameType/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameType = await _context.GameType.FindAsync(id);
            if (gameType != null)
            {
                _context.GameType.Remove(gameType);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameTypeExists(Guid id)
        {
            return _context.GameType.Any(e => e.GameTypeID == id);
        }
    }
}
