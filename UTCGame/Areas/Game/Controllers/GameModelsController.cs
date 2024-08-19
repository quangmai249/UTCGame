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
    public class GameModelsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public GameModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Game
        public async Task<IActionResult> Index(string _search, string _sort, string _platform, string _type)
        {
            var applicationDBContextAdmin = _context.Game.Include(g => g.Employee).Include(g => g.FolderMedia);

            var game_type = _context.GameType.ToList();
            var game_platform = _context.GamePlatform.ToList();
            ViewBag.GameType = game_type;
            ViewBag.GamePlatform = game_platform;

            if (!_search.IsNullOrEmpty())
            {
                var ls = applicationDBContextAdmin.Where(x => x.GameName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_platform.IsNullOrEmpty())
            {
                var ls = applicationDBContextAdmin.Where(x => x.GamePlatform != null && x.GamePlatform.Contains(_platform)).ToListAsync();
                return View(await ls);
            }
            if (!_type.IsNullOrEmpty())
            {
                var ls = applicationDBContextAdmin.Where(x => x.GameType != null && x.GameType.Contains(_type)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = applicationDBContextAdmin.OrderBy(x => x.GameName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = applicationDBContextAdmin.OrderByDescending(x => x.GameName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = applicationDBContextAdmin.OrderBy(x => !x.IsGameActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = applicationDBContextAdmin.OrderBy(x => x.IsGameActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }
            }
            return View(await applicationDBContextAdmin.ToListAsync());
        }

        // GET: Game/GameModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.Game
                .Include(g => g.Employee)
                .Include(g => g.FolderMedia)
                .FirstOrDefaultAsync(m => m.GameID == id);
            if (gameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // GET: Game/GameModels/Create
        public IActionResult Create()
        {
            ViewData["EmployeeID"] = new SelectList(_context.EmployeeModel, "EmployeeID", "EmployeeEmail");
            ViewData["FolderMediaID"] = new SelectList(_context.FolderMediaModel, "FolderMediaID", "FolderMediaName");

            var game_type = _context.GameType.Where(x => x.IsActive).ToList();
            var game_platform = _context.GamePlatform.Where(x => x.IsActive).ToList();
            ViewBag.GameType = game_type;
            ViewBag.GamePlatform = game_platform;

            return View();
        }

        // POST: Game/GameModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<string> game_type, List<string> game_platform, [Bind("GameID,GameName,GamePrice,GameType,GamePlatform,GameReleaseDate,EmployeeID,FolderMediaID,IsGameActive")] GameModel gameModel)
        {
            gameModel.GameID = Guid.NewGuid();
            gameModel.GamePlatform = String.Join(", ", game_platform);
            gameModel.GameType = String.Join(", ", game_type);
            _context.Add(gameModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Game/GameModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.Game.FindAsync(id);
            if (gameModel == null)
            {
                return NotFound();
            }
            ViewData["EmployeeID"] = new SelectList(_context.EmployeeModel, "EmployeeID", "EmployeeEmail", gameModel.EmployeeID);
            ViewData["FolderMediaID"] = new SelectList(_context.FolderMediaModel, "FolderMediaID", "FolderMediaName", gameModel.FolderMediaID);

            var game_type = _context.GameType.Where(x => x.IsActive).ToList();
            var game_platform = _context.GamePlatform.Where(x => x.IsActive).ToList();
            ViewBag.GameType = game_type;
            ViewBag.GamePlatform = game_platform;

            return View(gameModel);
        }

        // POST: Game/GameModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, List<string> game_type, List<string> game_platform, [Bind("GameID,GameName,GamePrice,GameType,GamePlatform,GameReleaseDate,EmployeeID,FolderMediaID,IsGameActive")] GameModel gameModel)
        {
            if (id != gameModel.GameID)
            {
                return NotFound();
            }
            try
            {
                gameModel.GamePlatform = String.Join(", ", game_platform);
                gameModel.GameType = String.Join(", ", game_type);
                _context.Update(gameModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GameModelExists(gameModel.GameID))
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

        // GET: Game/GameModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gameModel = await _context.Game
                .Include(g => g.Employee)
                .Include(g => g.FolderMedia)
                .FirstOrDefaultAsync(m => m.GameID == id);
            if (gameModel == null)
            {
                return NotFound();
            }

            return View(gameModel);
        }

        // POST: Game/GameModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var gameModel = await _context.Game.FindAsync(id);
            if (gameModel != null)
            {
                _context.Game.Remove(gameModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GameModelExists(Guid id)
        {
            return _context.Game.Any(e => e.GameID == id);
        }
    }
}
