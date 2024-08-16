using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Employee.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class EmployeeModelsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public EmployeeModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Employee/EmployeeModels
        public async Task<IActionResult> Index(string _search, string _sort)
        {

            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role).Where(x => x.EmployeeEmail.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role).OrderBy(x => x.EmployeeEmail).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role).OrderByDescending(x => x.EmployeeEmail).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role).OrderBy(x => !x.IsEmployeeActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role).OrderBy(x => x.IsEmployeeActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }
            }
            var applicationDBContext = _context.EmployeeModel.Include(e => e.Region).Include(e => e.Role);
            return View(await applicationDBContext.ToListAsync());
        }

        // GET: Employee/EmployeeModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .Include(e => e.Region)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // GET: Employee/EmployeeModels/Create
        public IActionResult Create()
        {
            ViewData["RegionID"] = new SelectList(_context.Region.Where(x => x.IsRegionActive), "RegionID", "RegionName");
            ViewData["RoleID"] = new SelectList(_context.Role.Where(x => x.IsRoleActive), "RoleID", "RoleName");
            return View();
        }

        // POST: Employee/EmployeeModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeID,EmployeeEmail,EmployeePassword,RegionID,RoleID,IsEmployeeActive")] EmployeeModel employeeModel)
        {
            employeeModel.EmployeeID = Guid.NewGuid();
            _context.Add(employeeModel);
            await _context.SaveChangesAsync();
            ModelState.Clear();

            return RedirectToAction(nameof(Index));
        }

        // GET: Employee/EmployeeModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            if (employeeModel == null)
            {
                return NotFound();
            }
            ViewData["RegionID"] = new SelectList(_context.Region.Where(x => x.IsRegionActive), "RegionID", "RegionName", employeeModel.RegionID);
            ViewData["RoleID"] = new SelectList(_context.Role.Where(x => x.IsRoleActive), "RoleID", "RoleName", employeeModel.RoleID);
            return View(employeeModel);
        }

        // POST: Employee/EmployeeModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeID,EmployeeEmail,EmployeePassword,RegionID,RoleID,IsEmployeeActive")] EmployeeModel employeeModel)
        {
            if (id != employeeModel.EmployeeID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(employeeModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EmployeeModelExists(employeeModel.EmployeeID))
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

        // GET: Employee/EmployeeModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employeeModel = await _context.EmployeeModel
                .Include(e => e.Region)
                .Include(e => e.Role)
                .FirstOrDefaultAsync(m => m.EmployeeID == id);
            if (employeeModel == null)
            {
                return NotFound();
            }

            return View(employeeModel);
        }

        // POST: Employee/EmployeeModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employeeModel = await _context.EmployeeModel.FindAsync(id);
            if (employeeModel != null)
            {
                _context.EmployeeModel.Remove(employeeModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeModelExists(Guid id)
        {
            return _context.EmployeeModel.Any(e => e.EmployeeID == id);
        }
    }
}
