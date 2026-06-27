using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCIntro.Models;

namespace MVCIntro.Controllers
{
    public class AuthorController : Controller
    {
        private readonly MvcDbContext _context = new MvcDbContext();
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            base.Dispose(disposing);
        }

        public async Task<IActionResult> Index()
        {
            var authors = await _context.Authors.ToListAsync();
            return View(authors);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await _context.Authors.AddAsync(model);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
            {
                return RedirectToAction(nameof(NotFoundAuthor));
            }

            return View(author);
        }

        public IActionResult NotFoundAuthor()
        {
            return View();
        }

        public async Task<IActionResult> Edit(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
            {
                return RedirectToAction(nameof(NotFoundAuthor));
            }

            return View(author);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Author model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            _context.Authors.Update(model);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var author = await _context.Authors.FindAsync(id);
            if (author is null)
            {
                return RedirectToAction(nameof(NotFoundAuthor));
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
