
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NguyenTruong_241230872.Models;

public class SachesController : Controller
{
    private readonly QLNhaSachContext _context;

    public SachesController(QLNhaSachContext context)
    {
        _context = context;
    }

    // GET: SACHS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Saches.ToListAsync());
    }

    // GET: SACHS/Details/5
    public async Task<IActionResult> Details(int? masach)
    {
        if (masach == null)
        {
            return NotFound();
        }

        var sach = await _context.Saches
            .FirstOrDefaultAsync(m => m.MaSach == masach);
        if (sach == null)
        {
            return NotFound();
        }

        return View(sach);
    }

    // GET: SACHS/Create
    public IActionResult Create()
    {
        ViewBag.DanhSachTL = new SelectList(_context.TheLoais, "MaTl", "TenTl");
        return View();
    }

    // POST: SACHS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("MaSach,MaTl,TenSach,Gia,Anh,MaTlNavigation")] Sach sach)
    {
        if (ModelState.IsValid)
        {
            _context.Add(sach);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index", "Home", new {maTL=sach.MaTl});
        }
        ViewBag.DanhSachTL = new SelectList(_context.TheLoais, "MaTl", "TenTl", sach.MaTl);
        return View(sach);
    }

    // GET: SACHS/Edit/5
    public async Task<IActionResult> Edit(int? masach)
    {
        if (masach == null)
        {
            return NotFound();
        }

        var sach = await _context.Saches.FindAsync(masach);
        if (sach == null)
        {
            return NotFound();
        }
        return View(sach);
    }

    // POST: SACHS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? masach, [Bind("MaSach,MaTl,TenSach,Gia,Anh,MaTlNavigation")] Sach sach)
    {
        if (masach != sach.MaSach)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(sach);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SachExists(sach.MaSach))
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
        return View(sach);
    }

    // GET: SACHS/Delete/5
    public async Task<IActionResult> Delete(int? masach)
    {
        if (masach == null)
        {
            return NotFound();
        }

        var sach = await _context.Saches
            .FirstOrDefaultAsync(m => m.MaSach == masach);
        if (sach == null)
        {
            return NotFound();
        }

        return View(sach);
    }

    // POST: SACHS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? masach)
    {
        var sach = await _context.Saches.FindAsync(masach);
        if (sach != null)
        {
            _context.Saches.Remove(sach);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool SachExists(int? masach)
    {
        return _context.Saches.Any(e => e.MaSach == masach);
    }
}
