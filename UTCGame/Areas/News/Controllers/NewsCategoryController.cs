using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using UTCGame.Areas.News.Models;
using UTCGame.Data;

namespace UTCGame.Areas.News.Controllers
{
    [Area("News")]
    [Authorize]
    public class NewsCategoryController : Controller
    {
        private readonly ApplicationDBContext _context;

        public NewsCategoryController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: News/NewsCategory
        public async Task<IActionResult> Index()
        {
            return View(await _context.NewsCategory.ToListAsync());
        }

        // GET: News/NewsCategory/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory
                .FirstOrDefaultAsync(m => m.NewsCategoryID == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // GET: News/NewsCategory/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: News/NewsCategory/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NewsCategoryID,NewsCategoryName,IsActive")] NewsCategory newsCategory)
        {
            if (ModelState.IsValid)
            {
                newsCategory.NewsCategoryID = Guid.NewGuid();
                _context.Add(newsCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(newsCategory);
        }

        // GET: News/NewsCategory/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory.FindAsync(id);
            if (newsCategory == null)
            {
                return NotFound();
            }
            return View(newsCategory);
        }

        // POST: News/NewsCategory/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("NewsCategoryID,NewsCategoryName,IsActive")] NewsCategory newsCategory)
        {
            if (id != newsCategory.NewsCategoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(newsCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsCategoryExists(newsCategory.NewsCategoryID))
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
            return View(newsCategory);
        }

        // GET: News/NewsCategory/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var newsCategory = await _context.NewsCategory
                .FirstOrDefaultAsync(m => m.NewsCategoryID == id);
            if (newsCategory == null)
            {
                return NotFound();
            }

            return View(newsCategory);
        }

        // POST: News/NewsCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var newsCategory = await _context.NewsCategory.FindAsync(id);
            if (newsCategory != null)
            {
                _context.NewsCategory.Remove(newsCategory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsCategoryExists(Guid id)
        {
            return _context.NewsCategory.Any(e => e.NewsCategoryID == id);
        }
    }
}
