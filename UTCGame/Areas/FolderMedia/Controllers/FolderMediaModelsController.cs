using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using UTCGame.Areas.FolderMedia.Models;
using UTCGame.Data;

namespace UTCGame.Areas.FolderMedia.Controllers
{
    [Area("FolderMedia")]
    public class FolderMediaModelsController : Controller
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _webHost;

        public FolderMediaModelsController(ApplicationDBContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _webHost = webHost;
        }

        // GET: FolderMedia/FolderMediaModels
        public async Task<IActionResult> Index(string _search, string _sort, List<IFormFile> formFiles, string folder)
        {
            if (!_search.IsNullOrEmpty())
            {
                var ls = _context.FolderMediaModel.Where(x => x.FolderMediaName.Contains(_search)).ToListAsync();
                return View(await ls);
            }
            if (!_sort.IsNullOrEmpty())
            {
                switch (_sort)
                {
                    case "az":
                        var az = _context.FolderMediaModel.OrderBy(x => x.FolderMediaName).ToListAsync();
                        return View(await az);
                    case "za":
                        var za = _context.FolderMediaModel.OrderByDescending(x => x.FolderMediaName).ToListAsync();
                        return View(await za);
                    case "active":
                        var active = _context.FolderMediaModel.OrderBy(x => !x.IsAvtive).ToListAsync();
                        return View(await active);
                    case "!active":
                        var not_active = _context.FolderMediaModel.OrderBy(x => x.IsAvtive).ToListAsync();
                        return View(await not_active);
                    default:
                        break;
                }
            }
            if (formFiles != null && folder != null)
            {
                string path = Path.Combine($"{_webHost.WebRootPath}/media", folder);
                if (ModelState.IsValid && Directory.Exists(path))
                {
                    int _c = 0;
                    foreach (var item in formFiles)
                    {
                        string fileName = Path.Combine(item.FileName);
                        string filePath = Path.Combine(path, fileName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        item.CopyTo(fileStream);
                        fileStream.Close();
                        _c++;
                    }
                    await _context.SaveChangesAsync();
                    ViewBag.CheckFile = $"{_c} files created successfully!";
                }
            }
            return View(await _context.FolderMediaModel.ToListAsync());
        }

        [HttpPost]
        public async Task<IActionResult> Index(List<IFormFile> formFiles, string folder)
        {
            if (formFiles != null && folder != null)
            {
                string path = Path.Combine($"{_webHost.WebRootPath}/media", folder);
                if (ModelState.IsValid && Directory.Exists(path))
                {
                    int _c = 0;
                    foreach (var item in formFiles)
                    {
                        string fileName = Path.Combine(item.FileName);
                        string filePath = Path.Combine(path, fileName);
                        FileStream fileStream = new FileStream(filePath, FileMode.Create);
                        item.CopyTo(fileStream);
                        fileStream.Close();
                        _c++;
                    }
                    await _context.SaveChangesAsync();
                    ViewBag.CheckFile = $"{_c} files created successfully!";
                }
            }
            return View(await _context.FolderMediaModel.ToListAsync());
        }

        // GET: FolderMedia/FolderMediaModels/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folderMediaModel = await _context.FolderMediaModel
                .FirstOrDefaultAsync(m => m.FolderMediaID == id);
            if (folderMediaModel == null)
            {
                return NotFound();
            }

            return View(folderMediaModel);
        }

        // GET: FolderMedia/FolderMediaModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FolderMedia/FolderMediaModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(List<IFormFile> formFiles, [Bind("FolderMediaID,FolderMediaName,IsAvtive")] FolderMediaModel folderMediaModel)
        {
            string path = Path.Combine($"{_webHost.WebRootPath}/media", folderMediaModel.FolderMediaName);
            if (ModelState.IsValid && !Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                int _c = 0;
                foreach (var item in formFiles)
                {
                    string fileName = Path.Combine(item.FileName);
                    string filePath = Path.Combine(path, fileName);
                    FileStream fileStream = new FileStream(filePath, FileMode.Create);
                    item.CopyTo(fileStream);
                    fileStream.Close();
                    _c++;
                }
                folderMediaModel.FolderMediaID = Guid.NewGuid();
                _context.Add(folderMediaModel);
                await _context.SaveChangesAsync();

                ViewBag.CheckFolder = "Folder created successfully!";
                ViewBag.CheckFile = $"{_c} files created successfully!";
                return View(folderMediaModel);
            }
            ViewBag.CheckFolderFailed = "Folder already created!";
            return View(folderMediaModel);
        }

        // GET: FolderMedia/FolderMediaModels/Edit/5
        public async Task<IActionResult> Edit(Guid? id, string path)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folderMedia = await _context.FolderMediaModel.FindAsync(id);
            if (folderMedia == null)
            {
                return NotFound();
            }

            if (path != null)
                System.IO.File.Delete(path);

            return View(folderMedia);
        }

        // POST: FolderMedia/FolderMediaModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string old_path, Guid id, [Bind("FolderMediaID,FolderMediaName,IsAvtive")] FolderMediaModel folderMediaModel)
        {
            if (id != folderMediaModel.FolderMediaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(folderMediaModel);
                    await _context.SaveChangesAsync();

                    string new_path = "wwwroot/media/" + folderMediaModel.FolderMediaName;
                    if (!Directory.Exists(new_path))
                    {
                        old_path = "wwwroot/media/" + old_path;
                        Directory.Move(old_path, new_path);
                        ViewBag.Checker = "Moved folder successfully!";
                    }
                    else
                    {
                        ViewBag.Checker = "This folder already created!";
                    }
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FolderMediaModelExists(folderMediaModel.FolderMediaID))
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
            return View(folderMediaModel);
        }

        // GET: FolderMedia/FolderMediaModels/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var folderMediaModel = await _context.FolderMediaModel
                .FirstOrDefaultAsync(m => m.FolderMediaID == id);
            if (folderMediaModel == null)
            {
                return NotFound();
            }

            return View(folderMediaModel);
        }

        // POST: FolderMedia/FolderMediaModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var folderMediaModel = await _context.FolderMediaModel.FindAsync(id);
            if (folderMediaModel != null)
            {
                _context.FolderMediaModel.Remove(folderMediaModel);
            }

            await _context.SaveChangesAsync();

            string path = string.Empty;
            if (folderMediaModel != null)
                path = Path.Combine($"{_webHost.WebRootPath}/media", folderMediaModel.FolderMediaName);

            if (Directory.Exists(path))
                Directory.Delete(path, true);

            return RedirectToAction(nameof(Index));
        }

        private bool FolderMediaModelExists(Guid id)
        {
            return _context.FolderMediaModel.Any(e => e.FolderMediaID == id);
        }
    }
}
