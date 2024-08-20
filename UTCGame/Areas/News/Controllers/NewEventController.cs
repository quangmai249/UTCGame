using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.News.Models;
using UTCGame.Data;
using X.PagedList;

namespace UTCGame.Areas.News.Controllers
{
    [Area("News")]
    [Authorize]
    public class NewEventController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NewEventController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: News/NewEvent
        public async Task<IActionResult> Index(DateTime _date, string _search, string _sort, string _category, int page = 1)
        {
            var applicationDBContext = _context.NewEvent.Include(n => n.FolderMediaModel).Include(n => n.NewsCategory);

            var Category = _context.NewsCategory.Where(x => x.IsActive).ToList();
            ViewBag.Category = Category;

            page = page < 1 ? 1 : page;
            int pageSize = 3;
            if (_date != DateTime.MinValue)
            {
                var ls = applicationDBContext.Where(x => x.NewEventDateTime.Contains(_date.ToShortDateString())).OrderByDescending(x => x.NewEventDateTime).ToPagedList(page, pageSize);
                return View(ls);
            }
            if (!_category.IsNullOrEmpty())
            {
                var ls = applicationDBContext.Where(x => x.NewsCategory.NewsCategoryName.Contains(_category)).ToPagedList(page, pageSize);
                return View(ls);
            }
            if (!_search.IsNullOrEmpty())
            {
                var ls = applicationDBContext.Where(x => x.NewEventTitle.Contains(_search)).ToPagedList(page, pageSize);
                return View(ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = applicationDBContext.OrderBy(x => x.NewEventTitle).ToPagedList(page, pageSize);
                        return View(az);
                    case "za":
                        var za = applicationDBContext.OrderByDescending(x => x.NewEventTitle).ToPagedList(page, pageSize);
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

            return View(applicationDBContext.OrderByDescending(x => x.NewEventDateTime).ToPagedList(page, pageSize));
        }

        // GET: News/NewEvent/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newEvent = await _context.NewEvent
                .Include(n => n.FolderMediaModel)
                .Include(n => n.NewsCategory)
                .FirstOrDefaultAsync(m => m.NewEventID == id);
            if (newEvent == null)
            {
                return NotFound();
            }

            return View(newEvent);
        }

        // GET: News/NewEvent/Create
        public IActionResult Create()
        {
            ViewData["FolderMediaID"] = new SelectList(_context.FolderMediaModel.Where(x => x.IsActive), "FolderMediaID", "FolderMediaName");
            ViewData["NewsCategoryID"] = new SelectList(_context.NewsCategory.Where(x => x.IsActive), "NewsCategoryID", "NewsCategoryName");
            return View();
        }

        // POST: News/NewEvent/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewEventID,NewEventTitle,NewEventDetail,NewEventDateTime,NewsCategoryID,FolderMediaID,IsActive")] NewEvent newEvent)
        {
            newEvent.NewEventID = Guid.NewGuid();
            _context.Add(newEvent);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: News/NewEvent/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newEvent = await _context.NewEvent.FindAsync(id);
            if (newEvent == null)
            {
                return NotFound();
            }
            ViewData["FolderMediaID"] = new SelectList(_context.FolderMediaModel.Where(x => x.IsActive), "FolderMediaID", "FolderMediaName", newEvent.FolderMediaID);
            ViewData["NewsCategoryID"] = new SelectList(_context.NewsCategory.Where(x => x.IsActive), "NewsCategoryID", "NewsCategoryName", newEvent.NewsCategoryID);
            return View(newEvent);
        }

        // POST: News/NewEvent/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NewEventID,NewEventTitle,NewEventDetail,NewEventDateTime,NewsCategoryID,FolderMediaID,IsActive")] NewEvent newEvent)
        {
            if (id != newEvent.NewEventID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(newEvent);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewEventExists(newEvent.NewEventID))
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

        // GET: News/NewEvent/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newEvent = await _context.NewEvent
                .Include(n => n.FolderMediaModel)
                .Include(n => n.NewsCategory)
                .FirstOrDefaultAsync(m => m.NewEventID == id);
            if (newEvent == null)
            {
                return NotFound();
            }

            return View(newEvent);
        }

        // POST: News/NewEvent/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var newEvent = await _context.NewEvent.FindAsync(id);
            if (newEvent != null)
            {
                _context.NewEvent.Remove(newEvent);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewEventExists(Guid id)
        {
            return _context.NewEvent.Any(e => e.NewEventID == id);
        }
    }
}
