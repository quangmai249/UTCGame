using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Employee.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Employee.Controllers
{
    [Area("Employee")]
    [Authorize]
    public class RegionController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RegionController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Region
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.Region.Where(x => x.RegionName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.Region.OrderBy(x => x.RegionName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.Region.OrderByDescending(x => x.RegionName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.Region.OrderBy(x => !x.IsRegionActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.Region.OrderBy(x => x.IsRegionActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }

            }
            return View(await _context.Region.ToListAsync());
        }

        // GET: Admin/Region/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // GET: Admin/Region/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Region/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RegionID,RegionName,IsRegionActive")] Models.Region region)
        {
            if (ModelState.IsValid)
            {
                region.RegionID = Guid.NewGuid();
                _context.Add(region);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(region);
        }

        // GET: Admin/Region/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region.FindAsync(id);
            if (region == null)
            {
                return NotFound();
            }
            return View(region);
        }

        // POST: Admin/Region/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RegionID,RegionName,IsRegionActive")] Models.Region region)
        {
            if (id != region.RegionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(region);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RegionExists(region.RegionID))
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
            return View(region);
        }

        // GET: Admin/Region/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var region = await _context.Region
                .FirstOrDefaultAsync(m => m.RegionID == id);
            if (region == null)
            {
                return NotFound();
            }

            return View(region);
        }

        // POST: Admin/Region/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var region = await _context.Region.FindAsync(id);
            if (region != null)
            {
                _context.Region.Remove(region);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RegionExists(Guid id)
        {
            return _context.Region.Any(e => e.RegionID == id);
        }
    }
}
