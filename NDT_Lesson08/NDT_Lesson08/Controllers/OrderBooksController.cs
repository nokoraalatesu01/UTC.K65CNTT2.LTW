using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NDT_Lesson08.Models;

public class OrderBooksController : Controller
{
    private readonly BookStoreDbContext _context;

    public OrderBooksController(BookStoreDbContext context)
    {
        _context = context;
    }

    private void LoadAccounts(string? selected = null)
    {
        ViewData["AccountId"] = new SelectList(_context.Accounts, "AccountId", "Username", selected);
    }

    // GET: OrderBooks
    public async Task<IActionResult> Index()
    {
        var list = await _context.OrderBooks
            .Include(o => o.Account)
            .ToListAsync();
        return View(list);
    }

    // GET: OrderBooks/Details/5
    public async Task<IActionResult> Details(string? id)
    {
        if (id == null) return NotFound();

        var orderBook = await _context.OrderBooks
            .Include(o => o.Account)
            .Include(o => o.OrderDetails).ThenInclude(d => d.Book)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (orderBook == null) return NotFound();

        return View(orderBook);
    }

    // GET: OrderBooks/Create
    public IActionResult Create()
    {
        LoadAccounts();
        return View();
    }

    // POST: OrderBooks/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("OrderId,OrderDate,AccountId,ReceiveAddress,ReceivePhone,OrderReceive,Note,Status")] OrderBook orderBook)
    {
        if (await _context.OrderBooks.AnyAsync(o => o.OrderId == orderBook.OrderId))
        {
            ModelState.AddModelError("OrderId", "Mã đơn hàng đã tồn tại.");
        }

        if (ModelState.IsValid)
        {
            _context.Add(orderBook);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        LoadAccounts(orderBook.AccountId);
        return View(orderBook);
    }

    // GET: OrderBooks/Edit/5
    public async Task<IActionResult> Edit(string? id)
    {
        if (id == null) return NotFound();

        var orderBook = await _context.OrderBooks.FindAsync(id);
        if (orderBook == null) return NotFound();

        LoadAccounts(orderBook.AccountId);
        return View(orderBook);
    }

    // POST: OrderBooks/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(string? id, [Bind("OrderId,OrderDate,AccountId,ReceiveAddress,ReceivePhone,OrderReceive,Note,Status")] OrderBook orderBook)
    {
        if (id != orderBook.OrderId) return NotFound();

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(orderBook);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderBookExists(orderBook.OrderId)) return NotFound();
                throw;
            }
            return RedirectToAction(nameof(Index));
        }
        LoadAccounts(orderBook.AccountId);
        return View(orderBook);
    }

    // GET: OrderBooks/Delete/5
    public async Task<IActionResult> Delete(string? id)
    {
        if (id == null) return NotFound();

        var orderBook = await _context.OrderBooks
            .Include(o => o.Account)
            .FirstOrDefaultAsync(m => m.OrderId == id);
        if (orderBook == null) return NotFound();

        return View(orderBook);
    }

    // POST: OrderBooks/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(string? id)
    {
        var orderBook = await _context.OrderBooks.FindAsync(id);
        if (orderBook != null)
        {
            // Đơn hàng còn chi tiết thì không xóa được (khóa ngoại)
            if (await _context.OrderDetails.AnyAsync(d => d.OrderId == id))
            {
                TempData["Error"] = "Không thể xóa: đơn hàng này còn chi tiết đơn hàng. Hãy xóa các chi tiết trước.";
                return RedirectToAction(nameof(Delete), new { id });
            }
            _context.OrderBooks.Remove(orderBook);
            await _context.SaveChangesAsync();
        }
        return RedirectToAction(nameof(Index));
    }

    private bool OrderBookExists(string id)
    {
        return _context.OrderBooks.Any(e => e.OrderId == id);
    }
}