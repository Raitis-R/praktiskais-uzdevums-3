using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using praktiskais_uzdevums_3.Models;

namespace praktiskais_uzdevums_3.Controllers;

public class QueriesController : Controller
{
    private readonly LibraryDbContext _db;

    public QueriesController(LibraryDbContext db)
    {
        _db = db;
    }

    public IActionResult ByAuthor(string? text)
    {
        ViewBag.Text = text;
        ViewBag.Searched = text != null;

        var result = new List<Book>();
        if (!string.IsNullOrWhiteSpace(text))
        {
            result = _db.Books
                .Include(b => b.Author).Include(b => b.Genre)
                .Where(b => b.Author.Name.Contains(text))
                .ToList();
        }
        return View(result);
    }
    public IActionResult ByYear(int? year)
    {
        ViewBag.Year = year;
        ViewBag.Searched = year != null;

        var result = new List<Book>();
        if (year != null)
        {
            result = _db.Books
                .Include(b => b.Author).Include(b => b.Genre)
                .Where(b => b.Year == year)
                .OrderBy(b => b.Year)
                .ToList();
        }
        return View(result);
    }
    public IActionResult ByGenre(string? text)
    {
        ViewBag.Text = text;
        ViewBag.Searched = text != null;

        var result = new List<Book>();
        if (!string.IsNullOrWhiteSpace(text))
        {
            result = _db.Books
                .Include(b => b.Author).Include(b => b.Genre)
                .Where(b => b.Genre.Title.Contains(text))
                .ToList();
        }
        return View(result);
    }

    public IActionResult SortedByYear()
    {
        var result = _db.Books
            .Include(b => b.Author).Include(b => b.Genre)
            .OrderBy(b => b.Year)
            .ToList();
        return View(result);
    }

    public IActionResult AuthorCounts()
    {
        var result = _db.Authors
            .Select(a => new { a.Name, Count = a.Books.Count() })
            .OrderByDescending(x => x.Count)
            .ToList()
            .Select(x => (x.Name, x.Count))
            .ToList();
        return View(result);
    }

    public IActionResult OldestNewest()
    {
        ViewBag.Oldest = _db.Books.Include(b => b.Author)
            .OrderBy(b => b.Year).FirstOrDefault();
        ViewBag.Newest = _db.Books.Include(b => b.Author)
            .OrderByDescending(b => b.Year).FirstOrDefault();
        return View();
    }
}