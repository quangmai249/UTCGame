using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Support.Models;
using UTCGame.Data;
using X.PagedList;

namespace UTCGame.Areas.Support.Controllers
{
    [Area("Support")]
    [Authorize]
    public class FAQController : Controller
    {
        private readonly ApplicationDBContext _context;

        public FAQController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Support/FAQ
        public async Task<IActionResult> Index(string _search, string _sort, int page = 1)
        {
            page = page < 1 ? 1 : page;
            int pageSize = 10;
            var applicationDBContext = _context.FAQ;
            if (!_search.IsNullOrEmpty())
            {
                var ls = applicationDBContext.Where(x => x.FAQ_Title.Contains(_search)).ToPagedList(page, pageSize);
                return View(ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = applicationDBContext.OrderBy(x => x.FAQ_Title).ToPagedList(page, pageSize);
                        return View(az);
                    case "za":
                        var za = applicationDBContext.OrderByDescending(x => x.FAQ_Title).ToPagedList(page, pageSize);
                        return View(za);
                    case "active":
                        var active = applicationDBContext.OrderBy(x => !x.IsActive).ToPagedList(page, pageSize);
                        return View(active);
                    case "!active":
                        var not_active = applicationDBContext.OrderBy(x => x.IsActive).ToPagedList(page, pageSize);
                        return View(not_active);
                    default:
                        break;
                }
            }
            return View(_context.FAQ.OrderBy(x => x.FAQ_Title).ToPagedList(page, pageSize));
        }

        // GET: Support/FAQ/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.FAQ_ID == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // GET: Support/FAQ/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Support/FAQ/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FAQ_ID,FAQ_Title,FAQ_Detail,IsActive")] FAQ fAQ)
        {
            if (ModelState.IsValid)
            {
                fAQ.FAQ_ID = Guid.NewGuid();
                _context.Add(fAQ);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fAQ);
        }

        // GET: Support/FAQ/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ.FindAsync(id);
            if (fAQ == null)
            {
                return NotFound();
            }
            return View(fAQ);
        }

        // POST: Support/FAQ/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("FAQ_ID,FAQ_Title,FAQ_Detail,IsActive")] FAQ fAQ)
        {
            if (id != fAQ.FAQ_ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fAQ);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FAQExists(fAQ.FAQ_ID))
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
            return View(fAQ);
        }

        // GET: Support/FAQ/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fAQ = await _context.FAQ
                .FirstOrDefaultAsync(m => m.FAQ_ID == id);
            if (fAQ == null)
            {
                return NotFound();
            }

            return View(fAQ);
        }

        // POST: Support/FAQ/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var fAQ = await _context.FAQ.FindAsync(id);
            if (fAQ != null)
            {
                _context.FAQ.Remove(fAQ);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FAQExists(Guid id)
        {
            return _context.FAQ.Any(e => e.FAQ_ID == id);
        }
    }
}
