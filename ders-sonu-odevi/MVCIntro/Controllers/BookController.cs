using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVCIntro.Models;

namespace MVCIntro.Controllers;

public class BookController : Controller
{
    private readonly MvcDbContext _context = new MvcDbContext();
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _context.Dispose();//Veri tabanı bağlantısını kapatıp, kaynakları RAM'e iade eder.
        }
        base.Dispose(disposing);
    }
    public async Task<IActionResult> Index()
    {
        var books = await _context.Books.ToListAsync();
        // books=[];
        return View(books);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        await _context.Books.AddAsync(model);
        await _context.SaveChangesAsync();
        
        return RedirectToAction(nameof(Index)); // "Index"
    }

    public async Task<IActionResult> Details(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if(book is null)
        {
            return RedirectToAction(nameof(NotFoundBook));
        }

        return View(book);
    }


    public IActionResult NotFoundBook()
    {
        return View();
    }


    public async Task<IActionResult> Edit(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
        {
            return RedirectToAction(nameof(NotFoundBook));
        }

        return View(book);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Book model)
    {
        if (!ModelState.IsValid)
        {
            return View(model);
        }
        _context.Books.Update(model);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book is null)
        {
            return RedirectToAction(nameof(NotFoundBook));
        }
        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

}
   