using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.Product.Models;
using UTCGame.Data;

namespace UTCGame.Areas.Product.Controllers
{
    [Area("Product")]
    [Authorize]
    public class ProductModelController : Controller
    {
        private readonly ApplicationDBContext _context;

        public ProductModelController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: Admin/Product
        public async Task<IActionResult> Index(string _search, string _sort)
        {
            var applicationDBContextAdmin = _context.ProductModel.Include(p => p.Game).Include(p => p.ProductType);
            if (!_search.IsNullOrEmpty())
            {
                var ls = applicationDBContextAdmin.Where(x => x.ProductName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = applicationDBContextAdmin.OrderBy(x => x.ProductName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = applicationDBContextAdmin.OrderByDescending(x => x.ProductName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = applicationDBContextAdmin.OrderBy(x => !x.IsProductActive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = applicationDBContextAdmin.OrderBy(x => x.IsProductActive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }

            }
            return View(await applicationDBContextAdmin.ToListAsync());
        }

        // GET: Admin/Product/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.ProductModel
                .Include(p => p.Game)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/ProductModel/Create
        public IActionResult Create()
        {
            ViewData["GameID"] = new SelectList(_context.Game, "GameID", "GameName");
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType.Where(x => x.IsActive), "ProductTypeID", "ProductTypeName");
            return View();
        }

        // POST: Product/ProductModel/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductID,ProductName,ProductPrice,ProductQuantity,ProductReleaseDate,ProductTypeID,GameID,IsProductActive")] ProductModel productModel)
        {
            productModel.ProductID = Guid.NewGuid();
            _context.Add(productModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Product/ProductModel/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel == null)
            {
                return NotFound();
            }
            ViewData["GameID"] = new SelectList(_context.Game, "GameID", "GameName", productModel.GameID);
            ViewData["ProductTypeID"] = new SelectList(_context.ProductType.Where(x => x.IsActive), "ProductTypeID", "ProductTypeName", productModel.ProductTypeID);
            return View(productModel);
        }

        // POST: Product/ProductModel/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ProductID,ProductName,ProductPrice,ProductQuantity,ProductReleaseDate,ProductTypeID,GameID,IsProductActive")] ProductModel productModel)
        {
            if (id != productModel.ProductID)
            {
                return NotFound();
            }

            try
            {
                _context.Update(productModel);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProductModelExists(productModel.ProductID))
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

        // GET: Product/ProductModel/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productModel = await _context.ProductModel
                .Include(p => p.Game)
                .Include(p => p.ProductType)
                .FirstOrDefaultAsync(m => m.ProductID == id);
            if (productModel == null)
            {
                return NotFound();
            }

            return View(productModel);
        }

        // POST: Product/ProductModel/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var productModel = await _context.ProductModel.FindAsync(id);
            if (productModel != null)
            {
                _context.ProductModel.Remove(productModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductModelExists(Guid id)
        {
            return _context.ProductModel.Any(e => e.ProductID == id);
        }
    }
}
