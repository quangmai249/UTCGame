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
    public class RoleController : Controller
    {
        private readonly ApplicationDBContext _context;

        public RoleController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Employee/Role
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.Role.Where(x => x.RoleName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.Role.OrderBy(x => x.RoleName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.Role.OrderByDescending(x => x.RoleName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.Role.OrderBy(x => !x.IsRoleActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.Role.OrderBy(x => x.IsRoleActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }

            }
            return View(await _context.Role.ToListAsync());
        }

        // GET: Employee/Role/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Role
                .FirstOrDefaultAsync(m => m.RoleID == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // GET: Employee/Role/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Role/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RoleID,RoleName,IsRoleActive")] Role role)
        {
            if (ModelState.IsValid)
            {
                role.RoleID = Guid.NewGuid();
                _context.Add(role);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(role);
        }

        // GET: Employee/Role/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Role.FindAsync(id);
            if (role == null)
            {
                return NotFound();
            }
            return View(role);
        }

        // POST: Employee/Role/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("RoleID,RoleName,IsRoleActive")] Role role)
        {
            if (id != role.RoleID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(role);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoleExists(role.RoleID))
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
            return View(role);
        }

        // GET: Employee/Role/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var role = await _context.Role
                .FirstOrDefaultAsync(m => m.RoleID == id);
            if (role == null)
            {
                return NotFound();
            }

            return View(role);
        }

        // POST: Employee/Role/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var role = await _context.Role.FindAsync(id);
            if (role != null)
            {
                _context.Role.Remove(role);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoleExists(Guid id)
        {
            return _context.Role.Any(e => e.RoleID == id);
        }
    }
}
