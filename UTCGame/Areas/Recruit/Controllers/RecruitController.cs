using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Recruit.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Recruit.Controllers
{
    [Area("Recruit")]
    public class RecruitController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RecruitController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Recruit/Recruit
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            var applicationDBContext = _context.RecruitModel.Include(r => r.Region);
            if (!_search.IsNullOrEmpty())
            {
                var ls = applicationDBContext.Where(x => x.RecruitName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = applicationDBContext.OrderBy(x => x.RecruitName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = applicationDBContext.OrderByDescending(x => x.RecruitName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = applicationDBContext.OrderBy(x => !x.IsActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = applicationDBContext.OrderBy(x => x.IsActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }
            }
            return View(await applicationDBContext.OrderBy(x => x.Region.RegionName).ToListAsync());
        }

        // GET: Recruit/Recruit/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel
                .Include(r => r.Region)
                .FirstOrDefaultAsync(m => m.RecruitID == id);
            if (recruitModel == null)
            {
                return NotFound();
            }

            return View(recruitModel);
        }

        // GET: Recruit/Recruit/Create
        public IActionResult Create()
        {
            ViewData["RegionID"] = new SelectList(_context.Region.Where(x => x.IsRegionActive), "RegionID", "RegionName");
            return View();
        }

        // POST: Recruit/Recruit/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RecruitID,RecruitName,RegionID,IsActive")] RecruitModel recruitModel)
        {
            recruitModel.RecruitID = Guid.NewGuid();
            _context.Add(recruitModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Recruit/Recruit/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel.FindAsync(id);
            if (recruitModel == null)
            {
                return NotFound();
            }
            ViewData["RegionID"] = new SelectList(_context.Region.Where(x => x.IsRegionActive), "RegionID", "RegionName", recruitModel.RegionID);
            return View(recruitModel);
        }

        // POST: Recruit/Recruit/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RecruitID,RecruitName,RegionID,IsActive")] RecruitModel recruitModel)
        {
            if (id != recruitModel.RecruitID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(recruitModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecruitModelExists(recruitModel.RecruitID))
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

        // GET: Recruit/Recruit/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var recruitModel = await _context.RecruitModel
                .Include(r => r.Region)
                .FirstOrDefaultAsync(m => m.RecruitID == id);
            if (recruitModel == null)
            {
                return NotFound();
            }

            return View(recruitModel);
        }

        // POST: Recruit/Recruit/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var recruitModel = await _context.RecruitModel.FindAsync(id);
            if (recruitModel != null)
            {
                _context.RecruitModel.Remove(recruitModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RecruitModelExists(Guid id)
        {
            return _context.RecruitModel.Any(e => e.RecruitID == id);
        }
    }
}
