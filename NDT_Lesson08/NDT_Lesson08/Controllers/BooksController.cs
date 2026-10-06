
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NDT_Lesson08.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
public class BooksController : Controller
{
    private readonly BookStoreDbContext _context;
	private void LoadLists(int? categoryId = null, int? publisherId = null)
	{
		ViewData["CategoryId"] = new SelectList(_context.Categories, "CategoryId", "CategoryName", categoryId);
		ViewData["PublisherId"] = new SelectList(_context.Publishers, "PublisherId", "PublisherName", publisherId);
	}

	public BooksController(BookStoreDbContext context)
    {
        _context = context;
    }

    // GET: BOOKS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Books.ToListAsync());
    }

    // GET: BOOKS/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // GET: BOOKS/Create
    public IActionResult Create()
    {
		LoadLists();
		return View();
    }

    // POST: BOOKS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
    {
        if (ModelState.IsValid)
        {
            _context.Add(book);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        LoadLists(book.CategoryId, book.PublisherId);
		return View(book);
    }

    // GET: BOOKS/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }
        LoadLists(book.CategoryId, book.PublisherId);
		return View(book);
    }

    // POST: BOOKS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? id, [Bind("BookId,Title,Author,Release,Price,Description,Picture,PublisherId,CategoryId")] Book book)
    {
        if (id != book.BookId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BookExists(book.BookId))
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
        LoadLists(book.CategoryId, book.PublisherId);
		return View(book);
    }

    // GET: BOOKS/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var book = await _context.Books
            .FirstOrDefaultAsync(m => m.BookId == id);
        if (book == null)
        {
            return NotFound();
        }

        return View(book);
    }

    // POST: BOOKS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book != null)
        {
            _context.Books.Remove(book);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool BookExists(string? id)
    {
        return _context.Books.Any(e => e.BookId == id);
    }
}
